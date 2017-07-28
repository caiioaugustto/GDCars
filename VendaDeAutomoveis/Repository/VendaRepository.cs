using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VendaDeAutomoveis.Repository.ConnectionContext.Adapters;
using VendaDeAutomoveis.Repository.ConnectionContext.Context;
using VendaDeAutomoveis.Repository.ConnectionContext.Interfaces;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.Repository
{
    public class VendaRepository : RepositoryBase<GDC_Vendas>, IVendasRepository
    {
        public VendaRepository(GDCarsContext context)
            : base(context)
        {
        }

        public void Adicionar(Venda obj)
        {
            obj.DataCompra = DateTime.Now;

            _context.Vendas.Add(obj.ToDbEntity());
        }

        public IList<Venda> BuscarPorCliente(Guid? id)
        {
            var sql = "SELECT * FROM GDC_Vendas where IdCliente = @id ";

            var e = _context.Database.Connection.Query(sql,
                param: new
                {
                    id = id
                });

            return e.FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Editar(Venda obj)
        {
            _context.Vendas.Attach(obj.ToDbEntity());
            var entry = _context.Entry(obj);
            entry.State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(Func<Venda, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public decimal GastosPorCliente(Guid id)
        {
            return _context.Vendas.Where(c => c.Id == id.ToString()).Sum(c => c.Valor);
        }

        public IQueryable<Venda> Obter(Func<Venda, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Venda ObterPorId(Guid id)
        {
            var sql = "SELECT * FROM GDC_Vendas where Id = @id ";
            
            var e = _context.Database.Connection.Query(sql,
                param: new
                {
                    id = id
                });

            return e.FirstOrDefault();

            //return _context.Vendas.Where(c => c.Id == id.ToString()).FirstOrDefault();
        }

        public IList<Venda> ObterTodos()
        {
            var sql = "SELECT * FROM GDC_Vendas ";

            return _context.Database.Connection.Query<Venda>(sql)
                .OrderBy(c => c.DataCompra)
                .ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}