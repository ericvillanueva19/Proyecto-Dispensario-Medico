using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DispensarioMedico.BLL;
using DispensarioMedico.Entities;

namespace DispensarioMedico.UI
{
    /// <summary>
    /// [AGENTE DE INTERFAZ DE USUARIO / UI]
    /// Formulario de Login con diseño oscuro moderno, paleta índigo/azul institucional,
    /// validaciones reactivas, estados de carga y notificaciones de seguridad.
    /// </summary>
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL = new UsuarioBLL();
        private System.Windows.Forms.Timer _timerBloqueo;
        private bool _passwordVisible = false;

        public FrmLogin()
        {
            InitializeComponent();
            ConfigurarEstilosFormulario();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  CONFIGURACIÓN VISUAL (equivale al diseño en código, sin Designer)
        // ════════════════════════════════════════════════════════════════════════
        private void ConfigurarEstilosFormulario()
        {
            // ── Colores del sistema de diseño ──────────────────────────────────
            Color bg          = Color.FromArgb(13,  17,  23);   // Fondo principal
            Color surface     = Color.FromArgb(22,  27,  34);   // Superficie de tarjeta
            Color border      = Color.FromArgb(48,  54,  61);   // Bordes sutiles
            Color accent      = Color.FromArgb(79,  140, 255);  // Azul/índigo institucional
            Color accentHover = Color.FromArgb(56,  115, 230);
            Color textPrimary = Color.FromArgb(240, 246, 252);
            Color textMuted   = Color.FromArgb(139, 148, 158);
            Color danger      = Color.FromArgb(255,  85,  85);

            this.BackColor        = bg;
            this.ForeColor        = textPrimary;
            this.Font             = new Font("Segoe UI", 9.5f);
            this.FormBorderStyle  = FormBorderStyle.None;
            this.Size             = new Size(460, 560);
            this.StartPosition    = FormStartPosition.CenterScreen;
            this.Text             = "Dispensario Médico UNAPEC — Login";

            // Permitir mover la ventana sin borde
            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    NativeMethods.ReleaseCapture();
                    NativeMethods.SendMessage(Handle, 0xA1, 0x2, 0);
                }
            };

            // ── Panel tarjeta central ──────────────────────────────────────────
            Panel card = new Panel
            {
                Size      = new Size(380, 460),
                BackColor = surface,
                Location  = new Point(40, 50),
            };
            card.Paint += (s, e) =>
            {
                // Borde redondeado simulado con un rectángulo de borde
                using (Pen p = new Pen(border, 1))
                    e.Graphics.DrawRectangle(p, 0, 0, card.Width - 1, card.Height - 1);
            };

            // ── Logo / Encabezado ─────────────────────────────────────────────
            Label lblLogo = new Label
            {
                Text      = "🏥",
                Font      = new Font("Segoe UI Emoji", 32),
                Location  = new Point(0, 30),
                Size      = new Size(380, 55),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = accent,
            };

