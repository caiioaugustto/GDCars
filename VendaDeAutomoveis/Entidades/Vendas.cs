using System;
using System.ComponentModel.DataAnnotations;
using VendaDeAutomoveis.Enum;

namespace VendaDeAutomoveis.Entidades
{
    public class Vendas
    {
        [Key]
        [Required]
        public int IdVenda { get; set; }

        [Required(ErrorMessage = "Informe o valor do produto")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage="Informe a data da compra")]
        public DateTime DataCompra { get; set; }

        [Required]
        public int IdCliente { get; set; }

        public virtual Clientes Cliente { get; set; }

        [Required(ErrorMessage = "Informe o Modelo do Produto")]
        public int IdProduto { get; set; }
        
        public virtual Produtos Produto { get; set; }

        [Required]
        public int IdFormaDePagamento { get; set; }

        public virtual FormaDePagamento FormaDePagamento { get; set; }

        //[Required(ErrorMessage = "Informe o Modelo do Produto")]
        //public int IdUpload { get; set; }

        //public virtual UploadArquivo Upload { get; set; }

        [Required(ErrorMessage = "Informe a Performance")]
        public int IdPerfomance { get; set; }

        public virtual Performance Perfomance { get; set; }

        [Required (ErrorMessage = "Informe o tipo de entrega")]
        public TipoEntrega TipoEntrega { get; set; }

        [Required(ErrorMessage = "Informe uma observação sobre o veiculo")]
        public string Observacoes { get; set; }
    }
}