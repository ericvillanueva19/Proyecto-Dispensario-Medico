using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;

namespace DispensarioMedico.UI
{
    public partial class FrmTipoFarmaco : Form
    {
        private TipoFarmacoBLL bll = new TipoFarmacoBLL();
        private int idActual = 0;

        public FrmTipoFarmaco()
        {
            InitializeComponent();
        }

        private void FrmTipoFarmaco_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvTipoFarmaco.DataSource = bll.Listar();
                dgvTipoFarmaco.ClearSelection();
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
            dgvTipoFarmaco.ClearSelection();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoFarmaco tf = new TipoFarmaco
                {
                    ID = idActual,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                bll.Guardar(tf);
                MessageBox.Show("Registro guardado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // El botón modificar podría hacer lo mismo que guardar si se seleccionó un registro
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
                MessageBox.Show("Seleccione un registro para inactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de inactivar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bll.Inactivar(idActual);
                    MessageBox.Show("Registro inactivado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvTipoFarmaco_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTipoFarmaco.Rows[e.RowIndex];
                idActual = Convert.ToInt32(row.Cells["ID"].Value);
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);
            }
        }
    }
}
