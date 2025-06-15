using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarmoreGranito.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato: 000.000.000-00")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(255, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
        [JsonIgnore]
        public string Senha { get; set; }

        [Phone(ErrorMessage = "Telefone inválido")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
        public string? Telefone { get; set; }

        [StringLength(50, ErrorMessage = "O cargo deve ter no máximo 50 caracteres")]
        public string? Cargo { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
    }

    public class UsuarioCreateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato: 000.000.000-00")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(255, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Phone(ErrorMessage = "Telefone inválido")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
        public string? Telefone { get; set; }

        [StringLength(50, ErrorMessage = "O cargo deve ter no máximo 50 caracteres")]
        public string? Cargo { get; set; }
    }

    public class UsuarioUpdateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato: 000.000.000-00")]
        public string CPF { get; set; }

        [Phone(ErrorMessage = "Telefone inválido")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
        public string? Telefone { get; set; }

        [StringLength(50, ErrorMessage = "O cargo deve ter no máximo 50 caracteres")]
        public string? Cargo { get; set; }

        public bool Ativo { get; set; }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }
} 