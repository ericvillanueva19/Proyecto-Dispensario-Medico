using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DispensarioMedico.DAL;
using DispensarioMedico.Entities;

namespace DispensarioMedico.BLL
{
    /// <summary>
    /// [AGENTE DE LÓGICA DE NEGOCIO / BLL — ESPECIALISTA EN SEGURIDAD]
    /// Intermedia entre la UI y la DAL aplicando:
    ///   · Sanitización y validación de entradas (Regex + blacklist de caracteres)
    ///   · Control de fuerza bruta (bloqueo tras 3 intentos por 30 segundos)
    ///   · Retardo artificial de 1.5 s para mitigar timing attacks
    /// </summary>
    public class UsuarioBLL
    {
        // ── Dependencia DAL ─────────────────────────────────────────────────────
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();

        // ── Estado de bloqueo por fuerza bruta (en memoria, por correo) ─────────
        private static readonly Dictionary<string, int>      _intentosFallidos  = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, DateTime> _tiempoBloqueo     = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

        /// <summary>Máximo de intentos fallidos antes de bloquear la cuenta.</summary>
        public const int MaxIntentos = 3;

        /// <summary>Duración del bloqueo temporal en segundos.</summary>
        public const int SegundosBloqueo = 30;

        // ── Regex de validación ─────────────────────────────────────────────────
        // RFC 5322 simplificado – acepta formatos reales de correo institucional
        private static readonly Regex _regexCorreo =
            new Regex(@"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$",
                      RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Caracteres que podrían indicar intentos de inyección básica
        private static readonly char[] _charsSospechosos =
            new[] { '\'', '"', ';', '-', '-', '<', '>', '\\', '/', '*', '=', '(', ')' };

        // ── API pública ─────────────────────────────────────────────────────────

        /// <summary>
        /// Intenta autenticar al usuario aplicando todas las capas de seguridad.
        /// </summary>
        /// <param name="correo">Correo ingresado en la UI.</param>
        /// <param name="contrasena">Contraseña ingresada en la UI.</param>
        /// <returns>El <see cref="Usuario"/> autenticado o null si falla.</returns>
        /// <exception cref="InvalidOperationException">Cuando la cuenta está bloqueada.</exception>
        /// <exception cref="ArgumentException">Cuando las entradas no pasan la sanitización.</exception>
        public async Task<Usuario> AutenticarAsync(string correo, string contrasena)
        {
            // 1. Sanitización de entradas ───────────────────────────────────────
            correo    = SanitizarCorreo(correo);
            contrasena = SanitizarContrasena(contrasena);

            // 2. Control de fuerza bruta: verificar si está bloqueado ───────────
            VerificarBloqueo(correo);

            // 3. Retardo artificial (mitiga timing attacks y fuerza bruta) ───────
            await Task.Delay(1500);

            // 4. Validar credenciales contra la DAL ────────────────────────────
            Usuario usuario = _usuarioDAL.ValidarCredenciales(correo, contrasena);

            if (usuario == null)
            {
                // Registrar intento fallido
                RegistrarIntentoFallido(correo);
                return null;
            }

            // 5. Login exitoso: limpiar contadores ────────────────────────────
            LimpiarContadores(correo);
            return usuario;
        }

        /// <summary>
        /// Devuelve los segundos restantes de bloqueo para un correo dado.
        /// Retorna 0 si no hay bloqueo activo.
        /// </summary>
        public int ObtenerSegundosRestantes(string correo)
        {
            if (correo == null) return 0;
            correo = correo.Trim().ToLowerInvariant();

            if (_tiempoBloqueo.TryGetValue(correo, out DateTime hasta))
            {
                double restantes = (hasta - DateTime.Now).TotalSeconds;
                return restantes > 0 ? (int)Math.Ceiling(restantes) : 0;
            }
            return 0;
        }

        /// <summary>
        /// Indica si un correo se encuentra actualmente bloqueado.
        /// </summary>
        public bool EstaBloqueado(string correo)
        {
            if (correo == null) return false;
            correo = correo.Trim().ToLowerInvariant();

            if (_tiempoBloqueo.TryGetValue(correo, out DateTime hasta))
                return DateTime.Now < hasta;

            return false;
        }

        // ── Sanitización ────────────────────────────────────────────────────────

        private string SanitizarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo no puede estar vacío.");

            correo = correo.Trim();

            if (!_regexCorreo.IsMatch(correo))
                throw new ArgumentException("El formato del correo electrónico no es válido.");

            if (correo.Length > 150)
                throw new ArgumentException("El correo excede el largo permitido.");

            return correo;
        }

        private string SanitizarContrasena(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            // No se hace Trim() en contraseña — un espacio puede ser intencional,
            // pero SÍ bloqueamos contraseñas que SOLO son espacios.
            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña no puede contener solo espacios en blanco.");

            if (contrasena.Length > 128)
                throw new ArgumentException("La contraseña excede el largo permitido.");

            // Detección de caracteres sospechosos de inyección básica
            if (contrasena.IndexOfAny(_charsSospechosos) >= 0)
                throw new ArgumentException("La contraseña contiene caracteres no permitidos.");

            return contrasena;
        }

        // ── Control de fuerza bruta ─────────────────────────────────────────────

        private void VerificarBloqueo(string correo)
        {
            string key = correo.ToLowerInvariant();

            if (_tiempoBloqueo.TryGetValue(key, out DateTime hasta))
            {
                if (DateTime.Now < hasta)
                {
                    int restantes = (int)Math.Ceiling((hasta - DateTime.Now).TotalSeconds);
                    throw new InvalidOperationException(
                        $"Cuenta bloqueada temporalmente. Intente nuevamente en {restantes} segundo(s).");
                }
                else
                {
                    // Bloqueo expirado: limpiar
                    _tiempoBloqueo.Remove(key);
                    _intentosFallidos.Remove(key);
                }
            }
        }

        private void RegistrarIntentoFallido(string correo)
        {
            string key = correo.ToLowerInvariant();

            if (!_intentosFallidos.ContainsKey(key))
                _intentosFallidos[key] = 0;

            _intentosFallidos[key]++;

            if (_intentosFallidos[key] >= MaxIntentos)
            {
                _tiempoBloqueo[key] = DateTime.Now.AddSeconds(SegundosBloqueo);
                _intentosFallidos.Remove(key);
                throw new InvalidOperationException(
                    $"Demasiados intentos fallidos. Cuenta bloqueada por {SegundosBloqueo} segundos.");
            }
        }

        private void LimpiarContadores(string correo)
        {
            string key = correo.ToLowerInvariant();
            _intentosFallidos.Remove(key);
            _tiempoBloqueo.Remove(key);
        }
    }
}
