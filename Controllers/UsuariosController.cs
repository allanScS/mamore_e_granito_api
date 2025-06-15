using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarmoreGranito.API.Data;
using MarmoreGranito.API.Models;
using MarmoreGranito.API.Services;
using Microsoft.Extensions.Logging;

namespace MarmoreGranito.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ApplicationDbContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios(
            [FromQuery] string nome = null,
            [FromQuery] string email = null,
            [FromQuery] string cargo = null,
            [FromQuery] string cpf = null)
        {
            try
            {
                _logger.LogInformation("Buscando usuários com filtros - Nome: {Nome}, Email: {Email}, Cargo: {Cargo}, CPF: {CPF}",
                    nome ?? "null", email ?? "null", cargo ?? "null", cpf ?? "null");

                var query = _context.Usuarios.AsQueryable();

                if (!string.IsNullOrEmpty(nome))
                    query = query.Where(u => u.Nome.ToLower().Contains(nome.ToLower()));

                if (!string.IsNullOrEmpty(email))
                    query = query.Where(u => u.Email.ToLower().Contains(email.ToLower()));

                if (!string.IsNullOrEmpty(cargo))
                    query = query.Where(u => u.Cargo.ToLower().Contains(cargo.ToLower()));

                if (!string.IsNullOrEmpty(cpf))
                    query = query.Where(u => u.CPF.Replace(".", "").Replace("-", "").Contains(cpf.Replace(".", "").Replace("-", "")));

                var usuarios = await query
                    .Select(u => new Usuario
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Email = u.Email,
                        CPF = u.CPF,
                        Cargo = u.Cargo,
                        Telefone = u.Telefone,
                        DataCriacao = u.DataCriacao,
                        DataUltimaAtualizacao = u.DataUltimaAtualizacao,
                        Ativo = u.Ativo
                    })
                    .ToListAsync();

                _logger.LogInformation("Usuários encontrados: {Count}", usuarios.Count);
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários");
                return StatusCode(500, new { message = $"Erro ao buscar usuários: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                _logger.LogInformation("Buscando usuário com ID: {Id}", id);

                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    _logger.LogWarning("Usuário não encontrado com ID: {Id}", id);
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                usuario.Senha = null;
                _logger.LogInformation("Usuário encontrado com ID: {Id}", id);

                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário com ID: {Id}", id);
                return StatusCode(500, new { message = $"Erro ao buscar usuário: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioCreateModel model)
        {
            try
            {
                _logger.LogInformation("Criando novo usuário com email: {Email}", model.Email);

                if (await _context.Usuarios.AnyAsync(u => u.Email == model.Email))
                {
                    _logger.LogWarning("Tentativa de criar usuário com email já existente: {Email}", model.Email);
                    return BadRequest(new { message = "E-mail já cadastrado" });
                }

                var usuario = new Usuario
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    CPF = model.CPF,
                    Senha = PasswordHashService.HashPassword(model.Senha),
                    Telefone = model.Telefone,
                    Cargo = model.Cargo,
                    DataCriacao = DateTime.UtcNow,
                    DataUltimaAtualizacao = DateTime.UtcNow,
                    Ativo = true
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Usuário criado com sucesso. ID: {Id}", usuario.Id);

                // Retorna o usuário criado (sem a senha)
                var usuarioRetorno = new Usuario
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    CPF = usuario.CPF,
                    Telefone = usuario.Telefone,
                    Cargo = usuario.Cargo,
                    DataCriacao = usuario.DataCriacao,
                    DataUltimaAtualizacao = usuario.DataUltimaAtualizacao,
                    Ativo = usuario.Ativo
                };

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioRetorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário com email: {Email}", model.Email);
                return StatusCode(500, new { message = $"Erro ao criar usuário: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> PutUsuario(int id, UsuarioUpdateModel usuario)
        {
            try
            {
                _logger.LogInformation("Atualizando usuário com ID: {Id}", id);

                if (id != usuario.Id)
                {
                    _logger.LogWarning("ID inválido na atualização de usuário. ID da rota: {RouteId}, ID do usuário: {UserId}", id, usuario.Id);
                    return BadRequest(new { message = "ID inválido" });
                }

                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    _logger.LogWarning("Usuário não encontrado para atualização. ID: {Id}", id);
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                // Verifica se o email já está em uso por outro usuário
                if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email && u.Id != id))
                {
                    _logger.LogWarning("Tentativa de atualizar usuário com email já existente. Email: {Email}", usuario.Email);
                    return BadRequest(new { message = "E-mail já está em uso por outro usuário" });
                }

                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.CPF = usuario.CPF;
                usuarioExistente.Telefone = usuario.Telefone;
                usuarioExistente.Cargo = usuario.Cargo;
                usuarioExistente.Ativo = usuario.Ativo;
                usuarioExistente.DataUltimaAtualizacao = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuário atualizado com sucesso. ID: {Id}", id);

                // Retorna o usuário atualizado
                var usuarioAtualizado = new Usuario
                {
                    Id = usuarioExistente.Id,
                    Nome = usuarioExistente.Nome,
                    Email = usuarioExistente.Email,
                    CPF = usuarioExistente.CPF,
                    Telefone = usuarioExistente.Telefone,
                    Cargo = usuarioExistente.Cargo,
                    DataCriacao = usuarioExistente.DataCriacao,
                    DataUltimaAtualizacao = usuarioExistente.DataUltimaAtualizacao,
                    Ativo = usuarioExistente.Ativo
                };

                return Ok(usuarioAtualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário com ID: {Id}", id);
                return StatusCode(500, new { message = $"Erro ao atualizar usuário: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                _logger.LogInformation("Desativando usuário com ID: {Id}", id);

                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    _logger.LogWarning("Usuário não encontrado para desativação. ID: {Id}", id);
                    return NotFound(new { message = "Usuário não encontrado" });
                }

                usuario.Ativo = false;
                usuario.DataUltimaAtualizacao = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Usuário desativado com sucesso. ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao desativar usuário com ID: {Id}", id);
                return StatusCode(500, new { message = $"Erro ao excluir usuário: {ex.Message}" });
            }
        }
    }
} 