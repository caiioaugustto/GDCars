using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendaDeAutomoveis.Entidades
{
    public class FormaDePagamento
    {
        [Key]
        public int IdFormaDePagamento { get; set; }
        public string ModeloFormaDePagamento { get; set; }
        public string TipoDoCliente { get; set; }
    }
}