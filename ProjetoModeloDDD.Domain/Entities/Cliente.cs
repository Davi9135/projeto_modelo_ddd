using System;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public virtual IEnumerable<Produto> Produtos { get; set; }
                
        public bool ClienteEspecial(Cliente cliente)
        {
            // Regra para ser um cliente Ativo:
            //    1 - Tem que ser um cliente Ativo.
            //    2 - Tem que ser um cliente que tenha pelo menos 5 anos de cadastro.
            return cliente.Ativo && ClienteComCincoOuMaisAnosDeCadastro(cliente);
        }

        public bool ClienteComCincoOuMaisAnosDeCadastro(Cliente cliente) => DateTime.Now.Year - cliente.DataCadastro.Year >= 5;
    }
}