            Label lblTitulo = new Label
            {
                Text      = "Dispensario Médico",
                Font      = new Font("Segoe UI", 16, FontStyle.Bold),
                Location  = new Point(0, 90),
                Size      = new Size(380, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = textPrimary,
            };

            Label lblSubtitulo = new Label
            {
                Text      = "UNAPEC · Sistema Institucional",
                Font      = new Font("Segoe UI", 9, FontStyle.Regular),
                Location  = new Point(0, 118),
                Size      = new Size(380, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = textMuted,
            };

            // ── Separador ─────────────────────────────────────────────────────
            Panel sep = new Panel
            {
                Location  = new Point(30, 148),
                Size      = new Size(320, 1),
                BackColor = border,
            };

            // ── Label + TextBox Correo ────────────────────────────────────────
            Label lblCorreo = new Label
            {
                Text      = "Correo electrónico",
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                Location  = new Point(30, 165),
                Size      = new Size(320, 18),
                ForeColor = textMuted,
                BackColor = Color.Transparent,
            };

            Panel pnlCorreo = ConstruirInputPanel(border);
            pnlCorreo.Location = new Point(30, 185);

            TextBox txtCorreo = new TextBox
            {
                Name            = "txtCorreo",
                Dock            = DockStyle.Fill,
                BackColor       = Color.FromArgb(30, 36, 44),
                ForeColor       = textPrimary,
                BorderStyle     = BorderStyle.None,
                Font            = new Font("Segoe UI", 10),
                PlaceholderText = "usuario@unapec.edu.do",
                Padding         = new Padding(4, 0, 0, 0),
            };

            pnlCorreo.Controls.Add(txtCorreo);
            pnlCorreo.MouseClick += (s, e) => txtCorreo.Focus();

            // ── Label + TextBox Contraseña ────────────────────────────────────
            Label lblPass = new Label
            {
                Text      = "Contraseña",
                Font      = new Font("Segoe UI", 8.5f),
                Location  = new Point(30, 250),
                Size      = new Size(320, 18),
                ForeColor = textMuted,
                BackColor = Color.Transparent,
            };

            Panel pnlPass = ConstruirInputPanel(border);
            pnlPass.Location = new Point(30, 270);

            TextBox txtPassword = new TextBox
            {
                Name                  = "txtPassword",
                Dock                  = DockStyle.None,
                BackColor             = Color.FromArgb(30, 36, 44),
                ForeColor             = textPrimary,
                BorderStyle           = BorderStyle.None,
                Font                  = new Font("Segoe UI", 10),
                PlaceholderText       = "••••••••",
                UseSystemPasswordChar = true,  // Activo por defecto
                Width                 = 282,
                Height                = 22,
                Location              = new Point(6, 9),
            };

            // Botón "ojo" para mostrar/ocultar contraseña
            Button btnOjo = new Button
            {
                Text      = "👁",
                Size      = new Size(32, 40),
                Dock      = DockStyle.Right,
                BackColor = Color.Transparent,
                ForeColor = textMuted,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand,
                Font      = new Font("Segoe UI Emoji", 11),
                TabStop   = false,
            };
            btnOjo.FlatAppearance.BorderSize = 0;
            btnOjo.Click += (s, e) =>
            {
                _passwordVisible = !_passwordVisible;
                txtPassword.UseSystemPasswordChar = !_passwordVisible;
                btnOjo.ForeColor = _passwordVisible ? accent : textMuted;
            };

            pnlPass.Controls.Add(txtPassword);
            pnlPass.Controls.Add(btnOjo);

            // ── Label de error/seguridad ──────────────────────────────────────
            Label lblError = new Label
            {
                Name      = "lblError",
                Text      = string.Empty,
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Location  = new Point(30, 325),
                Size      = new Size(320, 36),
                ForeColor = danger,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible   = false,
            };

            // ── Label de estado (cargando) ────────────────────────────────────
            Label lblEstado = new Label
            {
                Name      = "lblEstado",
                Text      = string.Empty,
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                Location  = new Point(30, 325),
                Size      = new Size(320, 36),
                ForeColor = accent,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible   = false,
            };

            // ── Botón Ingresar ────────────────────────────────────────────────
            Button btnIngresar = new Button
            {
                Name      = "btnIngresar",
                Text      = "INGRESAR  →",
                Size      = new Size(320, 42),
                Location  = new Point(30, 372),
                BackColor = accent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor    = Cursors.Hand,
            };
            btnIngresar.FlatAppearance.BorderSize  = 0;
            btnIngresar.FlatAppearance.MouseOverBackColor   = accentHover;
            btnIngresar.FlatAppearance.MouseDownBackColor   = Color.FromArgb(40, 90, 200);
            btnIngresar.Click += async (s, e) =>
                await EjecutarLoginAsync(txtCorreo, txtPassword, btnIngresar, lblError, lblEstado);

            // Permitir Enter para login
            txtPassword.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    await EjecutarLoginAsync(txtCorreo, txtPassword, btnIngresar, lblError, lblEstado);
            };
            txtCorreo.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    await EjecutarLoginAsync(txtCorreo, txtPassword, btnIngresar, lblError, lblEstado);
            };

            // ── Pie de página ─────────────────────────────────────────────────
            Label lblFooter = new Label
            {
                Text      = "© 2024 UNAPEC · Dispensario Médico",
                Font      = new Font("Segoe UI", 7.5f),
                Location  = new Point(0, 428),
                Size      = new Size(380, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = textMuted,
                BackColor = Color.Transparent,
            };

            // ── Botón cerrar (X) ──────────────────────────────────────────────
            Button btnCerrar = new Button
            {
                Text      = "✕",
                Size      = new Size(30, 30),
                Location  = new Point(420, 10),
                BackColor = Color.Transparent,
                ForeColor = textMuted,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10),
                Cursor    = Cursors.Hand,
                TabStop   = false,
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) => Application.Exit();

            // ── Ensamblar controles en la tarjeta ─────────────────────────────
            card.Controls.AddRange(new Control[]
            {
                lblLogo, lblTitulo, lblSubtitulo, sep,
                lblCorreo, pnlCorreo,
                lblPass, pnlPass,
                lblError, lblEstado,
                btnIngresar, lblFooter,
            });

            this.Controls.Add(card);
            this.Controls.Add(btnCerrar);

