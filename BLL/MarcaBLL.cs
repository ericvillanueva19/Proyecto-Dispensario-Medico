using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class MarcaBLL
    {
        private MarcaDAL dal = new MarcaDAL();

        public void Guardar(Marca marca)
        {
            Validar(marca);

            if (marca.ID == 0)
            {
                if (dal.Existe(marca.Descripcion))
                {
                    throw new Exception("Ya existe una Marca con esta descripción.");
                }
                dal.Insertar(marca);
            }
            else
            {
                if (dal.Existe(marca.Descripcion, marca.ID))
                {
                    throw new Exception("Ya existe otra Marca con esta descripción.");
                }
                dal.Actualizar(marca);
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

        public List<Marca> Listar()
        {
            return dal.Listar();
        }

        private void Validar(Marca marca)
        {
            if (marca == null)
            {
                throw new Exception("La entidad Marca no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                throw new Exception("La descripción no puede estar vacía.");
            }
            if (marca.Descripcion.Length > 100)
            {
                throw new Exception("La descripción no puede exceder los 100 caracteres.");
            }
        }
    }
}
