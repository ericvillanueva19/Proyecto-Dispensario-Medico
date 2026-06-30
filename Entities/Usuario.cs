using System;

namespace DispensarioMedico.Entities
{
    /// <summary>
    /// Entidad que representa un usuario del sistema con sus credenciales y rol.
    /// </summary>
    public class Usuario
    {
        /// <summary>Correo electrónico institucional del usuario (usado como identificador único de login).</summary>
        public string Correo { get; set; }

        /// <summary>
        /// Hash de la contraseña del usuario.
        /// NOTA: En producción usar BCrypt o PBKDF2. Aquí se simula con SHA-256.
        /// </summary>
        public string ContrasenaHash { get; set; }

        /// <summary>Nombre completo del usuario.</summary>
        public string Nombre { get; set; }

        /// <summary>Rol del usuario: "admin" o "cliente".</summary>
        public string Rol { get; set; }

        public Usuario()
        {
            Correo       = string.Empty;
            ContrasenaHash = string.Empty;
            Nombre       = string.Empty;
            Rol          = string.Empty;
        }

        public Usuario(string correo, string contrasenaHash, string nombre, string rol)
        {
            Correo         = correo;
            ContrasenaHash = contrasenaHash;
            Nombre         = nombre;
            Rol            = rol;
        }
    }
}
