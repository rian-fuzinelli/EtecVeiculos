using EtecVeiculos.Api.Data;
using EtecVeiculos.Api.DTOs;
using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoVeiculosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TipoVeiculosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TipoVeiculo>>> Get()
    {
        var tipos = await _context.TipoVeiculos.ToListAsync();
        return Ok(tipos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task <ActionResult<TipoVeiculo>> Get (int id)
    {
        var tipo = await _context.TipoVeiculos.FindAsync(id);
        if (tipo == null)
            return NotFound("Tipo de veículo não localizado!");
        return Ok(tipo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Create (TipoVeiculoVM tipoVeiculoVM)
    {
        if (ModelState.IsValid)
        {
            TipoVeiculo tipoVeiculo = new() {
                Name = tipoVeiculoVM.Name
            };
            await _context.AddAsync(tipoVeiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new{id = tipoVeiculo.Id});
        }
        return BadRequest("Verifique os dados informados");
    }  

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task <ActionResult> Edit(int id, TipoVeiculo tipoVeiculo)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.TipoVeiculos.Any(q => q.Id == id))
                    return NotFound("Tipo de Veículo não econtrado!");
                
                if (id != tipoVeiculo.Id)
                    return BadRequest("Verifique os dados informados");

                _context.Entry(tipoVeiculo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um problema: {ex.Message}");
            }
        }
        return BadRequest("Verifique os dados informados!");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task <ActionResult> Delete(int id)
    {
        try
        {
            var tipoVeiculo = await _context.TipoVeiculos.FirstOrDefaultAsync(q => q.Id == id);
            if (tipoVeiculo == null)
                return NotFound("Tipo de Veículo não encontrado");              
            
            _context.Remove(tipoVeiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um problema: {ex.Message}");
        }
    }
}