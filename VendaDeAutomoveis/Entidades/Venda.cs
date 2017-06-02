using System;
using System.ComponentModel.DataAnnotations;

namespace VendaDeAutomoveis.Entidades
{
    public class Venda : Entity
    {
        
        //[Key]
        //[Required]
        //public int IdVenda { get; set; }

        //[Required(ErrorMessage = "Informe o valor do produto")]
        //public decimal Valor { get; set; }

        [Required(ErrorMessage="Informe a data da compra")]
        public DateTime DataCompra { get; set; }

        [Required]
        public Guid IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Informe o Modelo do Produto")]
        public Guid IdProduto { get; set; }
        
        public virtual Veiculo Veiculo { get; set; }

        [Required]
        public Guid IdFormaDePagamento { get; set; }

        public virtual FormaDePagamento FormaDePagamento { get; set; }

        //[Required(ErrorMessage = "Informe o Modelo do Produto")]
        //public int IdUpload { get; set; }

        //public virtual UploadArquivo Upload { get; set; }

        [Required(ErrorMessage = "Informe a Performance")]
        public Guid IdPerfomance { get; set; }

        public virtual Performance Perfomance { get; set; }

        [Required (ErrorMessage = "Informe o tipo de entrega")]
        public Entrega TipoEntrega { get; set; }

        [Required(ErrorMessage = "Informe uma observação sobre o veiculo")]
        public string Observacoes { get; set; }

        public enum Entrega
        {
            Domiciliar = 1, Loja = 2
        }
    }
}