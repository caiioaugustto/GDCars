using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.DAO
{
    public class ClienteDAO
    {
        private VendasContext context;
        public ClienteDAO(VendasContext context)
        {
            this.context = context;
        }
        public void AdicionarCliente(Clientes cliente)
        {
            cliente.TipoDoCliente = TipoCliente.Comum;
            
            context.Clientes.Add(cliente);
            //context.SaveChanges();
        }
        public void EditarCliente(Clientes cliente)
        {
            context.Clientes.Attach(cliente);
            var entry = context.Entry(cliente);
            entry.State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public bool VerificarCPFExistente(Clientes cliente)
        {
          Clientes clientes = context.Clientes
          .Where(u => u.CPF == cliente.CPF)
          .FirstOrDefault();
          
          if( clientes == null )
          {
              return false;
          }
          else
          {
              return true;
          }
        }
       
        public IList<Clientes> Lista()
        {
            return context.Clientes.OrderBy(a => a.Nome)
                .ThenBy(a => a.DataNascimento).ToList();
        }
        public Clientes PegarCliente(int IdCliente)
        {
            var cliente = context.Clientes.Where(b => b.IdCliente == IdCliente).FirstOrDefault();
            return cliente;
        }
    }
}