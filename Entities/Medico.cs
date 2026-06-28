using System;

namespace DispensarioMedico.Entities
{
    public class Medico
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Tanda_Labor { get; set; }
        public string Especialidad { get; set; }
        public bool Estado { get; set; }

        public Medico()
        {
            Nombre = string.Empty;
            Cedula = string.Empty;
            Tanda_Labor = string.Empty;
            Especialidad = string.Empty;
            Estado = true;
        }
    }
}
