using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;

namespace DispensarioMedico.UI
{
    public partial class FrmMedico : Form
    {
        private MedicoBLL bll = new MedicoBLL();
        private int idActual = 0;

        public FrmMedico()
        {
            InitializeComponent();
        }

        private void FrmMedico_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatos();
        }

        private void CargarCombos()
        {
            cmbTandaLaboral.Items.Clear();
            cmbTandaLaboral.Items.Add("Matutina (Mañana)");
            cmbTandaLaboral.Items.Add("Vespertina (Tarde)");
            cmbTandaLaboral.Items.Add("Nocturna (Noche)");
            cmbTandaLaboral.Items.Add("Jornada Completa");
            cmbTandaLaboral.SelectedIndex = -1;
        }

        private void CargarDatos()
        {
            try
            {
                dgvMedico.DataSource = bll.Listar();
                dgvMedico.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            idActual = 0;
            txtNombre.Text = string.Empty;
            txtCedula.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            cmbTandaLaboral.SelectedIndex = -1;
            chkEstado.Checked = true;
            dgvMedico.ClearSelection();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Medico m = new Medico
                {
                    ID = idActual,
                    Nombre = txtNombre.Text.Trim(),
                    Cedula = txtCedula.Text.Trim(),
                    Especialidad = txtEspecialidad.Text.Trim(),
                    Tanda_Labor = cmbTandaLaboral.SelectedItem != null ? cmbTandaLaboral.SelectedItem.ToString() : string.Empty,
                    Estado = chkEstado.Checked
                };

                bll.Guardar(m);
                MessageBox.Show("Médico guardado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Seleccione un médico para inactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de inactivar este médico?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bll.Inactivar(idActual);
                    MessageBox.Show("Médico inactivado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMedico_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMedico.Rows[e.RowIndex];
                idActual = Convert.ToInt32(row.Cells["ID"].Value);
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtCedula.Text = row.Cells["Cedula"].Value.ToString();
                txtEspecialidad.Text = row.Cells["Especialidad"].Value.ToString();
                cmbTandaLaboral.SelectedItem = row.Cells["Tanda_Labor"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);
            }
        }
    }
}
