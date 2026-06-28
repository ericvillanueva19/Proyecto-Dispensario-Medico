using System;

namespace DispensarioMedico.Entities
{
    public class Ubicacion
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Estante { get; set; }
        public string Tramo { get; set; }
        public string Celda { get; set; }
        public bool Estado { get; set; }

        public Ubicacion()
        {
            Descripcion = string.Empty;
            Estante = string.Empty;
            Tramo = string.Empty;
            Celda = string.Empty;
            Estado = true;
        }
    }
}
