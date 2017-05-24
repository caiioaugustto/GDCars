using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Factory;
using VendaDeAutomoveis.Factory.EntidadesFactory;

namespace VendaDeAutomoveis.DAO
{
    public class PagamentosDAO
    {
        private VendasContext context;
        public IList<FormaDePagamento> Lista()
        {
            return context.FormaDePagamento.ToList();
        }
        public PagamentosDAO(VendasContext context)
        {
            this.context = context;
        }
        public FormaDePagamento PegarPagamento(int IdPagamento)
        {
            var pagamento = context.FormaDePagamento.Where(b => b.IdFormaDePagamento == IdPagamento).FirstOrDefault();
            return pagamento;
        }
        public IList<FormaDePagamento>ListaDePagamentosVip()
        {
            return context.FormaDePagamento
                .Where(u => u.TipoDoCliente == "Vip" || u.TipoDoCliente == "Ambos")
                .ToList();
        }
        public IList<FormaDePagamento>ListaDePagamentosComum()
        {
            return context.FormaDePagamento
                .Where(u => u.TipoDoCliente == "Comum" || u.TipoDoCliente == "Ambos")
                .ToList();
        }
    }
}
