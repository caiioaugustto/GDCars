using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.DAO
{
    public class EnderecoDAO
    {
        private VendasContext context;

        public EnderecoDAO(VendasContext context)
        {
            this.context = context;
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            context.Endereco.Add(endereco);
            context.SaveChanges();
        }

        public Endereco PegarEnderencoPorIdCliente(int IdCliente)
        {
            var endereco = context.Endereco.Where(e => e.IdCliente == IdCliente).FirstOrDefault();
            return endereco;
        }

        public void EditarEndereco(Endereco endereco)
        {
            context.Endereco.Attach(endereco);
            var entry = context.Entry(endereco);
            entry.State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}