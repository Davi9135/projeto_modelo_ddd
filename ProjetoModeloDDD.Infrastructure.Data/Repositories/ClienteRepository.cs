using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoModeloDDD.Infrastructure.Data.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public IEnumerable<Cliente> BuscarPorNome(string nome)
        {
            return Db.Clientes.Where(c => c.Nome.Contains(nome));
        }
    }
}