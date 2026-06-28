using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class MedicoBLL
    {
        private MedicoDAL dal = new MedicoDAL();

        public void Guardar(Medico medico)
        {
            Validar(medico);

            if (medico.ID == 0)
            {
                if (dal.ExisteCedula(medico.Cedula))
                {
                    throw new Exception("Ya existe un Médico registrado con esta Cédula.");
                }
                dal.Insertar(medico);
            }
            else
            {
                if (dal.ExisteCedula(medico.Cedula, medico.ID))
                {
                    throw new Exception("Ya existe otro Médico registrado con esta Cédula.");
                }
                dal.Actualizar(medico);
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

        public List<Medico> Listar()
        {
            return dal.Listar();
        }

        private void Validar(Medico medico)
        {
            if (medico == null)
            {
                throw new Exception("La entidad Médico no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(medico.Nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(medico.Cedula))
            {
                throw new Exception("La cédula no puede estar vacía.");
            }
            if (string.IsNullOrWhiteSpace(medico.Tanda_Labor))
            {
                throw new Exception("Debe seleccionar una tanda laboral.");
            }
            if (string.IsNullOrWhiteSpace(medico.Especialidad))
            {
                throw new Exception("La especialidad no puede estar vacía.");
            }

            if (medico.Nombre.Length > 150)
            {
                throw new Exception("El nombre no puede exceder los 150 caracteres.");
            }
            if (medico.Cedula.Length > 20)
            {
                throw new Exception("La cédula no puede exceder los 20 caracteres.");
            }
            if (medico.Especialidad.Length > 100)
            {
                throw new Exception("La especialidad no puede exceder los 100 caracteres.");
            }
            if (medico.Tanda_Labor.Length > 50)
            {
                throw new Exception("La tanda laboral no puede exceder los 50 caracteres.");
            }
        }
    }
}
