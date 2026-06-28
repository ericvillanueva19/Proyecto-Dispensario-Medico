using System;

namespace DispensarioMedico.Entities
{
    public class RegistroVisita
    {
        public int ID_Visita { get; set; }
        public int MedicoID { get; set; }
        public string MedicoNombre { get; set; } // Propiedad auxiliar para UI
        public int PacienteID { get; set; }
        public string PacienteNombre { get; set; } // Propiedad auxiliar para UI
        public DateTime Fecha_Visita { get; set; }
        public TimeSpan Hora_Visita { get; set; }
        public string Sintomas { get; set; }
        public int MedicamentoID { get; set; }
        public string MedicamentoDescripcion { get; set; } // Propiedad auxiliar para UI
        public string Recomendaciones { get; set; }
        public bool Estado { get; set; }

        public RegistroVisita()
        {
            Sintomas = string.Empty;
            Recomendaciones = string.Empty;
            Estado = true;
            Fecha_Visita = DateTime.Today;
            Hora_Visita = DateTime.Now.TimeOfDay;
        }
    }
}
