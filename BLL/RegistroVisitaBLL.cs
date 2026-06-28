using System;
using System.Collections.Generic;
using DispensarioMedico.Entities;
using DispensarioMedico.DAL;

namespace DispensarioMedico.BLL
{
    public class RegistroVisitaBLL
    {
        private RegistroVisitaDAL dal = new RegistroVisitaDAL();

        public void Guardar(RegistroVisita visita)
        {
            Validar(visita);
            dal.Insertar(visita);
        }

        public List<RegistroVisita> Consultar(int? medicoID, int? pacienteID, DateTime? fecha)
        {
            return dal.Consultar(medicoID, pacienteID, fecha);
        }

        private void Validar(RegistroVisita visita)
        {
            if (visita == null)
            {
                throw new Exception("La entidad Visita no puede ser nula.");
            }
            if (visita.MedicoID <= 0)
            {
                throw new Exception("Debe seleccionar un Médico válido.");
            }
            if (visita.PacienteID <= 0)
            {
                throw new Exception("Debe seleccionar un Paciente válido.");
            }
            if (visita.MedicamentoID <= 0)
            {
                throw new Exception("Debe seleccionar un Medicamento válido.");
            }
            if (string.IsNullOrWhiteSpace(visita.Sintomas))
            {
                throw new Exception("El campo Síntomas es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(visita.Recomendaciones))
            {
                throw new Exception("El campo Recomendaciones es obligatorio.");
            }
        }
    }
}
