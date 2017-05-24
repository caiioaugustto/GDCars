using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.DAO
{
    public class VendaDAO
    {
        private VendasContext context;
        public VendaDAO(VendasContext context)
        {
            this.context = context;
        }
        public void AdicionarVenda(Vendas venda)
        {
            venda.DataCompra = DateTime.Now;

            context.Vendas.Add(venda);
            context.SaveChanges();
        }
        public void EditarVenda(Vendas venda)
        {
            context.Vendas.Attach(venda);
            var entry = context.Entry(venda);
            entry.State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public IList<Vendas>Lista()
        {
            return context.Vendas.ToList();
        }
        public IList<Vendas> BuscaPorCliente(int? IdCliente)
        {
            return context.Vendas.Where(m => m.IdCliente == IdCliente).ToList(); 
        }
        public decimal TotalPorCliente(int IdCliente)
        {
            return context.Vendas.Where(m => m.IdCliente == IdCliente).Sum(m => m.Valor );
        }
    }
}