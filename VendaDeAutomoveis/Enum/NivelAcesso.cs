using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendaDeAutomoveis.Enum
{
    public enum NivelAcesso
    {
        [Display(Name = "Administrador")]
        Admin = 1,
        [Display(Name = "Ativo")]
        Usuario = 2,
        [Display(Name = "Bloqueado")]
        Bloqueado = 3
    }
}