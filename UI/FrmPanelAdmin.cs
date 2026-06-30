using System;
using System.Drawing;
using System.Windows.Forms;
using DispensarioMedico.Entities;

namespace DispensarioMedico.UI
{
    /// <summary>
    /// [UI] Panel del Administrador con menú lateral y secciones:
    /// Usuarios, Reportes y Configuración.
    /// </summary>
    public partial class FrmPanelAdmin : Form
    {
        private readonly Usuario _usuarioActual;

        public FrmPanelAdmin(Usuario usuario)
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
            Color accent      = Color.FromArgb(79,  140, 255);
            Color textPrimary = Color.FromArgb(240, 246, 252);
            Color textMuted   = Color.FromArgb(139, 148, 158);
            Color border      = Color.FromArgb(48,  54,  61);
            Color success     = Color.FromArgb(35,  134,  54);

            // ── Formulario ─────────────────────────────────────────────────────
            this.Text             = "Panel Administrador — Dispensario UNAPEC";
            this.Size             = new Size(1100, 680);
            this.StartPosition    = FormStartPosition.CenterScreen;
            this.BackColor        = bg;
            this.Font             = new Font("Segoe UI", 9.5f);
            this.FormBorderStyle  = FormBorderStyle.FixedSingle;
            this.MaximizeBox      = false;

            // ── Sidebar (menú lateral) ─────────────────────────────────────────
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

            // Avatar / Nombre usuario
            Label lblAvatar = new Label
            {
                Text      = "👤",
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
                Text      = "● ADMINISTRADOR",
                Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                Location  = new Point(0, 103),
                Size      = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = success,
                BackColor = Color.Transparent,
            };

            // Separador
            Panel sep1 = new Panel { Location = new Point(20, 135), Size = new Size(190, 1), BackColor = border };

            // Sección label
            Label lblMenu = new Label
            {
                Text      = "MENÚ PRINCIPAL",
                Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                Location  = new Point(20, 150),
                Size      = new Size(190, 18),
                ForeColor = textMuted,
                BackColor = Color.Transparent,
            };

            // Panel de contenido principal (cambia con cada menú)
            Panel contentPanel = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = bg,
            };

            // Botones de menú
            Button btnUsuarios     = CrearBotonMenu("👥  Usuarios",     accent,  textPrimary, 175);
            Button btnReportes     = CrearBotonMenu("📊  Reportes",     sidebar, textMuted,   225);
            Button btnConfiguracion = CrearBotonMenu("⚙️  Configuración", sidebar, textMuted,  275);

            btnUsuarios.Click      += (s, e) => CargarContenido(contentPanel, "Gestión de Usuarios",     accent);
            btnReportes.Click      += (s, e) => CargarContenido(contentPanel, "Reportes del Sistema",    accent);
            btnConfiguracion.Click += (s, e) => CargarContenido(contentPanel, "Configuración General",   accent);

            // Separador inferior
            Panel sep2 = new Panel { Location = new Point(20, 500), Size = new Size(190, 1), BackColor = border };

            // Botón Cerrar Sesión
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

            // Título del contenido inicial
            CargarContenido(contentPanel, "Gestión de Usuarios", accent);

            sidebar_pnl.Controls.AddRange(new Control[]
            {
                lblAvatar, lblNombreUsuario, lblRolBadge, sep1, lblMenu,
                btnUsuarios, btnReportes, btnConfiguracion, sep2, btnCerrarSesion,
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

            Label lbl = new Label
            {
                Text      = titulo,
                Font      = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(240, 246, 252),
                Location  = new Point(40, 40),
                Size      = new Size(750, 45),
                BackColor = Color.Transparent,
            };

            Label info = new Label
            {
                Text      = $"Módulo '{titulo}' cargado correctamente.\nAquí se integrará el formulario correspondiente.",
                Font      = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(139, 148, 158),
                Location  = new Point(40, 95),
                Size      = new Size(750, 50),
                BackColor = Color.Transparent,
            };

            Panel accentBar = new Panel
            {
                Location  = new Point(40, 38),
                Size      = new Size(4, 44),
                BackColor = accent,
            };

            panel.Controls.AddRange(new Control[] { accentBar, lbl, info });
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
