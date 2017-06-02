using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using VendaDeAutomoveis.DAO.ConnectionContext;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Repository.ConnectionContext.Adapters;
using VendaDeAutomoveis.Repository.ConnectionContext.Context;
using VendaDeAutomoveis.Repository.ConnectionContext.Interfaces;

namespace VendaDeAutomoveis.Repository
{
    public class ClienteRepository : RepositoryBase<GDC_Clientes>, IRepository<Cliente>
    {
        string connectionString = GDCarsConnectionString.Connection();

        public ClienteRepository(GDCarsContext context)
            : base(context)
        {
        }

        public Cliente VerificarCPFExistente(string cpf)
        {
            var sql = "SELECT * FROM GDC_Clientes where CPF = @cpf ";

            var e = _context.Database.Connection.Query(sql,
                param: new
                {
                    cpf = cpf
                });

            return e.FirstOrDefault();

            //var validarCliente = _context.Clientes.Where(c => c.CPF == cpf).FirstOrDefault().ToDomain();
            //return validarCliente;
        }

        public IList<Cliente> ObterTodos()
        {
            var sql = "SELECT * FROM GDC_Clientes ";

            return _context.Database.Connection.Query<Cliente>(sql)
                .OrderBy(c => c.Nome)
                .ThenBy(c => c.DataNascimento)
                .ToList();
        }

        public IQueryable<Cliente> Obter(Func<Cliente, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Cliente obj)
        {
            _context.Clientes.Add(obj.ToDbEntity());
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public void Adicionar(Cliente obj)
        {
            _context.Clientes.Add(obj.ToDbEntity());
            Salvar();
        }

        public void Excluir(Func<Cliente, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Editar(Cliente obj)
        {
            _context.Clientes.Attach(obj.ToDbEntity());
            var entry = _context.Entry(obj);
            entry.State = System.Data.Entity.EntityState.Modified;
            Salvar();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Cliente ObterPorId(Guid id)
        {
            var sql = "SELECT * FROM GDC_Clientes where Id = @id ";

            var e = _context.Database.Connection.Query(sql,
                param: new
                {
                    id = id
                });

            return e.FirstOrDefault();
        }
    }
}