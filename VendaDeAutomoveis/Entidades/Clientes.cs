using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VendaDeAutomoveis.Enum;


namespace VendaDeAutomoveis.Entidades
{
    public class Clientes
    {
        //public Clientes()
        //{
        //    IdCliente = Guid.NewGuid();
        //}

        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Informe o nome do cliente")]
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o número RG do cliente")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Informe o número de CPF/MF do cliente")]
        public string CPF { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [Required(ErrorMessage = "Informe a data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe em qual perfil o cliente se enquadra")]
        public TipoCliente TipoDoCliente { get; set; }

        public int IdEndereco { get; set; }

        public virtual Endereco Endereco { get; set; }

        //adicionar na entidade Endereco
        //public int IdCliente { get; set; }
        //public Cliente cliente { get; set; }

        //[StringLength(50)]
        //public string Endereco { get; set; }

        //[StringLength(6)]
        //public string NumeroDaCasa { get; set; }

        //[StringLength(10)]
        //public string Complemento { get; set; }

        //public string CEP { get; set; }

        //[StringLength(25)]
        //public string Bairro { get; set; }

        //[StringLength(20)]
        //public string Cidade { get; set; }

        //[StringLength(15)]
        //public string Estado { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

    }
}