using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.Models
{
    public class HistoricoPedidosModel
    {
        public int? IdCliente { get; set; }
        public IList<Vendas> Vendas { get; set; }
        public IList<Clientes> Clientes { get; set; }
    }
}