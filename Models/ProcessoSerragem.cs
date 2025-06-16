using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarmoreGranito.API.Models
{
    public class ProcessoSerragem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O bloco é obrigatório")]
        public int BlocoId { get; set; }

        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A quantidade de chapas é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de chapas deve ser maior que zero")]
        public int QuantidadeChapas { get; set; }

        public string Observacoes { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual Bloco Bloco { get; set; } = null!;
    }
} 