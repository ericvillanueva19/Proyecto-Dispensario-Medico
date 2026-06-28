using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;

namespace DispensarioMedico.UI
{
    public partial class FrmUbicacion : Form
    {
        private UbicacionBLL bll = new UbicacionBLL();
        private int idActual = 0;

        public FrmUbicacion()
        {
            InitializeComponent();
        }

        private void FrmUbicacion_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvUbicacion.DataSource = bll.Listar();
                dgvUbicacion.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            idActual = 0;
            txtDescripcion.Text = string.Empty;
            txtEstante.Text = string.Empty;
            txtTramo.Text = string.Empty;
            txtCelda.Text = string.Empty;
            chkEstado.Checked = true;
            dgvUbicacion.ClearSelection();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Ubicacion u = new Ubicacion
                {
                    ID = idActual,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estante = txtEstante.Text.Trim(),
                    Tramo = txtTramo.Text.Trim(),
                    Celda = txtCelda.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                bll.Guardar(u);
                MessageBox.Show("Ubicación guardada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnInactivar_Click(object sender, EventArgs e)
        {
            if (idActual == 0)
            {
                MessageBox.Show("Seleccione una ubicación para inactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de inactivar esta ubicación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bll.Inactivar(idActual);
                    MessageBox.Show("Ubicación inactivada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUbicacion_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUbicacion.Rows[e.RowIndex];
                idActual = Convert.ToInt32(row.Cells["ID"].Value);
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtEstante.Text = row.Cells["Estante"].Value.ToString();
                txtTramo.Text = row.Cells["Tramo"].Value.ToString();
                txtCelda.Text = row.Cells["Celda"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);
            }
        }
    }
}
