using EtecVeiculos.Api.Data;
using EtecVeiculos.Api.DTOs;
using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MarcasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Marca>>> Get()
    {
        var marcas = await _context.Marcas.ToListAsync();
        return Ok(marcas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task <ActionResult<Marca>> Get (int id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null)
            return NotFound("Marca não localizada!");
        return Ok(marca);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Create (MarcaVM marcaVM)
    {
        if (ModelState.IsValid)
        {
            Marca marca = new() {
                Nome = marcaVM.Name
            };
            await _context.AddAsync(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new{id = marca.Id});
        }
        return BadRequest("Verifique os dados informados");
    }  

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task <ActionResult> Edit(int id, Marca marca)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!_context.TipoVeiculos.Any(q => q.Id == id))
                    return NotFound("Marca não econtrada!");
                
                if (id != marca.Id)
                    return BadRequest("Verifique os dados informados");

                _context.Entry(marca).State = EntityState.Modified;
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
            var marca = await _context.TipoVeiculos.FirstOrDefaultAsync(q => q.Id == id);
            if (marca == null)
                return NotFound("Marca não encontrada");              
            
            _context.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um problema: {ex.Message}");
        }
    }
}