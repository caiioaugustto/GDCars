using System.ComponentModel.DataAnnotations;

namespace VendaDeAutomoveis.Entidades
{
    public class Produtos : Entity
    {
        //[Key]
        //[Required(ErrorMessage = "O campo ID do Veiculo é obrigatório")]
        //public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do Fabricante")]
        [StringLength(30, MinimumLength = 3)] 
        public string Fabricante { get; set; }

        [Required(ErrorMessage = "Informe o  Nome do Veiculo")]
        [StringLength(30, MinimumLength = 3)]
        public string ModeloVeiculo { get; set; }

        [Required(ErrorMessage = "Informe o Ano de Fabricação")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade de Portas")]
        public int QuantidadeDePortas { get; set; }

        //[Required(ErrorMessage = "Informe o Preço do Veiculo")]
        //public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe o Tipo do Veiculo")]
        public TipoVeiculo TipoVeiculo { get; set; }
    }
}