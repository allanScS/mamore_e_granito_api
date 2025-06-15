using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarmoreGranito.API.Data;
using MarmoreGranito.API.Models;
using MarmoreGranito.API.Services;
using Microsoft.Extensions.Logging;

namespace MarmoreGranito.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation($"Tentativa de login para o email: {request.Email}");

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (usuario == null)
                {
                    _logger.LogWarning($"Usuário não encontrado para o email: {request.Email}");
                    return Unauthorized(new { message = "Usuário ou senha inválidos" });
                }

                if (!PasswordHashService.VerifyPassword(request.Senha, usuario.Senha))
                {
                    _logger.LogWarning($"Senha inválida para o usuário: {request.Email}");
                    return Unauthorized(new { message = "Usuário ou senha inválidos" });
                }

                if (!usuario.Ativo)
                {
                    _logger.LogWarning($"Tentativa de login de usuário inativo: {request.Email}");
                    return Unauthorized(new { message = "Usuário inativo" });
                }

                var token = TokenService.GenerateToken(usuario);

                _logger.LogInformation($"Login bem-sucedido para o usuário: {request.Email}");

                return new LoginResponse
                {
                    Token = token,
                    Email = usuario.Email,
                    Nome = usuario.Nome
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao fazer login: {ex.Message}");
                return StatusCode(500, new { message = $"Erro ao fazer login: {ex.Message}" });
            }
        }
    }
} 