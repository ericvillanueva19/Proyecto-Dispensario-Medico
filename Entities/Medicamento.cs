using System;

namespace DispensarioMedico.Entities
{
    public class Medicamento
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        
        public int TipoFarmacoID { get; set; }
        public string TipoFarmacoDescripcion { get; set; } // Para mostrar en el Grid

        public int MarcaID { get; set; }
        public string MarcaDescripcion { get; set; } // Para mostrar en el Grid

        public int UbicacionID { get; set; }
        public string UbicacionDescripcion { get; set; } // Para mostrar en el Grid

        public string Dosis { get; set; }
        public bool Estado { get; set; }

        public Medicamento()
        {
            Descripcion = string.Empty;
            TipoFarmacoDescripcion = string.Empty;
            MarcaDescripcion = string.Empty;
            UbicacionDescripcion = string.Empty;
            Dosis = string.Empty;
            Estado = true;
        }
    }
}
