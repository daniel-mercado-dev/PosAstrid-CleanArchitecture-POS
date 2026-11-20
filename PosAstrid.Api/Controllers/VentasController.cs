using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosAstrid.Api.DTOs;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;

namespace PosAstrid.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentaRepository _ventas;
    private readonly IUnitOfWork _unitOfWork;

    public VentasController(IVentaRepository ventas, IUnitOfWork unitOfWork)
    {
        _ventas = ventas;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarVentaRequest request, CancellationToken ct)
    {
        try
        {
            if (request.Items is null || request.Items.Count == 0)
                return BadRequest(new { success = false, mensaje = "La venta no tiene items." });

            var venta = VentaCabecera.Crear(request.UsuarioId, request.MetodoPago, request.ClienteId);

            foreach (var item in request.Items)
                venta.AgregarDetalle(item.ProductoId, item.Producto, item.Cantidad, item.Precio);

            await _ventas.AddAsync(venta, ct);
            await _unitOfWork.SaveChangesAsync(ct);   // EF asigna el Id

            venta.MarcarRegistrada();                 // evento con Id y total reales
            await _unitOfWork.SaveChangesAsync(ct);   // publica el evento al handler

            return Ok(new { success = true, turno = venta.Id, total = venta.MontoTotal });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, mensaje = ex.Message });
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { success = false, mensaje = "No se pudo guardar. Verifica que el usuario, cliente o producto existan." });
        }
    }

[HttpGet]
public async Task<IActionResult> ObtenerTodas(CancellationToken ct)
{
    var ventas = await _ventas.GetAllAsync(ct);

    var resultado = ventas.Select(v => new
    {
        v.Id,
        v.UsuarioId,
        v.ClienteId,
        v.MetodoPago,
        v.MontoTotal,
        Items = v.Detalles.Select(d => new
        {
            d.Id,
            d.ProductoId,
            d.Producto,
            d.Cantidad,
            d.PrecioUnitario
        })
    });

    return Ok(resultado);
}

[HttpGet("{id:int}")]
public async Task<IActionResult> ObtenerPorId(int id, CancellationToken ct)
{
    var venta = await _ventas.GetByIdAsync(id, ct);

    if (venta is null)
        return NotFound(new { success = false, mensaje = $"No se encontró la venta con ID {id}." });

    return Ok(new
    {
        venta.Id,
        venta.UsuarioId,
        venta.ClienteId,
        venta.MetodoPago,
        venta.MontoTotal,
        Items = venta.Detalles.Select(d => new
        {
            d.Id,
            d.ProductoId,
            d.Producto,
            d.Cantidad,
            d.PrecioUnitario
        })
    });
}

}