using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pecas2.Data;
using Pecas2.Models;

namespace Pecas2.Controllers
{
    public class ItemPedidoController : Controller
    {
        private readonly PecasContext _context;

        public ItemPedidoController(PecasContext context)
        {
            _context = context;
        }

        // GET: ItemPedido
        public async Task<IActionResult> Index()
        {
            var pecasContext = _context.ItemPedidos.Include(i => i.Pedido).Include(i => i.Produto);
            return View(await pecasContext.ToListAsync());
        }

        // GET: ItemPedido/Details/5
        public async Task<IActionResult> Details(int? pedidoId, int? produtoId)
        {
            // Verifica se os parâmetros foram fornecidos
            if (pedidoId == null || produtoId == null)
            {
                return NotFound();
            }

            // Busca o itemPedido com base na chave composta (PedidoId e ProdutoId)
            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.PedidoId == pedidoId && m.ProdutoId == produtoId);

            // Se não encontrar, retorna "NotFound"
            if (itemPedido == null)
            {
                return NotFound();
            }

            // Retorna a view com o ItemPedido encontrado
            return View(itemPedido);
        }


        // GET: ItemPedido/Create
        public IActionResult Create()
        {
            var pedidos = _context.Pedido.ToList();
            var produtos = _context.Produto.ToList();

            if (pedidos == null || !pedidos.Any())
            {
                // Lidar com o caso em que não há pedidos disponíveis
                ModelState.AddModelError("", "Nenhum pedido disponível.");
            }

            if (produtos == null || !produtos.Any())
            {
                // Lidar com o caso em que não há produtos disponíveis
                ModelState.AddModelError("", "Nenhum produto disponível.");
            }

            // Passando o preço do produto junto com o Id e Nome
            ViewData["PedidoId"] = new SelectList(pedidos, "Id", "Descricao");
            ViewData["ProdutoId"] = new SelectList(produtos, "Id", "Nome");

            // Criando uma lista de produtos com seus preços
            ViewBag.ProdutosComPreco = produtos.Select(p => new { p.Id, p.Nome, p.Preco }).ToList();

            return View();
        }



        // POST: ItemPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int PedidoId, List<int> ProdutoIds, List<int> Quantidades)
        {
            if (ProdutoIds != null && Quantidades != null && ProdutoIds.Count == Quantidades.Count)
            {
                for (int i = 0; i < ProdutoIds.Count; i++)
                {
                    var produto = _context.Produto.Find(ProdutoIds[i]);
                    if (produto != null)
                    {
                        var itemPedido = new ItemPedido
                        {
                            PedidoId = PedidoId,
                            ProdutoId = ProdutoIds[i],
                            Quantidade = Quantidades[i],
                            // Adiciona o cálculo do preço total diretamente no ItemPedido
                            Total = Quantidades[i] * produto.Preco
                        };

                        _context.ItemPedidos.Add(itemPedido);
                    }
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Caso não haja produtos ou quantidades, retorne à página de criação com erro
            return View();
        }

        // GET: ItemPedido/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int pedidoId, int produtoId)
        {
            if (pedidoId == 0 || produtoId == 0)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.PedidoId == pedidoId && m.ProdutoId == produtoId);

            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int pedidoId, int produtoId, ItemPedido itemPedido)
        {
            if (pedidoId != itemPedido.PedidoId || produtoId != itemPedido.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPedidoExists(itemPedido.PedidoId, itemPedido.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemPedido);
        }

        private bool ItemPedidoExists(int pedidoId, int produtoId)
        {
            return _context.ItemPedidos.Any(e => e.PedidoId == pedidoId && e.ProdutoId == produtoId);
        }


        // GET: ItemPedido/Delete/5
        // Ação GET para a página de confirmação de exclusão
        // Ação GET para a página de confirmação de exclusão
        public async Task<IActionResult> Delete(int? pedidoId, int? produtoId)
        {
            if (pedidoId == null || produtoId == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.PedidoId == pedidoId && m.ProdutoId == produtoId);

            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // Ação POST para realmente excluir o item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int pedidoId, int produtoId)
        {
            var itemPedido = await _context.ItemPedidos
                .FirstOrDefaultAsync(m => m.PedidoId == pedidoId && m.ProdutoId == produtoId);

            if (itemPedido != null)
            {
                _context.ItemPedidos.Remove(itemPedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
