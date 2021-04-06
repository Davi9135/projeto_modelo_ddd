using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoModeloDDD.Domain.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Produto> BuscarPorNome(string nome)
        {
            return _repository.BuscarPorNome(nome);
        }
    }
}
