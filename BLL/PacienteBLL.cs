using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class PacienteBLL
    {
        private PacienteDAL dal = new PacienteDAL();

        // Tipos de pacientes permitidos (Regla institucional)
        private readonly List<string> tiposPermitidos = new List<string>
        {
            "Estudiante",
            "Empleado",
            "Profesor",
            "Otros"
        };

        public void Guardar(Paciente paciente)
        {
            Validar(paciente);

            if (paciente.ID == 0)
            {
                if (dal.ExisteCedula(paciente.Cedula))
                {
                    throw new Exception("Ya existe un Paciente registrado con esta Cédula.");
                }
                dal.Insertar(paciente);
            }
            else
            {
                if (dal.ExisteCedula(paciente.Cedula, paciente.ID))
                {
                    throw new Exception("Ya existe otro Paciente registrado con esta Cédula.");
                }
                dal.Actualizar(paciente);
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

        public List<Paciente> Listar()
        {
            return dal.Listar();
        }

        private void Validar(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new Exception("La entidad Paciente no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(paciente.Nombre))
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(paciente.Cedula))
            {
                throw new Exception("La cédula no puede estar vacía.");
            }
            if (string.IsNullOrWhiteSpace(paciente.No_Carnet))
            {
                throw new Exception("El No. de Carnet no puede estar vacío.");
            }
            
            // Regla estricta: Validar que el campo 'TipoPaciente' corresponda únicamente a uno de los valores institucionales permitidos
            if (string.IsNullOrWhiteSpace(paciente.TipoPaciente) || !tiposPermitidos.Contains(paciente.TipoPaciente))
            {
                throw new Exception("El Tipo de Paciente debe ser uno de los permitidos: Estudiante, Empleado, Profesor u Otros.");
            }

            if (paciente.Nombre.Length > 150)
            {
                throw new Exception("El nombre no puede exceder los 150 caracteres.");
            }
            if (paciente.Cedula.Length > 20)
            {
                throw new Exception("La cédula no puede exceder los 20 caracteres.");
            }
            if (paciente.No_Carnet.Length > 50)
            {
                throw new Exception("El No. de Carnet no puede exceder los 50 caracteres.");
            }
        }
    }
}
