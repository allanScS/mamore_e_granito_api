using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarmoreGranito.API.Data;
using MarmoreGranito.API.Models;
using Microsoft.Extensions.Logging;

namespace MarmoreGranito.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChapasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChapasController> _logger;

        public ChapasController(ApplicationDbContext context, ILogger<ChapasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapaResponseModel>>> GetChapas()
        {
            try
            {
                _logger.LogInformation("Buscando todas as chapas");
                var chapas = await _context.Chapas
                    .Include(c => c.Bloco)
                    .Where(c => c.Disponivel)
                    .ToListAsync();

                return chapas.Select(ChapaResponseModel.FromChapa).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar chapas");
                return StatusCode(500, new { message = $"Erro ao buscar chapas: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapaResponseModel>> GetChapa(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando chapa com ID {id}");
                var chapa = await _context.Chapas
                    .Include(c => c.Bloco)
                    .FirstOrDefaultAsync(c => c.Id == id && c.Disponivel);

                if (chapa == null)
                {
                    _logger.LogWarning($"Chapa com ID {id} não encontrada");
                    return NotFound(new { message = "Chapa não encontrada" });
                }

                return ChapaResponseModel.FromChapa(chapa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar chapa com ID {id}");
                return StatusCode(500, new { message = $"Erro ao buscar chapa: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Chapa>> PostChapa(ChapaCreateModel model)
        {
            try
            {
                var bloco = await _context.Blocos.FindAsync(model.BlocoId);
                if (bloco == null)
                {
                    _logger.LogWarning($"Tentativa de criar chapa para bloco inexistente ID {model.BlocoId}");
                    return BadRequest(new { message = "Bloco não encontrado" });
                }

                if (!bloco.Disponivel)
                {
                    _logger.LogWarning($"Tentativa de criar chapa para bloco indisponível ID {model.BlocoId}");
                    return BadRequest(new { message = "Bloco não está disponível" });
                }

                var chapa = new Chapa
                {
                    BlocoId = model.BlocoId,
                    TipoMaterial = model.TipoMaterial,
                    Comprimento = model.Comprimento,
                    Largura = model.Largura,
                    Espessura = model.Espessura,
                    ValorUnitario = model.ValorUnitario,
                    DataCadastro = DateTime.UtcNow,
                    Disponivel = model.Disponivel,
                    QuantidadeEstoque = model.QuantidadeEstoque
                };

                _context.Chapas.Add(chapa);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Chapa criada com sucesso para o bloco {bloco.Codigo}");
                return CreatedAtAction(nameof(GetChapa), new { id = chapa.Id }, chapa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar chapa");
                return StatusCode(500, new { message = $"Erro ao criar chapa: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapa(int id, ChapaCreateModel model)
        {
            try
            {
                var chapaExistente = await _context.Chapas.FindAsync(id);
                if (chapaExistente == null)
                {
                    _logger.LogWarning($"Chapa com ID {id} não encontrada para atualização");
                    return NotFound(new { message = "Chapa não encontrada" });
                }

                var bloco = await _context.Blocos.FindAsync(model.BlocoId);
                if (bloco == null)
                {
                    _logger.LogWarning($"Tentativa de atualizar chapa com bloco inexistente ID {model.BlocoId}");
                    return BadRequest(new { message = "Bloco não encontrado" });
                }

                if (!bloco.Disponivel && chapaExistente.BlocoId != model.BlocoId)
                {
                    _logger.LogWarning($"Tentativa de atualizar chapa para bloco indisponível ID {model.BlocoId}");
                    return BadRequest(new { message = "Bloco não está disponível" });
                }

                chapaExistente.BlocoId = model.BlocoId;
                chapaExistente.TipoMaterial = model.TipoMaterial;
                chapaExistente.Comprimento = model.Comprimento;
                chapaExistente.Largura = model.Largura;
                chapaExistente.Espessura = model.Espessura;
                chapaExistente.ValorUnitario = model.ValorUnitario;
                chapaExistente.Disponivel = model.Disponivel;
                chapaExistente.QuantidadeEstoque = model.QuantidadeEstoque;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Chapa atualizada com sucesso: {id}");
                return Ok(chapaExistente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar chapa {id}");
                return StatusCode(500, new { message = $"Erro ao atualizar chapa: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapa(int id)
        {
            try
            {
                var chapa = await _context.Chapas.FindAsync(id);
                if (chapa == null)
                {
                    _logger.LogWarning($"Chapa com ID {id} não encontrada para exclusão");
                    return NotFound(new { message = "Chapa não encontrada" });
                }

                chapa.Disponivel = false;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Chapa desativada com sucesso: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir chapa {id}");
                return StatusCode(500, new { message = $"Erro ao excluir chapa: {ex.Message}" });
            }
        }
    }
} 