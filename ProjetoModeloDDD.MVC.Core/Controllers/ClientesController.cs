using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.Core.ViewModels;
using System.Collections.Generic;

namespace ProjetoModeloDDD.MVC.Core.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IMapper _autoMapper;
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(IMapper autoMapper, IClienteAppService clienteAppService)
        {
            _autoMapper = autoMapper;
            _clienteAppService = clienteAppService;
        }
        
        // GET: ClientesController
        public ActionResult Index()
        {
            var clienteViewModel = _autoMapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteAppService.GetAll());
            return View(clienteViewModel);
        }

        public ActionResult Especiais()
        {
            var clienteViewModel = _autoMapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteAppService.ObterClientesEspeciais());
            return View(clienteViewModel);
        }

        // GET: ClientesController/Details/5
        public ActionResult Details(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            var clienteViewModel = _autoMapper.Map<Cliente, ClienteViewModel>(cliente);
            
            return View(clienteViewModel);
        }

        // GET: ClientesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                var clienteDomain = _autoMapper.Map<ClienteViewModel, Cliente>(clienteViewModel);
                _clienteAppService.Add(clienteDomain);

                return RedirectToAction("Index");
            }

            return View(clienteViewModel);
        }

        // GET: ClientesController/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            var clienteViewModel = _autoMapper.Map<Cliente, ClienteViewModel>(cliente);

            return View(clienteViewModel);
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteDomain = _autoMapper.Map<ClienteViewModel, Cliente>(cliente);
                _clienteAppService.Update(clienteDomain);

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: ClientesController/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            var clienteViewModel = _autoMapper.Map<Cliente, ClienteViewModel>(cliente);            

            return View(clienteViewModel);
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cliente = _clienteAppService.GetById(id);
            _clienteAppService.Remove(cliente);

            return RedirectToAction("Index");
        }
    }
}
