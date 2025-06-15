using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MarmoreGranito.API.Models
{
    public class Bloco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório")]
        [StringLength(50, ErrorMessage = "O código deve ter no máximo 50 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A pedreira de origem é obrigatória")]
        [StringLength(100, ErrorMessage = "A pedreira de origem deve ter no máximo 100 caracteres")]
        public string PedreiraOrigem { get; set; }

        [Required(ErrorMessage = "A metragem é obrigatória")]
        [Range(0.001, double.MaxValue, ErrorMessage = "A metragem deve ser maior que zero")]
        public decimal MetragemM3 { get; set; }

        [Required(ErrorMessage = "O tipo de material é obrigatório")]
        [StringLength(100, ErrorMessage = "O tipo de material deve ter no máximo 100 caracteres")]
        public string TipoMaterial { get; set; }

        [Required(ErrorMessage = "O valor de compra é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de compra deve ser maior que zero")]
        public decimal ValorCompra { get; set; }

        [Required(ErrorMessage = "O número da nota fiscal é obrigatório")]
        [StringLength(50, ErrorMessage = "O número da nota fiscal deve ter no máximo 50 caracteres")]
        public string NotaFiscalEntrada { get; set; }

        public DateTime DataCadastro { get; set; }
        public bool Disponivel { get; set; }

        public virtual ICollection<Chapa> Chapas { get; set; }
        public virtual ICollection<ProcessoSerragem> ProcessosSerragem { get; set; }
    }
} 