using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Entidades;
using VendaDeAutomoveis.Enum;

namespace VendaDeAutomoveis.DAO
{
    public class LoginDAO
    {
        private VendasContext context;
        public LoginDAO(VendasContext context)
        {
            this.context = context;
        }

        public Logins Buscar(string email, string senha)
        {
            var validarAcesso = context.Logins.Where(u => u.Email == email && u.Senha == senha).FirstOrDefault();
            return validarAcesso;
        }

        public void Adicionar(Logins login)
        {
            login.TipoAcesso = NivelAcesso.Usuario;

            context.Logins.Add(login);
            SaveChange();
        }

        public IList<Logins> Listar()
        {
            return context.Logins.ToList();
        }

        public void BloquearAcesso(int id)
        {
            var usuario = context.Logins.Where(u => u.IdLogin == id).FirstOrDefault();
            usuario.TipoAcesso = NivelAcesso.Bloqueado;
            SaveChange();
        }

        public void SaveChange()
        {
            context.SaveChanges();
        }
    }
}