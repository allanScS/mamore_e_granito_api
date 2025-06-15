using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarmoreGranito.API.Data;
using MarmoreGranito.API.Models;

namespace MarmoreGranito.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChapasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChapasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapa>>> GetChapas()
        {
            try
            {
                return await _context.Chapas
                    .Include(c => c.Bloco)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar chapas: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chapa>> GetChapa(int id)
        {
            try
            {
                var chapa = await _context.Chapas
                    .Include(c => c.Bloco)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (chapa == null)
                    return NotFound(new { message = "Chapa não encontrada" });

                return chapa;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar chapa: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Chapa>> PostChapa(Chapa chapa)
        {
            try
            {
                var bloco = await _context.Blocos.FindAsync(chapa.BlocoId);
                if (bloco == null)
                    return BadRequest(new { message = "Bloco não encontrado" });

                if (!bloco.Disponivel)
                    return BadRequest(new { message = "Bloco não está disponível" });

                chapa.DataCadastro = DateTime.UtcNow;
                chapa.Disponivel = true;

                _context.Chapas.Add(chapa);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetChapa), new { id = chapa.Id }, chapa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao criar chapa: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapa(int id, Chapa chapa)
        {
            try
            {
                if (id != chapa.Id)
                    return BadRequest(new { message = "ID inválido" });

                var chapaExistente = await _context.Chapas.FindAsync(id);
                if (chapaExistente == null)
                    return NotFound(new { message = "Chapa não encontrada" });

                chapaExistente.BlocoId = chapa.BlocoId;
                chapaExistente.TipoMaterial = chapa.TipoMaterial;
                chapaExistente.Comprimento = chapa.Comprimento;
                chapaExistente.Largura = chapa.Largura;
                chapaExistente.Espessura = chapa.Espessura;
                chapaExistente.ValorUnitario = chapa.ValorUnitario;
                chapaExistente.Disponivel = chapa.Disponivel;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
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
                    return NotFound(new { message = "Chapa não encontrada" });

                chapa.Disponivel = false;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao excluir chapa: {ex.Message}" });
            }
        }
    }
} 