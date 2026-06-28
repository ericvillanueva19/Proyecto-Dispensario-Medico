using System;

namespace DispensarioMedico.Entities
{
    public class Paciente
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string No_Carnet { get; set; }
        public string TipoPaciente { get; set; }
        public bool Estado { get; set; }

        public Paciente()
        {
            Nombre = string.Empty;
            Cedula = string.Empty;
            No_Carnet = string.Empty;
            TipoPaciente = string.Empty;
            Estado = true;
        }
    }
}
