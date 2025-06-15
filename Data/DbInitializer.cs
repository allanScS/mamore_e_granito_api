using System;
using System.Linq;
using MarmoreGranito.API.Models;
using MarmoreGranito.API.Services;

namespace MarmoreGranito.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Verificar se já existem usuários
            if (context.Usuarios.Any())
            {
                // Atualizar a senha do admin se necessário
                var admin = context.Usuarios.FirstOrDefault(u => u.Email == "admin@admin.com");
                if (admin != null && !PasswordHashService.VerifyPassword("admin123#", admin.Senha))
                {
                    admin.Senha = PasswordHashService.HashPassword("admin123#");
                    context.SaveChanges();
                }
                return;
            }

            // Criar usuário administrador
            var novoAdmin = new Usuario
            {
                Nome = "Administrador",
                Email = "admin@admin.com",
                CPF = "000.000.000-00",
                Senha = PasswordHashService.HashPassword("admin123#"),
                DataCriacao = DateTime.UtcNow,
                DataUltimaAtualizacao = DateTime.UtcNow,
                Ativo = true
            };

            context.Usuarios.Add(novoAdmin);
            context.SaveChanges();
        }
    }
} 