using PosAstrid.Domain.Abstractions;
namespace PosAstrid.Domain.Entities
{
    public class CajaTurno : Entity
    {
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }
        public DateTime FechaApertura { get; private set; } = DateTime.UtcNow;
        public DateTime? FechaCierre { get; private set; }
        public decimal MontoApertura { get; private set; }
        public decimal? MontoCierreReal { get; private set; }
        public decimal TotalVentasEfectivo { get; private set; }
        public string Estado { get; private set; } = "Abierto"; // Abierto, Cerrado

        public CajaTurno(int usuarioId, decimal montoApertura)
        {
            UsuarioId = usuarioId;
            MontoApertura = montoApertura;
            FechaApertura = DateTime.UtcNow;
            TotalVentasEfectivo = 0;
            Estado = "Abierto";
        }

        protected CajaTurno() { }

        // Método para acumular ventas en efectivo mientras la caja esté abierta
        public void RegistrarVentaEfectivo(decimal monto)
        {
            if (Estado != "Abierto")
                throw new InvalidOperationException("No se pueden registrar ventas porque la caja se encuentra cerrada.");

            TotalVentasEfectivo += monto;
        }

        // Método para cerrar el turno de caja de forma segura
        public void CerrarCaja(decimal montoCierreReal)
        {
            if (Estado == "Cerrado")
                throw new InvalidOperationException("Esta caja ya ha sido cerrada.");

            MontoCierreReal = montoCierreReal;
            FechaCierre = DateTime.UtcNow;
            Estado = "Cerrado";
        }
    }
}