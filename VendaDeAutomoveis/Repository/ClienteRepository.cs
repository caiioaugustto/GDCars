using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VendaDeAutomoveis.DAO.ConnectionContext;
using VendaDeAutomoveis.Entidades;
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
            using (SqlConnection connSql = new SqlConnection(connectionString))
            {
                SqlCommand cmdSql = new SqlCommand("Select * from GDC_Clientes", connSql);

                connSql.Open();

                var clientes = new List<Cliente>();

                using (SqlDataReader sdr = cmdSql.ExecuteReader())
                {
                    var cliente = new Cliente();

                    if (sdr.Read())
                    {
                        cliente.IdCliente = Guid.Parse(sdr["Id"].ToString());
                        cliente.Nome = (String)sdr["Nome"];
                        cliente.CPF = Convert.ToString("CPF");
                        cliente.RG = (String)sdr["RG"];
                        cliente.Email = (String)sdr["Email"];
                        cliente.TipoDoCliente = Cliente.TipoCliente.Comum;
                        cliente.IdEndereco = Guid.Parse(sdr["IdEndereco"].ToString());
                    }

                    clientes.Add(cliente);
                }

                connSql.Close();

                return clientes;
            }

            //List<Cliente> clientes = new List<Cliente>();

            //using (var sqlConnection = new SqlConnection(connectionString))
            //{
            //    var queryClientes = sqlConnection.Query<Cliente>("Select * from GDC_Clientes");

            //    foreach (Cliente cliente in queryClientes)
            //        clientes.Add(cliente);
            //}

            //return clientes;

            //var sql = "SELECT * FROM GDC_Clientes ";

            //return _context.Database.Connection.Query<Cliente>(sql)
            //    .OrderBy(c => c.Nome)
            //    .ThenBy(c => c.DataNascimento)
            //    .ToList();
        }

        public IQueryable<Cliente> Obter(Func<Cliente, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Guid idEndereco, Guid idCliente)
        {
            var sql = "update GDC_Clientes set IdEndereco = @idEndereco where Id = @idCliente ";

            var e = _context.Database.Connection.Execute(sql,
                param: new
                {
                    idEndereco = idEndereco,
                    idCliente = idCliente
                });

            Salvar();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public void Adicionar(Cliente obj)
        {
            //_context.Clientes.Add(obj.ToDbEntity());
            //Salvar();
        }

        public void Excluir(Func<Cliente, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Editar(Cliente obj)
        {
            var sql = "update GDC_Clientes set Nome = @nome, RG = @rg, CPF = @cpf, DataNascimento = @datanascimento where Id = @idCliente ";

            var e = _context.Database.Connection.Execute(sql,
                param: new
                {
                    idCliente = obj.IdCliente,

                    nome = obj.Nome,
                    RG = obj.RG,
                    CPF = obj.CPF,
                    DataNascimento = obj.DataNascimento,
                });

            Salvar();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Cliente ObterPorId(Guid id)
        {
            var sql = "SELECT * FROM GDC_Clientes where Id = '@id' ";

            return _context.Database.Connection.Query(sql,
                    param: new
                    {
                        id = id
                    }).FirstOrDefault();

        }
    }
}