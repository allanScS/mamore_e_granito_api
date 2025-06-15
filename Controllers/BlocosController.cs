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
    public class BlocosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlocosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bloco>>> GetBlocos()
        {
            try
            {
                return await _context.Blocos
                    .Include(b => b.Chapas)
                    .Include(b => b.ProcessosSerragem)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar blocos: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bloco>> GetBloco(int id)
        {
            try
            {
                var bloco = await _context.Blocos
                    .Include(b => b.Chapas)
                    .Include(b => b.ProcessosSerragem)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (bloco == null)
                    return NotFound(new { message = "Bloco não encontrado" });

                return bloco;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar bloco: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Bloco>> PostBloco(Bloco bloco)
        {
            try
            {
                if (await _context.Blocos.AnyAsync(b => b.Codigo == bloco.Codigo))
                    return BadRequest(new { message = "Código já cadastrado" });

                bloco.DataCadastro = DateTime.UtcNow;
                bloco.Disponivel = true;

                _context.Blocos.Add(bloco);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBloco), new { id = bloco.Id }, bloco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao criar bloco: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloco(int id, Bloco bloco)
        {
            try
            {
                if (id != bloco.Id)
                    return BadRequest(new { message = "ID inválido" });

                var blocoExistente = await _context.Blocos.FindAsync(id);
                if (blocoExistente == null)
                    return NotFound(new { message = "Bloco não encontrado" });

                if (blocoExistente.Codigo != bloco.Codigo && await _context.Blocos.AnyAsync(b => b.Codigo == bloco.Codigo))
                    return BadRequest(new { message = "Código já cadastrado" });

                blocoExistente.Codigo = bloco.Codigo;
                blocoExistente.PedreiraOrigem = bloco.PedreiraOrigem;
                blocoExistente.MetragemM3 = bloco.MetragemM3;
                blocoExistente.TipoMaterial = bloco.TipoMaterial;
                blocoExistente.ValorCompra = bloco.ValorCompra;
                blocoExistente.NotaFiscalEntrada = bloco.NotaFiscalEntrada;
                blocoExistente.Disponivel = bloco.Disponivel;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao atualizar bloco: {ex.Message}" });
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

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao excluir bloco: {ex.Message}" });
            }
        }
    }
} 