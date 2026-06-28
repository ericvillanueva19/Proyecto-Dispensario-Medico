using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class TipoFarmacoBLL
    {
        private TipoFarmacoDAL dal = new TipoFarmacoDAL();

        public void Guardar(TipoFarmaco tipoFarmaco)
        {
            Validar(tipoFarmaco);

            if (tipoFarmaco.ID == 0)
            {
                if (dal.Existe(tipoFarmaco.Descripcion))
                {
                    throw new Exception("Ya existe un tipo de fármaco con esta descripción.");
                }
                dal.Insertar(tipoFarmaco);
            }
            else
            {
                if (dal.Existe(tipoFarmaco.Descripcion, tipoFarmaco.ID))
                {
                    throw new Exception("Ya existe otro tipo de fármaco con esta descripción.");
                }
                dal.Actualizar(tipoFarmaco);
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

        public List<TipoFarmaco> Listar()
        {
            return dal.Listar();
        }

        private void Validar(TipoFarmaco tipoFarmaco)
        {
            if (tipoFarmaco == null)
            {
                throw new Exception("La entidad TipoFarmaco no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(tipoFarmaco.Descripcion))
            {
                throw new Exception("La descripción no puede estar vacía.");
            }
            if (tipoFarmaco.Descripcion.Length > 100)
            {
                throw new Exception("La descripción no puede exceder los 100 caracteres.");
            }
        }
    }
}
