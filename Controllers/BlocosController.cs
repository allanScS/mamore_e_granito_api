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
    public class BlocosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BlocosController> _logger;

        public BlocosController(ApplicationDbContext context, ILogger<BlocosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bloco>>> GetBlocos()
        {
            try
            {
                _logger.LogInformation("Buscando todos os blocos disponíveis");
                return await _context.Blocos
                    .Where(b => b.Disponivel)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar blocos");
                return StatusCode(500, new { message = $"Erro ao buscar blocos: {ex.Message}" });
            }
        }

        [HttpGet("nao-cerrados")]
        public async Task<ActionResult<IEnumerable<Bloco>>> GetBlocosNaoCerrados()
        {
            try
            {
                _logger.LogInformation("Buscando todos os blocos disponíveis não cerrados");
                return await _context.Blocos
                    .Where(b => b.Disponivel && !b.Cerrado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar blocos não cerrados");
                return StatusCode(500, new { message = $"Erro ao buscar blocos não cerrados: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bloco>> GetBloco(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando bloco com ID {id}");
                var bloco = await _context.Blocos
                    .FirstOrDefaultAsync(b => b.Id == id && b.Disponivel);

                if (bloco == null)
                {
                    _logger.LogWarning($"Bloco com ID {id} não encontrado ou indisponível");
                    return NotFound(new { message = "Bloco não encontrado" });
                }

                return bloco;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar bloco com ID {id}");
                return StatusCode(500, new { message = $"Erro ao buscar bloco: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Bloco>> PostBloco(BlocoCreateModel model)
        {
            try
            {
                if (await _context.Blocos.AnyAsync(b => b.Codigo == model.Codigo))
                    return BadRequest(new { message = "Código já cadastrado" });

                var bloco = new Bloco
                {
                    Codigo = model.Codigo,
                    PedreiraOrigem = model.PedreiraOrigem,
                    Largura = model.Largura,
                    Altura = model.Altura,
                    Comprimento = model.Comprimento,
                    TipoMaterial = model.TipoMaterial,
                    ValorCompra = model.ValorCompra,
                    NotaFiscalEntrada = model.NotaFiscalEntrada,
                    DataCadastro = DateTime.UtcNow,
                    Disponivel = true,
                    Cerrado = false
                };

                _context.Blocos.Add(bloco);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Bloco criado com sucesso: {bloco.Codigo}");
                return CreatedAtAction(nameof(GetBloco), new { id = bloco.Id }, bloco);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar bloco");
                return StatusCode(500, new { message = $"Erro ao criar bloco: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Bloco>> PutBloco(int id, BlocoCreateModel model)
        {
            try
            {
                var blocoExistente = await _context.Blocos.FindAsync(id);
                if (blocoExistente == null)
                {
                    _logger.LogWarning($"Bloco com ID {id} não encontrado para atualização");
                    return NotFound(new { message = "Bloco não encontrado" });
                }

                if (blocoExistente.Codigo != model.Codigo && await _context.Blocos.AnyAsync(b => b.Codigo == model.Codigo))
                {
                    _logger.LogWarning($"Tentativa de atualizar bloco com código já existente: {model.Codigo}");
                    return BadRequest(new { message = "Código já cadastrado" });
                }

                blocoExistente.Codigo = model.Codigo;
                blocoExistente.PedreiraOrigem = model.PedreiraOrigem;
                blocoExistente.Largura = model.Largura;
                blocoExistente.Altura = model.Altura;
                blocoExistente.Comprimento = model.Comprimento;
                blocoExistente.TipoMaterial = model.TipoMaterial;
                blocoExistente.ValorCompra = model.ValorCompra;
                blocoExistente.NotaFiscalEntrada = model.NotaFiscalEntrada;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Bloco atualizado com sucesso: {blocoExistente.Codigo}");
                return Ok(blocoExistente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar bloco {id}");
                return StatusCode(500, new { message = $"Erro ao atualizar bloco: {ex.Message}" });
            }
        }

        [HttpPut("{id}/marcar-como-cerrado")]
        public async Task<ActionResult<Bloco>> MarcarComoCerrado(int id)
        {
            try
            {
                var bloco = await _context.Blocos.FindAsync(id);
                if (bloco == null)
                {
                    _logger.LogWarning($"Bloco com ID {id} não encontrado");
                    return NotFound(new { message = "Bloco não encontrado" });
                }

                if (bloco.Cerrado)
                {
                    _logger.LogWarning($"Bloco com ID {id} já está marcado como cerrado");
                    return BadRequest(new { message = "Bloco já está marcado como cerrado" });
                }

                bloco.Cerrado = true;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Bloco {bloco.Codigo} marcado como cerrado com sucesso");
                return Ok(bloco);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao marcar bloco {id} como cerrado");
                return StatusCode(500, new { message = $"Erro ao marcar bloco como cerrado: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloco(int id)
        {
            try
            {
                var bloco = await _context.Blocos.FindAsync(id);
                if (bloco == null)
                    return NotFound(new { message = "Bloco não encontrado" });

                bloco.Disponivel = false;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Bloco desativado com sucesso: {bloco.Codigo}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir bloco {id}");
                return StatusCode(500, new { message = $"Erro ao excluir bloco: {ex.Message}" });
            }
        }
    }
} 