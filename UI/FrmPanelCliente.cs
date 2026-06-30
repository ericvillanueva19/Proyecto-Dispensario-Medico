using System;
using System.Drawing;
using System.Windows.Forms;
using DispensarioMedico.Entities;

namespace DispensarioMedico.UI
{
    /// <summary>
    /// [UI] Panel del Cliente con menú lateral y secciones:
    /// Pedidos, Facturas y Mi Perfil.
    /// </summary>
    public partial class FrmPanelCliente : Form
    {
        private readonly Usuario _usuarioActual;

        public FrmPanelCliente(Usuario usuario)
        {
            _usuarioActual = usuario;
            SesionActual.Usuario = usuario;
            InitializeComponent();
            ConfigurarPanel();
        }

        private void ConfigurarPanel()
        {
            // ── Paleta de colores ──────────────────────────────────────────────
            Color bg          = Color.FromArgb(13,  17,  23);
            Color sidebar     = Color.FromArgb(22,  27,  34);
            Color accent      = Color.FromArgb(56,  211, 159);  // Verde-teal para cliente
            Color textPrimary = Color.FromArgb(240, 246, 252);
            Color textMuted   = Color.FromArgb(139, 148, 158);
            Color border      = Color.FromArgb(48,  54,  61);
            Color rolColor    = Color.FromArgb(56,  189, 248);

            // ── Formulario ─────────────────────────────────────────────────────
            this.Text             = "Panel Cliente — Dispensario UNAPEC";
            this.Size             = new Size(1100, 680);
            this.StartPosition    = FormStartPosition.CenterScreen;
            this.BackColor        = bg;
            this.Font             = new Font("Segoe UI", 9.5f);
            this.FormBorderStyle  = FormBorderStyle.FixedSingle;
            this.MaximizeBox      = false;

            // ── Sidebar ────────────────────────────────────────────────────────
            Panel sidebar_pnl = new Panel
            {
                Dock      = DockStyle.Left,
                Width     = 230,
                BackColor = sidebar,
            };
            sidebar_pnl.Paint += (s, e) =>
            {
                using (Pen p = new Pen(border, 1))
                    e.Graphics.DrawLine(p, sidebar_pnl.Width - 1, 0, sidebar_pnl.Width - 1, sidebar_pnl.Height);
            };

            Label lblAvatar = new Label
            {
                Text      = "🧑",
                Font      = new Font("Segoe UI Emoji", 28),
                Location  = new Point(0, 30),
                Size      = new Size(230, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = accent,
                BackColor = Color.Transparent,
            };

            Label lblNombreUsuario = new Label
            {
                Text      = _usuarioActual.Nombre,
                Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Location  = new Point(0, 82),
                Size      = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = textPrimary,
                BackColor = Color.Transparent,
            };

            Label lblRolBadge = new Label
            {
                Text      = "● CLIENTE",
                Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                Location  = new Point(0, 103),
                Size      = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = rolColor,
                BackColor = Color.Transparent,
            };

            Panel sep1 = new Panel { Location = new Point(20, 135), Size = new Size(190, 1), BackColor = border };

            Label lblMenu = new Label
            {
                Text      = "MI ESPACIO",
                Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                Location  = new Point(20, 150),
                Size      = new Size(190, 18),
                ForeColor = textMuted,
                BackColor = Color.Transparent,
            };

            // Panel de contenido
            Panel contentPanel = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = bg,
            };

            // Botones del menú cliente
            Button btnPedidos  = CrearBotonMenu("📦  Mis Pedidos",  accent,  textPrimary, 175);
            Button btnFacturas = CrearBotonMenu("🧾  Facturas",     sidebar, textMuted,   225);
            Button btnPerfil   = CrearBotonMenu("👤  Mi Perfil",    sidebar, textMuted,   275);

            btnPedidos.Click  += (s, e) => CargarContenido(contentPanel, "Mis Pedidos",  accent);
            btnFacturas.Click += (s, e) => CargarContenido(contentPanel, "Mis Facturas", accent);
            btnPerfil.Click   += (s, e) => CargarContenidoPerfil(contentPanel, accent);

            Panel sep2 = new Panel { Location = new Point(20, 500), Size = new Size(190, 1), BackColor = border };

            Button btnCerrarSesion = new Button
            {
                Text      = "⏻  Cerrar Sesión",
                Size      = new Size(190, 40),
                Location  = new Point(20, 515),
                BackColor = Color.FromArgb(100, 30, 30),
                ForeColor = Color.FromArgb(255, 130, 130),
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor    = Cursors.Hand,
                Anchor    = AnchorStyles.Bottom | AnchorStyles.Left,
            };
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Click += (s, e) => CerrarSesion();

            // Cargar sección inicial
            CargarContenido(contentPanel, "Mis Pedidos", accent);

            sidebar_pnl.Controls.AddRange(new Control[]
            {
                lblAvatar, lblNombreUsuario, lblRolBadge, sep1, lblMenu,
                btnPedidos, btnFacturas, btnPerfil, sep2, btnCerrarSesion,
            });

            this.Controls.Add(contentPanel);
            this.Controls.Add(sidebar_pnl);
        }

