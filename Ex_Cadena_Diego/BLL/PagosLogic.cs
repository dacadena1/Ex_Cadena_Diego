using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class PagosLogic
    {
        public Pagos Create(Pagos pago)
        {
            Pagos _pago = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Verificar si el pago ya existe en base a la FacturaID
                Pagos _result = repository.Retrieve<Pagos>(p => p.FacturaID == pago.FacturaID);
                if (_result == null)
                {
                    // Crear el pago si no existe
                    _pago = repository.Create(pago);
                }
                else
                {
                    throw new Exception("El pago ya existe para esta factura");
                }
            }
            return _pago;
        }

        public Pagos RetrieveById(int id)
        {
            Pagos _pago = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Recuperar el pago por su PagoID
                _pago = repository.Retrieve<Pagos>(p => p.PagoID == id);
            }
            return _pago;
        }

        public bool Update(Pagos pago)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Recuperar el pago existente desde la base de datos
                var existingPago = repository.Retrieve<Pagos>(p => p.PagoID == pago.PagoID);
                if (existingPago == null)
                {
                    throw new Exception("El pago no existe");
                }

                // Actualizar los valores del pago existente
                existingPago.FacturaID = pago.FacturaID;
                existingPago.Monto = pago.Monto;
                existingPago.FechaPago = pago.FechaPago;
                existingPago.MetodoPago = pago.MetodoPago;

                // Guardar los cambios en la base de datos
                return repository.Update(existingPago);
            }
        }


        public bool Delete(int id)
        {
            bool _delete = false;
            var _pago = RetrieveById(id);
            if (_pago != null)
            {
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    // Eliminar el pago
                    _delete = repository.Delete(_pago);
                }
            }
            return _delete;
        }

        public List<Pagos> RetrieveAll()
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Usar una expresión lambda para obtener todos los proyectos
                return repository.Filter<Pagos>(p => p.PagoID > 0).ToList();
            }
        }
    }
}
