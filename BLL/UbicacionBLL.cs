using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class UbicacionBLL
    {
        private UbicacionDAL dal = new UbicacionDAL();

        public void Guardar(Ubicacion ubicacion)
        {
            Validar(ubicacion);

            if (ubicacion.ID == 0)
            {
                if (dal.Existe(ubicacion.Descripcion))
                {
                    throw new Exception("Ya existe una Ubicación con esta descripción.");
                }
                dal.Insertar(ubicacion);
            }
            else
            {
                if (dal.Existe(ubicacion.Descripcion, ubicacion.ID))
                {
                    throw new Exception("Ya existe otra Ubicación con esta descripción.");
                }
                dal.Actualizar(ubicacion);
            }
        }

        public void Inactivar(int id)
        {
            if (id <= 0)
            {
                throw new Exception("El ID a inactivar no es válido.");
            }
            dal.Inactivar(id);
        }

        public List<Ubicacion> Listar()
        {
            return dal.Listar();
        }

        private void Validar(Ubicacion ubicacion)
        {
            if (ubicacion == null)
            {
                throw new Exception("La entidad Ubicación no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(ubicacion.Descripcion))
            {
                throw new Exception("La descripción no puede estar vacía.");
            }
            if (string.IsNullOrWhiteSpace(ubicacion.Estante))
            {
                throw new Exception("El campo 'Estante' no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(ubicacion.Tramo))
            {
                throw new Exception("El campo 'Tramo' no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(ubicacion.Celda))
            {
                throw new Exception("El campo 'Celda' no puede estar vacío.");
            }

            if (ubicacion.Descripcion.Length > 100)
            {
                throw new Exception("La descripción no puede exceder los 100 caracteres.");
            }
            if (ubicacion.Estante.Length > 50 || ubicacion.Tramo.Length > 50 || ubicacion.Celda.Length > 50)
            {
                throw new Exception("Los campos Estante, Tramo y Celda no pueden exceder los 50 caracteres.");
            }
        }
    }
}
