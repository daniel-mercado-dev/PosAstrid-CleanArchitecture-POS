using Microsoft.AspNetCore.Mvc;
using PosAstrid.Api.DTOs;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PosAstrid.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _productos;
    private readonly IUnitOfWork _unitOfWork;

    public ProductosController(IProductoRepository productos, IUnitOfWork unitOfWork)
    {
        _productos = productos;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearProductoRequest request, CancellationToken ct)
    {
        try
        {
           
            var producto = Producto.Crear(request.Nombre, request.PrecioBase, request.CategoriaId);

            _productos.Agregar(producto);
            await _unitOfWork.SaveChangesAsync(ct);

            return Ok(new { producto.Id, producto.Nombre, producto.PrecioBase, producto.CategoriaId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (DbUpdateException)
{
    return BadRequest(new { mensaje = "No se pudo guardar. Verifica que la categoría exista." });
}
    }

[HttpPut("{id}")]
public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoRequest request, CancellationToken ct)
{
    try
    {
        // 1. Buscamos el producto existente usando el método que ya tienes en tu repositorio
        var producto = await _productos.ObtenerPorIdAsync(id, ct);
        if (producto == null)
        {
            return NotFound(new { mensaje = "El producto no fue encontrado." });
        }

        // 2. Ejecutamos el método de negocio de la entidad (respeta las reglas de dominio)
        producto.ActualizarDatos(request.Nombre, request.PrecioBase, request.CategoriaId);

        // 3. Guardamos los cambios de forma atómica con Unit of Work
        await _unitOfWork.SaveChangesAsync(ct);

        return Ok(new { producto.Id, producto.Nombre, producto.PrecioBase, producto.CategoriaId });
    }
    catch (ArgumentException ex)
    {
        // Captura las validaciones que lanzaron las reglas de negocio de la entidad
        return BadRequest(new { mensaje = ex.Message });
    }
     catch (DbUpdateException)
    {
        return BadRequest(new { mensaje = "No se pudo guardar. Verifica que la categoría exista." });
    }
}

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var lista = await _productos.ObtenerTodosAsync(ct);
        return Ok(lista.Select(p => new { p.Id, p.Nombre, p.PrecioBase, p.CategoriaId }));
    }
}