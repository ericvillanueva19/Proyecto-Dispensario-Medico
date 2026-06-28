using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;

namespace DispensarioMedico.UI
{
    public partial class FrmMarca : Form
    {
        private MarcaBLL bll = new MarcaBLL();
        private int idActual = 0;

        public FrmMarca()
        {
            InitializeComponent();
        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvMarca.DataSource = bll.Listar();
                dgvMarca.ClearSelection();
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
            chkEstado.Checked = true;
            dgvMarca.ClearSelection();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Marca m = new Marca
                {
                    ID = idActual,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                bll.Guardar(m);
                MessageBox.Show("Registro de marca guardado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Seleccione una marca para inactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de inactivar esta marca?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bll.Inactivar(idActual);
                    MessageBox.Show("Marca inactivada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMarca_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMarca.Rows[e.RowIndex];
                idActual = Convert.ToInt32(row.Cells["ID"].Value);
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);
            }
        }
    }
}
