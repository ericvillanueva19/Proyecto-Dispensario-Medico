using DispensarioMedico.Entities;

namespace DispensarioMedico.UI
{
    /// <summary>
    /// Almacena la sesión activa del usuario en memoria durante la ejecución de la app.
    /// Se limpia al cerrar sesión para asegurar que no queden credenciales en RAM.
    /// </summary>
    public static class SesionActual
    {
        /// <summary>Usuario actualmente autenticado. Null si no hay sesión activa.</summary>
        public static Usuario Usuario { get; set; }

        /// <summary>
        /// Limpia completamente la sesión actual, eliminando la referencia del usuario
        /// y permitiendo que el GC libere el objeto de la RAM.
        /// </summary>
        public static void Limpiar()
        {
            Usuario = null;
        }
    }
}
