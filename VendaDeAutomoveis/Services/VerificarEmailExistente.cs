using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.DAO;
using VendaDeAutomoveis.Entidades;

namespace VendaDeAutomoveis.Services
{
    static class VerificarEmailExistente
    {
        public static bool ValidarEmailExistente(this LoginDAO loginDAO, Logins login)
        {
            var user = loginDAO.Listar().Where(a => a.Email == login.Email).FirstOrDefault();

            if (user != null)
                return true;
            else
                return false;
        }
    }
}