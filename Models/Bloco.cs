using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarmoreGranito.API.Models
{
    public class Bloco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório")]
        [StringLength(50, ErrorMessage = "O código deve ter no máximo 50 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A pedreira de origem é obrigatória")]
        [StringLength(100, ErrorMessage = "A pedreira de origem deve ter no máximo 100 caracteres")]
        public string PedreiraOrigem { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo de material deve ter no máximo 100 caracteres")]
        public string TipoMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "A largura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A altura deve ser maior que zero")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "O valor de compra é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de compra deve ser maior que zero")]
        public decimal ValorCompra { get; set; }

        [Required(ErrorMessage = "O número da nota fiscal é obrigatório")]
        [StringLength(50, ErrorMessage = "O número da nota fiscal deve ter no máximo 50 caracteres")]
        public string NotaFiscalEntrada { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
        public bool Disponivel { get; set; }
        public bool Cerrado { get; set; }

        [JsonIgnore]
        public virtual ICollection<Chapa> Chapas { get; set; } = new List<Chapa>();
        [JsonIgnore]
        public virtual ICollection<ProcessoSerragem> ProcessosSerragem { get; set; } = new List<ProcessoSerragem>();

        [NotMapped]
        public decimal MetragemM3 => (Largura * Altura * Comprimento) / 1000000; // Convertendo de cm³ para m³
    }

    public class BlocoCreateModel
    {
        [Required(ErrorMessage = "O código é obrigatório")]
        [StringLength(50, ErrorMessage = "O código deve ter no máximo 50 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A pedreira de origem é obrigatória")]
        [StringLength(100, ErrorMessage = "A pedreira de origem deve ter no máximo 100 caracteres")]
        public string PedreiraOrigem { get; set; } = string.Empty;

        [Required(ErrorMessage = "A largura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero")]
        public decimal Largura { get; set; }

        [Required(ErrorMessage = "A altura é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A altura deve ser maior que zero")]
        public decimal Altura { get; set; }

        [Required(ErrorMessage = "O comprimento é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero")]
        public decimal Comprimento { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo de material deve ter no máximo 100 caracteres")]
        public string TipoMaterial { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor de compra é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de compra deve ser maior que zero")]
        public decimal ValorCompra { get; set; }

        [Required(ErrorMessage = "O número da nota fiscal é obrigatório")]
        [StringLength(50, ErrorMessage = "O número da nota fiscal deve ter no máximo 50 caracteres")]
        public string NotaFiscalEntrada { get; set; } = string.Empty;
    }
} 