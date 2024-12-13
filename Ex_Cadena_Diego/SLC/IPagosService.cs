using Entities;
using System;
using System.Collections.Generic;

namespace SLC
{
    public interface IPagosService
    {
        // Crear un pago
        Pagos CreatePago(Pagos pago);

        // Eliminar un pago por ID
        bool Delete(int id);

        // Obtener todos los pagos
        List<Pagos> GetAll();

        // Obtener un pago por ID
        Pagos GetById(int id);

        // Actualizar un pago
        bool UpdatePago(Pagos pago);
    }

    // Definición de la entidad Pagos
    public class Pagos
    {
        public int PagoID { get; set; }
        public int FacturaID { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
    }
}
