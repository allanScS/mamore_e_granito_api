using System;
using System.Linq;
using MarmoreGranito.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarmoreGranito.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.Usuarios.Any())
            {
                var adminUser = new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@admin.com",
                    CPF = "000.000.000-00",
                    Senha = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Cargo = "Administrador",
                    DataCadastro = DateTime.UtcNow,
                    Ativo = true
                };

                context.Usuarios.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
} 