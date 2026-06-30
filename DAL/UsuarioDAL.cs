using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    /// <summary>
    /// [AGENTE ARQUITECTO DE DATOS / DAL]
    /// Capa de Acceso a Datos para la autenticación de usuarios.
    /// Simula un repositorio en memoria para no requerir tabla SQL en esta etapa.
    ///
    /// ─────────────────────────────────────────────────
    ///  CREDENCIALES DE PRUEBA (solo para desarrollo)
    /// ─────────────────────────────────────────────────
    ///  ROL ADMIN:
    ///    Correo  : admin@unapec.edu.do
    ///    Password: Admin@2024
    ///
    ///  ROL CLIENTE:
    ///    Correo  : cliente@unapec.edu.do
    ///    Password: Cliente@2024
    ///
    ///  ROL CLIENTE 2:
    ///    Correo  : juan.perez@unapec.edu.do
    ///    Password: Juan@5678
    /// ─────────────────────────────────────────────────
    /// </summary>
    public class UsuarioDAL
    {
        // ── Repositorio en memoria ──────────────────────────────────────────────
        private static readonly List<Usuario> _usuarios = new List<Usuario>
        {
            // Usuario administrador del sistema
            new Usuario(
                correo        : "admin@unapec.edu.do",
                contrasenaHash: ComputeSha256("Admin@2024"),
                nombre        : "Administrador Sistema",
                rol           : "admin"
            ),

            // Usuario cliente / paciente 1
            new Usuario(
                correo        : "cliente@unapec.edu.do",
                contrasenaHash: ComputeSha256("Cliente@2024"),
                nombre        : "María González",
                rol           : "cliente"
            ),

            // Usuario cliente / paciente 2
            new Usuario(
                correo        : "juan.perez@unapec.edu.do",
                contrasenaHash: ComputeSha256("Juan@5678"),
                nombre        : "Juan Pérez",
                rol           : "cliente"
            ),
        };

        // ── Métodos públicos ────────────────────────────────────────────────────

        /// <summary>
        /// Busca un usuario por correo y verifica la contraseña mediante comparación
        /// de hashes SHA-256 para evitar comparaciones en texto plano.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario (ya sanitizado por la BLL).</param>
        /// <param name="contrasena">Contraseña en texto plano ingresada por el usuario.</param>
        /// <returns>El objeto <see cref="Usuario"/> si las credenciales son válidas; null en caso contrario.</returns>
        public Usuario ValidarCredenciales(string correo, string contrasena)
        {
            string hashIngresado = ComputeSha256(contrasena);

            // Búsqueda insensible a mayúsculas en el correo + comparación de hash
            Usuario usuarioEncontrado = _usuarios.FirstOrDefault(u =>
                string.Equals(u.Correo, correo, StringComparison.OrdinalIgnoreCase));

            if (usuarioEncontrado == null)
                return null; // Correo no existe

            // Comparación segura de hashes (evita timing leaks elementales)
            bool hashCoincide = SecureStringEquals(usuarioEncontrado.ContrasenaHash, hashIngresado);

            return hashCoincide ? usuarioEncontrado : null;
        }

        // ── Helpers privados ────────────────────────────────────────────────────

        /// <summary>
        /// Genera el hash SHA-256 de una cadena en texto plano.
        /// </summary>
        public static string ComputeSha256(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>
        /// Comparación de cadenas en tiempo constante para mitigar ataques de temporización.
        /// </summary>
        private static bool SecureStringEquals(string a, string b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int resultado = 0;
            for (int i = 0; i < a.Length; i++)
                resultado |= a[i] ^ b[i]; // XOR: si son iguales el resultado es 0

            return resultado == 0;
        }
    }
}
