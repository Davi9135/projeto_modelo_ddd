using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.MVC.Core.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IMapper _autoMapper;
        private readonly IClienteAppService _clienteAppService;
        private readonly IProdutoAppService _produtoAppService;

        public ProdutosController(IMapper autoMapper, IClienteAppService clienteAppService, IProdutoAppService produtoAppService)
        {
            _autoMapper = autoMapper;
            _clienteAppService = clienteAppService;
            _produtoAppService = produtoAppService;
        }

        // GET: ProdutosController
        public ActionResult Index()
        {
            var clienteViewModel = _autoMapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoAppService.GetAll());
            return View(clienteViewModel);
        }

        // GET: ProdutosController/Details/5
        public ActionResult Details(int id)
        {
            var produto = _produtoAppService.GetById(id);
            var produtoViewModel = _autoMapper.Map<Produto, ProdutoViewModel>(produto);

            return View(produtoViewModel);
        }

        // GET: ProdutosController/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_clienteAppService.GetAll(), "ClienteId", "Nome");

            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {                
                var produtoDomain = _autoMapper.Map<ProdutoViewModel, Produto>(produtoViewModel);                
                _produtoAppService.Add(produtoDomain);

                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(_clienteAppService.GetAll(), "ClienteId", "Nome", produtoViewModel.ClienteId);

            return View(produtoViewModel);
        }

        // GET: ProdutosController/Edit/5
        public ActionResult Edit(int id)
        {
            var produto = _produtoAppService.GetById(id);
            var produtoViewModel = _autoMapper.Map<Produto, ProdutoViewModel>(produto);
            ViewBag.ClienteId = new SelectList(_clienteAppService.GetAll(), "ClienteId", "Nome", produtoViewModel.ClienteId);

            return View(produtoViewModel);
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var produtoDomain = _autoMapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoAppService.Update(produtoDomain);

                return RedirectToAction("Index");
            }
            
            ViewBag.ClienteId = new SelectList(_clienteAppService.GetAll(), "ClienteId", "Nome", produto.ClienteId);

            return View(produto);
        }

        // GET: ProdutosController/Delete/5
        public ActionResult Delete(int id)
        {
            var produto = _produtoAppService.GetById(id);
            var produtoViewModel = _autoMapper.Map<Produto, ProdutoViewModel>(produto);

            return View(produtoViewModel);
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var produto = _produtoAppService.GetById(id);
            _produtoAppService.Remove(produto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetFiltroPorNomeDoProduto(string nome)
        {
            IEnumerable<ProdutoViewModel> produtoViewModelList = _autoMapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoAppService.BuscarPorNome(nome));
            ViewBag.nome = Request.Query["nome"].Count > 0 ? Request.Query["nome"].ToString() : string.Empty;

            return View("Index", produtoViewModelList);
        }
    }
}