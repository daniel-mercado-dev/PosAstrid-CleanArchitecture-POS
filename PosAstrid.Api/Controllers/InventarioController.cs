using Microsoft.AspNetCore.Mvc;
using PosAstrid.Api.DTOs;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;

namespace PosAstrid.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventarioController : ControllerBase
{
    private readonly IInventarioRepository _inventarios;
    private readonly IProductoRepository _productos;
    private readonly IUnitOfWork _unitOfWork;

    public InventarioController(IInventarioRepository inventarios, IProductoRepository productos, IUnitOfWork unitOfWork)
    {
        _inventarios = inventarios;
        _productos = productos;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(RegistrarInventarioRequest request, CancellationToken ct)
    {
        try
        {
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest(new { success = false, mensaje = "No se enviaron items." });

            var lima = TimeZoneInfo.FindSystemTimeZoneById("America/Lima");
            var hoyLima = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, lima).Date;
            var hoy = DateTime.SpecifyKind(hoyLima, DateTimeKind.Utc);

            foreach (var item in request.Items)
            {
                var producto = await _productos.ObtenerPorIdAsync(item.ProductoId, ct);
                if (producto is null)
                    return BadRequest(new { success = false, mensaje = $"El producto {item.ProductoId} no existe." });

                var existente = await _inventarios.ObtenerPorProductoYFechaAsync(item.ProductoId, hoy, ct);

                if (existente is not null)
                {
                    // MODO TARDE
                    existente.ActualizarStockFinal(item.StockFinal);
                    existente.ActualizarPrecio(item.PrecioUnitario);
                }
                else
                {
                    // MODO MAÑANA: si no mandan stock final, arranca igual al inicial
                    var stockFinal = item.StockFinal == 0 ? item.StockInicial : item.StockFinal;
                    var nuevo = Inventario.Crear(item.ProductoId, item.StockInicial, stockFinal, item.PrecioUnitario, hoy);
                    _inventarios.Agregar(nuevo);
                }
            }

            await _unitOfWork.SaveChangesAsync(ct);
            return Ok(new { success = true, mensaje = "Inventario actualizado correctamente." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, mensaje = ex.Message });
        }
    }
}