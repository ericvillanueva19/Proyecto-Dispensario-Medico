using System;

namespace DispensarioMedico.Entities
{
    public class TipoFarmaco
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public TipoFarmaco()
        {
            Descripcion = string.Empty;
            Estado = true;
        }
    }
}
