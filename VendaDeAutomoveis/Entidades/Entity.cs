using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendaDeAutomoveis.Entidades
{
    public class Entity : EntityUpload
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data_Cadastro { get; set; }

        [Required]
        public double Valor { get; set; }
    }
}