            // ── Timer para actualizar cuenta regresiva ─────────────────────────
            _timerBloqueo = new System.Windows.Forms.Timer { Interval = 1000 };
            _timerBloqueo.Tick += (s, e) =>
                ActualizarCuentaRegresiva(txtCorreo.Text, btnIngresar, lblError, lblEstado);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  LÓGICA DE LOGIN
        // ════════════════════════════════════════════════════════════════════════
        private async Task EjecutarLoginAsync(
            TextBox txtCorreo, TextBox txtPassword,
            Button btnIngresar, Label lblError, Label lblEstado)
        {
            // ── Validación visual reactiva (campos vacíos) ─────────────────────
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MostrarError(lblError, lblEstado, "⚠ El campo de correo no puede estar vacío.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MostrarError(lblError, lblEstado, "⚠ El campo de contraseña no puede estar vacío.");
                return;
            }

            // ── Estado de carga: deshabilitar controles ────────────────────────
            EstablecerEstadoCarga(true, btnIngresar, txtCorreo, txtPassword, lblError, lblEstado);

            try
            {
                Usuario usuario = await _usuarioBLL.AutenticarAsync(
                    txtCorreo.Text.Trim(),
                    txtPassword.Text);

                if (usuario == null)
                {
                    MostrarError(lblError, lblEstado,
                        "⚠ Credenciales incorrectas. Verifique su correo y contraseña.");
                }
                else
                {
                    // Login exitoso: abrir panel correspondiente
                    AbrirPanelUsuario(usuario);
                    LimpiarFormulario(txtCorreo, txtPassword, lblError, lblEstado);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Cuenta bloqueada por fuerza bruta
                MostrarError(lblError, lblEstado, "🔒 " + ex.Message);
                btnIngresar.Enabled = false;
                _timerBloqueo.Start();
            }
            catch (ArgumentException ex)
            {
                // Error de sanitización / validación
                MostrarError(lblError, lblEstado, "⚠ " + ex.Message);
            }
            catch (Exception ex)
            {
                MostrarError(lblError, lblEstado, "✕ Error inesperado: " + ex.Message);
            }
            finally
            {
                EstablecerEstadoCarga(false, btnIngresar, txtCorreo, txtPassword, lblError, lblEstado);
            }
        }

        private void AbrirPanelUsuario(Usuario usuario)
        {
            Form panel;

            if (usuario.Rol.Equals("admin", StringComparison.OrdinalIgnoreCase))
                panel = new FrmPanelAdmin(usuario);
            else
                panel = new FrmPanelCliente(usuario);

            this.Hide();
            panel.FormClosed += (s, e) =>
            {
                SesionActual.Limpiar();
                this.Show();
            };
            panel.Show();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  HELPERS DE INTERFAZ
        // ════════════════════════════════════════════════════════════════════════
        private static Panel ConstruirInputPanel(Color borderColor)
        {
            Panel p = new Panel
            {
                Size      = new Size(320, 40),
                BackColor = Color.FromArgb(30, 36, 44),
                Cursor    = Cursors.IBeam,
            };
            p.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(borderColor, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            };
            return p;
        }

        private static void MostrarError(Label lblError, Label lblEstado, string mensaje)
        {
            lblEstado.Visible = false;
            lblError.Text     = mensaje;
            lblError.Visible  = true;
        }

        private static void EstablecerEstadoCarga(
            bool cargando, Button btn, TextBox txtC, TextBox txtP,
            Label lblError, Label lblEstado)
        {
            btn.Enabled  = !cargando;
            txtC.Enabled = !cargando;
            txtP.Enabled = !cargando;

            if (cargando)
            {
                lblError.Visible  = false;
                lblEstado.Text    = "🔐 Verificando credenciales de forma segura...";
                lblEstado.Visible = true;
            }
            else
            {
                lblEstado.Visible = false;
            }
        }

        private void ActualizarCuentaRegresiva(
            string correo, Button btnIngresar, Label lblError, Label lblEstado)
        {
            int restantes = _usuarioBLL.ObtenerSegundosRestantes(correo);

            if (restantes > 0)
            {
                MostrarError(lblError, lblEstado,
                    $"🔒 Cuenta bloqueada. Intente en {restantes} segundo(s).");
                btnIngresar.Enabled = false;
            }
            else
            {
                _timerBloqueo.Stop();
                lblError.Visible    = false;
                btnIngresar.Enabled = true;
            }
        }

        private static void LimpiarFormulario(
            TextBox txtCorreo, TextBox txtPassword, Label lblError, Label lblEstado)
        {
            txtCorreo.Clear();
            txtPassword.Clear();
            lblError.Visible  = false;
            lblEstado.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timerBloqueo?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
