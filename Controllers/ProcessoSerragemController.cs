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
    [Route("api/processo-serragem")]
    public class ProcessoSerragemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProcessoSerragemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessoSerragem>>> GetProcessos()
        {
            try
            {
                return await _context.ProcessosSerragem
                    .Include(p => p.Bloco)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar processos de serragem: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessoSerragem>> GetProcesso(int id)
        {
            try
            {
                var processo = await _context.ProcessosSerragem
                    .Include(p => p.Bloco)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (processo == null)
                    return NotFound(new { message = "Processo de serragem não encontrado" });

                return processo;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar processo de serragem: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProcessoSerragem>> PostProcesso(ProcessoSerragem processo)
        {
            try
            {
                var bloco = await _context.Blocos.FindAsync(processo.BlocoId);
                if (bloco == null)
                    return BadRequest(new { message = "Bloco não encontrado" });

                if (!bloco.Disponivel)
                    return BadRequest(new { message = "Bloco não está disponível" });

                processo.DataInicio = DateTime.UtcNow;
                processo.Status = "Em Andamento";

                _context.ProcessosSerragem.Add(processo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProcesso), new { id = processo.Id }, processo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao criar processo de serragem: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcesso(int id, ProcessoSerragem processo)
        {
            try
            {
                if (id != processo.Id)
                    return BadRequest(new { message = "ID inválido" });

                var processoExistente = await _context.ProcessosSerragem.FindAsync(id);
                if (processoExistente == null)
                    return NotFound(new { message = "Processo de serragem não encontrado" });

                if (processo.Status == "Concluído" && processoExistente.Status != "Concluído")
                {
                    // Gerar chapas automaticamente
                    var bloco = await _context.Blocos.FindAsync(processoExistente.BlocoId);
                    if (bloco == null)
                        return BadRequest(new { message = "Bloco não encontrado" });

                    // Calcular dimensões das chapas baseado no bloco
                    decimal espessura = 0.02m; // 2cm padrão
                    decimal comprimento = (decimal)Math.Cbrt((double)(bloco.MetragemM3 * 2));
                    decimal largura = comprimento;

                    // Criar as chapas
                    for (int i = 0; i < processo.QuantidadeChapas; i++)
                    {
                        var chapa = new Chapa
                        {
                            BlocoId = bloco.Id,
                            TipoMaterial = bloco.TipoMaterial,
                            Comprimento = comprimento,
                            Largura = largura,
                            Espessura = espessura,
                            ValorUnitario = bloco.ValorCompra / processo.QuantidadeChapas,
                            DataCadastro = DateTime.UtcNow,
                            Disponivel = true
                        };

                        _context.Chapas.Add(chapa);
                    }

                    // Atualizar status do bloco
                    bloco.Disponivel = false;
                }

                processoExistente.QuantidadeChapas = processo.QuantidadeChapas;
                processoExistente.Observacoes = processo.Observacoes;
                processoExistente.Status = processo.Status;
                processoExistente.DataConclusao = processo.Status == "Concluído" ? DateTime.UtcNow : null;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao atualizar processo de serragem: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcesso(int id)
        {
            try
            {
                var processo = await _context.ProcessosSerragem.FindAsync(id);
                if (processo == null)
                    return NotFound(new { message = "Processo de serragem não encontrado" });

                if (processo.Status == "Concluído")
                    return BadRequest(new { message = "Não é possível excluir um processo concluído" });

                _context.ProcessosSerragem.Remove(processo);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao excluir processo de serragem: {ex.Message}" });
            }
        }
    }
} 