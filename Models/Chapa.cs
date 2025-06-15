using System;
using System.ComponentModel.DataAnnotations;

namespace MarmoreGranito.API.Models
{
    public class Chapa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O bloco de origem é obrigatório")]
        public int BlocoId { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo de material deve ter no máximo 100 caracteres")]
        public string TipoMaterial { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "A largura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "A espessura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A espessura deve ser maior que zero")]
        public decimal Espessura { get; set; }

        [Required(ErrorMessage = "O valor unitário é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior que zero")]
        public decimal ValorUnitario { get; set; }

        public DateTime DataCadastro { get; set; }
        public bool Disponivel { get; set; }

        public virtual Bloco Bloco { get; set; }
    }
} 