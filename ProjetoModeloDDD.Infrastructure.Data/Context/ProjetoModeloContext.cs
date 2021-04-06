using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using ProjetoModeloDDD.Infrastructure.Data.EntityConfig;

namespace ProjetoModeloDDD.Infrastructure.Data.Context
{
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext() : base("ProjetoModeloDDD") 
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Define qualquer propriedade com o nome terminado em 'ID' em uma primary key.
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(x => x.IsKey());
            // Qualquer propriedade criada como string vai ser uma varchar no banco. Pro default o EF cria como nvarchar que ocupa 2x em aspaço.
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));
            // Configura o varchar com o tamanho de 100, caso nao informe o tipo e nem o tamano, ele pega esses valores por default.
            modelBuilder.Properties<string>()
                .Configure(x => x.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if(item.State == EntityState.Added)
                {
                    item.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if(item.State == EntityState.Modified)
                {
                    item.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}