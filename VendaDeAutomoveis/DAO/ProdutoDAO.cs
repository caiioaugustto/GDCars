using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.DAO
{
    public class ProdutoDAO
    {
        private VendasContext context;
        public ProdutoDAO(VendasContext contexto)
        {
            this.context = contexto;
        }
        public void AdicionarProduto(Produtos produto)
        {
            context.Produtos.Add(produto);
            context.SaveChanges();        
        }
        public void EditarProduto(Produtos produto)
        {
            context.Produtos.Attach(produto);
            var entry = context.Entry(produto);
            entry.State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void Excluir(int IdProduto)
        {
            //var produto = PegarProduto(IdProduto);
            //context.Produtos.Remove(produto);
            //context.SaveChanges();
            
        }

        public IList<Produtos> Lista()
        {
            return context.Produtos.ToList();
        }
        public Produtos PegarProduto(int IdProduto)
        {
            var produto = context.Produtos.Where(b => b.Id == IdProduto).FirstOrDefault();
            return produto;
        }
    }
}