using System.ComponentModel.DataAnnotations;

namespace MarmoreGranito.API.Models
{
    public class ProcessoSerragemRequest
    {
        [Required(ErrorMessage = "O bloco é obrigatório")]
        public int BlocoId { get; set; }

        [Required(ErrorMessage = "A quantidade de chapas é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de chapas deve ser maior que zero")]
        public int QuantidadeChapas { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "A espessura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A espessura deve ser maior que zero")]
        public decimal Espessura { get; set; }

        public string? Observacoes { get; set; }
    }
} 