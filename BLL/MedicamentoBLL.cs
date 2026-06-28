using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class MedicamentoBLL
    {
        private MedicamentoDAL dal = new MedicamentoDAL();

        public void Guardar(Medicamento med)
        {
            Validar(med);

            if (med.ID == 0)
            {
                if (dal.Existe(med.Descripcion))
                {
                    throw new Exception("Ya existe un medicamento con esta descripción.");
                }
                dal.Insertar(med);
            }
            else
            {
                if (dal.Existe(med.Descripcion, med.ID))
                {
                    throw new Exception("Ya existe otro medicamento con esta descripción.");
                }
                dal.Actualizar(med);
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

        public List<Medicamento> Listar()
        {
            return dal.Listar();
        }

        private void Validar(Medicamento med)
        {
            if (med == null)
            {
                throw new Exception("La entidad Medicamento no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(med.Descripcion))
            {
                throw new Exception("La descripción no puede estar vacía.");
            }
            if (med.Descripcion.Length > 150)
            {
                throw new Exception("La descripción no puede exceder los 150 caracteres.");
            }
            if (string.IsNullOrWhiteSpace(med.Dosis))
            {
                throw new Exception("La dosis no puede estar vacía.");
            }
            if (med.Dosis.Length > 100)
            {
                throw new Exception("La dosis no puede exceder los 100 caracteres.");
            }
            if (med.TipoFarmacoID <= 0)
            {
                throw new Exception("Debe seleccionar un Tipo de Fármaco válido.");
            }
            if (med.MarcaID <= 0)
            {
                throw new Exception("Debe seleccionar una Marca válida.");
            }
            if (med.UbicacionID <= 0)
            {
                throw new Exception("Debe seleccionar una Ubicación válida.");
            }
        }
    }
}
