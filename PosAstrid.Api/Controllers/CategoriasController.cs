using Microsoft.AspNetCore.Mvc;
using PosAstrid.Api.DTOs;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;

namespace PosAstrid.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _categorias;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriasController(ICategoriaRepository categorias, IUnitOfWork unitOfWork)
    {
        _categorias = categorias;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var lista = await _categorias.ObtenerTodasAsync(ct);
        return Ok(lista.Select(c => new { c.Id, c.Nombre, c.Activo }));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearCategoriaRequest request, CancellationToken ct)
    {
        try
        {
            var categoria = Categoria.Crear(request.Nombre);
            _categorias.Agregar(categoria);
            await _unitOfWork.SaveChangesAsync(ct);

            return Ok(new { categoria.Id, categoria.Nombre, categoria.Activo });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