        private static Button CrearBotonMenu(string texto, Color bg, Color fg, int y)
        {
            Button b = new Button
            {
                Text      = texto,
                Size      = new Size(190, 42),
                Location  = new Point(20, y),
                BackColor = bg,
                ForeColor = fg,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9.5f),
                Cursor    = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding   = new Padding(8, 0, 0, 0),
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        private static void CargarContenido(Panel panel, string titulo, Color accent)
        {
            panel.Controls.Clear();

            Panel accentBar = new Panel
            {
                Location  = new Point(40, 38),
                Size      = new Size(4, 44),
                BackColor = accent,
            };

            Label lbl = new Label
            {
                Text      = titulo,
                Font      = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(240, 246, 252),
                Location  = new Point(56, 40),
                Size      = new Size(750, 45),
                BackColor = Color.Transparent,
            };

            Label info = new Label
            {
                Text      = $"Módulo '{titulo}' listo.\nAquí se desplegará la información correspondiente.",
                Font      = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(139, 148, 158),
                Location  = new Point(56, 95),
                Size      = new Size(750, 50),
                BackColor = Color.Transparent,
            };

            panel.Controls.AddRange(new Control[] { accentBar, lbl, info });
        }

        private void CargarContenidoPerfil(Panel panel, Color accent)
        {
            panel.Controls.Clear();

            Panel accentBar = new Panel
            {
                Location  = new Point(40, 38),
                Size      = new Size(4, 44),
                BackColor = accent,
            };

            Label lbl = new Label
            {
                Text      = "Mi Perfil",
                Font      = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(240, 246, 252),
                Location  = new Point(56, 40),
                Size      = new Size(750, 45),
                BackColor = Color.Transparent,
            };

            Label lblNombre = new Label
            {
                Text      = $"👤  Nombre: {_usuarioActual.Nombre}",
                Font      = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(200, 210, 220),
                Location  = new Point(56, 110),
                Size      = new Size(500, 28),
                BackColor = Color.Transparent,
            };

            Label lblCorreo = new Label
            {
                Text      = $"✉️  Correo: {_usuarioActual.Correo}",
                Font      = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(200, 210, 220),
                Location  = new Point(56, 148),
                Size      = new Size(500, 28),
                BackColor = Color.Transparent,
            };

            Label lblRol = new Label
            {
                Text      = $"🔖  Rol: {_usuarioActual.Rol.ToUpperInvariant()}",
                Font      = new Font("Segoe UI", 11),
                ForeColor = accent,
                Location  = new Point(56, 186),
                Size      = new Size(500, 28),
                BackColor = Color.Transparent,
            };

            panel.Controls.AddRange(new Control[] { accentBar, lbl, lblNombre, lblCorreo, lblRol });
        }

        private void CerrarSesion()
        {
            DialogResult r = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Cerrar Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                SesionActual.Limpiar();
                this.Close();
            }
        }
    }
}
