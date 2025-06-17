using System;
using System.Linq;
using MarmoreGranito.API.Models;
using MarmoreGranito.API.Services;
using Microsoft.EntityFrameworkCore;

namespace MarmoreGranito.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            // Seed Usuários
            if (!context.Usuarios.Any())
            {
                var adminUser = new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@admin.com",
                    CPF = "000.000.000-00",
                    Senha = PasswordHashService.HashPassword("admin123#"),
                    Cargo = "Administrador",
                    DataCadastro = DateTime.UtcNow,
                    Ativo = true
                };

                context.Usuarios.Add(adminUser);
                context.SaveChanges();
            }

            // Seed Blocos
            if (!context.Blocos.Any())
            {
                var blocos = new[]
                {
                    new Bloco
                    {
                        Codigo = "BL001",
                        PedreiraOrigem = "Pedreira São Thomé",
                        TipoMaterial = "Mármore Branco",
                        Largura = 180,
                        Altura = 160,
                        Comprimento = 300,
                        ValorCompra = 15000,
                        NotaFiscalEntrada = "NF-001",
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        Cerrado = false
                    },
                    new Bloco
                    {
                        Codigo = "BL002",
                        PedreiraOrigem = "Pedreira Cachoeiro",
                        TipoMaterial = "Granito Preto São Gabriel",
                        Largura = 190,
                        Altura = 170,
                        Comprimento = 310,
                        ValorCompra = 18000,
                        NotaFiscalEntrada = "NF-002",
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        Cerrado = false
                    },
                    new Bloco
                    {
                        Codigo = "BL003",
                        PedreiraOrigem = "Pedreira Rio Novo",
                        TipoMaterial = "Granito Amarelo Ornamental",
                        Largura = 175,
                        Altura = 155,
                        Comprimento = 290,
                        ValorCompra = 16500,
                        NotaFiscalEntrada = "NF-003",
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        Cerrado = false
                    },
                    new Bloco
                    {
                        Codigo = "BL004",
                        PedreiraOrigem = "Pedreira Monte Verde",
                        TipoMaterial = "Mármore Travertino",
                        Largura = 185,
                        Altura = 165,
                        Comprimento = 305,
                        ValorCompra = 17000,
                        NotaFiscalEntrada = "NF-004",
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        Cerrado = false
                    },
                    new Bloco
                    {
                        Codigo = "BL005",
                        PedreiraOrigem = "Pedreira Serra Alta",
                        TipoMaterial = "Granito Verde Ubatuba",
                        Largura = 195,
                        Altura = 175,
                        Comprimento = 315,
                        ValorCompra = 19000,
                        NotaFiscalEntrada = "NF-005",
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        Cerrado = false
                    }
                };

                context.Blocos.AddRange(blocos);
                context.SaveChanges();

                // Seed ProcessoSerragem
                var processosSerragem = new[]
                {
                    new ProcessoSerragem
                    {
                        BlocoId = blocos[0].Id,
                        DataInicio = DateTime.UtcNow.AddDays(-30),
                        QuantidadeChapas = 20,
                        Observacoes = "Processo de serragem padrão"
                    },
                    new ProcessoSerragem
                    {
                        BlocoId = blocos[1].Id,
                        DataInicio = DateTime.UtcNow.AddDays(-25),
                        QuantidadeChapas = 18,
                        Observacoes = "Serragem com espessura especial"
                    },
                    new ProcessoSerragem
                    {
                        BlocoId = blocos[2].Id,
                        DataInicio = DateTime.UtcNow.AddDays(-20),
                        QuantidadeChapas = 22,
                        Observacoes = "Serragem com acabamento polido"
                    },
                    new ProcessoSerragem
                    {
                        BlocoId = blocos[3].Id,
                        DataInicio = DateTime.UtcNow.AddDays(-15),
                        QuantidadeChapas = 25,
                        Observacoes = "Processo de serragem com reforço"
                    },
                    new ProcessoSerragem
                    {
                        BlocoId = blocos[4].Id,
                        DataInicio = DateTime.UtcNow.AddDays(-10),
                        QuantidadeChapas = 19,
                        Observacoes = "Serragem com espessura fina"
                    }
                };

                context.ProcessosSerragem.AddRange(processosSerragem);
                context.SaveChanges();

                // Seed Chapas
                var chapas = new[]
                {
                    new Chapa
                    {
                        BlocoId = blocos[0].Id,
                        TipoMaterial = blocos[0].TipoMaterial,
                        Comprimento = 300,
                        Largura = 180,
                        Espessura = 2,
                        ValorUnitario = 1200,
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        QuantidadeEstoque = 15
                    },
                    new Chapa
                    {
                        BlocoId = blocos[1].Id,
                        TipoMaterial = blocos[1].TipoMaterial,
                        Comprimento = 310,
                        Largura = 190,
                        Espessura = 2.5M,
                        ValorUnitario = 1500,
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        QuantidadeEstoque = 12
                    },
                    new Chapa
                    {
                        BlocoId = blocos[2].Id,
                        TipoMaterial = blocos[2].TipoMaterial,
                        Comprimento = 290,
                        Largura = 175,
                        Espessura = 3,
                        ValorUnitario = 1300,
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        QuantidadeEstoque = 18
                    },
                    new Chapa
                    {
                        BlocoId = blocos[3].Id,
                        TipoMaterial = blocos[3].TipoMaterial,
                        Comprimento = 305,
                        Largura = 185,
                        Espessura = 2,
                        ValorUnitario = 1400,
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        QuantidadeEstoque = 20
                    },
                    new Chapa
                    {
                        BlocoId = blocos[4].Id,
                        TipoMaterial = blocos[4].TipoMaterial,
                        Comprimento = 315,
                        Largura = 195,
                        Espessura = 2.5M,
                        ValorUnitario = 1600,
                        DataCadastro = DateTime.UtcNow,
                        Disponivel = true,
                        QuantidadeEstoque = 14
                    }
                };

                context.Chapas.AddRange(chapas);
                context.SaveChanges();
            }
        }
    }
} 