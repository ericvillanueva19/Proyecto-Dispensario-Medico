using System;

namespace DispensarioMedico.Entities
{
    public class Marca
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public Marca()
        {
            Descripcion = string.Empty;
            Estado = true;
        }
    }
}
