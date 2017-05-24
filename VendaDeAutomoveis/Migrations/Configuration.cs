namespace VendaDeAutomoveis.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VendaDeAutomoveis.Entidades;
    using VendaDeAutomoveis.Enum;

    internal sealed class Configuration : DbMigrationsConfiguration<VendaDeAutomoveis.DAO.VendasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VendaDeAutomoveis.DAO.VendasContext context)
        {
            context.Logins.AddOrUpdate(
                l => l.IdLogin,
                 new Logins
                 {
                     IdLogin = 1,
                     Email = "caio@admin.com.br",
                     Senha = "21-23-2f-29-7a-57-a5-a7-43-89-4a-0e-4a-80-1f-c3",
                     Nome = "Caio",
                     SobreNome = "Augusto",
                     TipoAcesso = NivelAcesso.Admin
                 });

            context.FormaDePagamento.AddOrUpdate(
               f => f.IdFormaDePagamento,
               new FormaDePagamento { IdFormaDePagamento = 1, ModeloFormaDePagamento = "Á vista (Ambos)", TipoDoCliente = "Ambos" },
               new FormaDePagamento { IdFormaDePagamento = 2, ModeloFormaDePagamento = "Financiamento 12x s/ juros (VIP)", TipoDoCliente = Convert.ToString(TipoCliente.Vip) },
               new FormaDePagamento { IdFormaDePagamento = 3, ModeloFormaDePagamento = "Financiamento 60x s/ juros (VIP)", TipoDoCliente = Convert.ToString(TipoCliente.Vip) },
               new FormaDePagamento { IdFormaDePagamento = 4, ModeloFormaDePagamento = "Financiamento 12x c/ juros (Comum)", TipoDoCliente = Convert.ToString(TipoCliente.Comum) },
               new FormaDePagamento { IdFormaDePagamento = 5, ModeloFormaDePagamento = "Financiamento 60x c/ juros (Comum)", TipoDoCliente = Convert.ToString(TipoCliente.Comum) }
               );


            context.Produtos.AddOrUpdate(
                p => p.Id,
                new Produtos { Id = 1, Ano = 2015, Fabricante = "Chevrolet", ModeloVeiculo = "Celta", Valor = 25000, QuantidadeDePortas = 4, TipoVeiculo = TipoVeiculo.Hatch },
                new Produtos { Id = 2, Ano = 2015, Fabricante = "Chevrolet", ModeloVeiculo = "Cruze", Valor = 50000, QuantidadeDePortas = 4, TipoVeiculo = TipoVeiculo.Esportivo }
                );
        }
    }
}
