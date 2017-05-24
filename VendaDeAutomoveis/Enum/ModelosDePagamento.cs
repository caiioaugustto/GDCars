using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendaDeAutomoveis.Entidades
{
    public enum ModelosDePagamento
    {
        PagamentoAVista = 1, 
        PagamentoAPrazo12xComJuros = 2, 
        PagamentoAPrazo12xSemJuros = 3, 
        PagamentoAPrazo60xComJuros = 4, 
        PagamentoAPrazo60xSemJuros = 5
    }
}