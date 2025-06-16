using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarmoreGranito.API.Models
{
    public class Chapa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O bloco de origem é obrigatório")]
        public int BlocoId { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo de material deve ter no máximo 100 caracteres")]
        public string TipoMaterial { get; set; } = string.Empty;

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

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior ou igual a zero")]
        public int QuantidadeEstoque { get; set; }

        [JsonIgnore]
        public virtual Bloco? Bloco { get; set; }

        [NotMapped]
        public decimal Medidas => Comprimento * Largura * Espessura;
    }

    public class ChapaCreateModel
    {
        [Required(ErrorMessage = "O ID do bloco é obrigatório")]
        public int BlocoId { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo de material deve ter no máximo 100 caracteres")]
        public string TipoMaterial { get; set; } = string.Empty;

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

        public bool Disponivel { get; set; } = true;

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior ou igual a zero")]
        public int QuantidadeEstoque { get; set; }

        [NotMapped]
        public decimal Medidas => Comprimento * Largura * Espessura;
    }

    public class ChapaResponseModel
    {
        public int Id { get; set; }
        public int BlocoId { get; set; }
        public string BlocoCodigo { get; set; } = string.Empty;
        public string TipoMaterial { get; set; } = string.Empty;
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Espessura { get; set; }
        public decimal ValorUnitario { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Disponivel { get; set; }
        public int QuantidadeEstoque { get; set; }
        public decimal Medidas { get; set; }

        public static ChapaResponseModel FromChapa(Chapa chapa)
        {
            return new ChapaResponseModel
            {
                Id = chapa.Id,
                BlocoId = chapa.BlocoId,
                BlocoCodigo = chapa.Bloco?.Codigo ?? string.Empty,
                TipoMaterial = chapa.TipoMaterial,
                Comprimento = chapa.Comprimento,
                Largura = chapa.Largura,
                Espessura = chapa.Espessura,
                ValorUnitario = chapa.ValorUnitario,
                DataCadastro = chapa.DataCadastro,
                Disponivel = chapa.Disponivel,
                QuantidadeEstoque = chapa.QuantidadeEstoque,
                Medidas = chapa.Medidas
            };
        }
    }
} 