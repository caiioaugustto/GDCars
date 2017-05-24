using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.EntityConfig;

namespace VendaDeAutomoveis.DAO
{
    public class VendasContext : DbContext
    {

        //public VendasContext()
        //    : base("VendasContext")
        //{

        //}

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<FormaDePagamento> FormaDePagamento { get; set; }
        public DbSet<Roda> Roda { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Cor> Cor { get; set; }
        public DbSet<Performance> Perfomance { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Upload> Upload { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendas>().HasRequired(m => m.Cliente);
            modelBuilder.Entity<Vendas>().HasRequired(m => m.Produto);
            modelBuilder.Entity<Vendas>().HasRequired(m => m.FormaDePagamento);
            modelBuilder.Entity<Vendas>().HasRequired(m => m.Perfomance);

            modelBuilder.Entity<Roda>().HasRequired(m => m.Upload);

            modelBuilder.Entity<Banco>().HasRequired(m => m.Upload);

            modelBuilder.Entity<Produtos>().HasRequired(m => m.Upload);

            modelBuilder.Entity<Clientes>().HasRequired(c => c.Endereco);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Properties()
            //    .Where(a => a.Name == "Id" + a.ReflectedType.Name)
            //    .Configure(a => a.IsKey());

            //modelBuilder.Configurations.Add(new VendaConfig());
            //modelBuilder.Configurations.Add(new ClienteConfig());
            //modelBuilder.Configurations.Add(new EnderecoConfig());
            //modelBuilder.Configurations.Add(new FormaPagamentoConfig());

            modelBuilder.Properties<string>()
               .Configure(a => a.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
               .Configure(a => a.HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }
    }
}