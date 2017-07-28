using System;
using System.Collections.Generic;
using System.Linq;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Repository.ConnectionContext.Adapters;
using VendaDeAutomoveis.Repository.ConnectionContext.Context;
using VendaDeAutomoveis.Repository.ConnectionContext.Interfaces;

namespace VendaDeAutomoveis.Repository
{
    public class LoginRepository : RepositoryBase<GDC_Logins>, ILoginRepository
    {
        //string connectionString = GDCarsConnectionString.Connection();
        public LoginRepository(GDCarsContext context)
            : base(context)
        {
        }

        public void Adicionar(Login login)
        {
            login.TipoAcesso = NivelAcesso.Usuario;

            _context.Logins.Add(login.ToDbEntity());
            Salvar();
        }

        public IList<Login> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Login> Obter(Func<Login, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Login ObterPorId(Guid id)
        {
            //return _context.Logins.Where(l => l.Id == id).FirstOrDefault();
            throw new NotImplementedException();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public void Excluir(Func<Login, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Editar(Login obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void BloquearAcesso(Guid id)
        {
            var usuario = _context.Logins.Where(u => u.Id == id.ToString()).FirstOrDefault();
            usuario.Tipo_Acesso = NivelAcesso.Bloqueado.ToString();
            Salvar();
        }

        public bool VerificarEmailExistente(string email)
        {
            var user = Obter(l => l.Email == email).FirstOrDefault();
            
            if (user != null)
                return true;
            else
                return false;
        }

        public Login AutenticarAcesso(string email, string senha)
        {
            var validarAcesso = _context.Logins.Where(u => u.Email == email && u.Senha == senha).FirstOrDefault();
            return validarAcesso.ToDomain();
        }
    }
}