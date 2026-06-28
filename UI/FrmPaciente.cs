using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;

namespace DispensarioMedico.UI
{
    public partial class FrmPaciente : Form
    {
        private PacienteBLL bll = new PacienteBLL();
        private int idActual = 0;

        public FrmPaciente()
        {
            InitializeComponent();
        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatos();
        }

        private void CargarCombos()
        {
            cmbTipoPaciente.Items.Clear();
            cmbTipoPaciente.Items.Add("Estudiante");
            cmbTipoPaciente.Items.Add("Empleado");
            cmbTipoPaciente.Items.Add("Profesor");
            cmbTipoPaciente.Items.Add("Otros");
            cmbTipoPaciente.SelectedIndex = -1;
        }

        private void CargarDatos()
        {
            try
            {
                dgvPaciente.DataSource = bll.Listar();
                dgvPaciente.ClearSelection();
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
            txtNoCarnet.Text = string.Empty;
            cmbTipoPaciente.SelectedIndex = -1;
            chkEstado.Checked = true;
            dgvPaciente.ClearSelection();
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente p = new Paciente
                {
                    ID = idActual,
                    Nombre = txtNombre.Text.Trim(),
                    Cedula = txtCedula.Text.Trim(),
                    No_Carnet = txtNoCarnet.Text.Trim(),
                    TipoPaciente = cmbTipoPaciente.SelectedItem != null ? cmbTipoPaciente.SelectedItem.ToString() : string.Empty,
                    Estado = chkEstado.Checked
                };

                bll.Guardar(p);
                MessageBox.Show("Paciente guardado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Seleccione un paciente para inactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de inactivar este paciente?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bll.Inactivar(idActual);
                    MessageBox.Show("Paciente inactivado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPaciente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPaciente.Rows[e.RowIndex];
                idActual = Convert.ToInt32(row.Cells["ID"].Value);
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtCedula.Text = row.Cells["Cedula"].Value.ToString();
                txtNoCarnet.Text = row.Cells["No_Carnet"].Value.ToString();
                cmbTipoPaciente.SelectedItem = row.Cells["TipoPaciente"].Value.ToString();
                chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);
            }
        }
    }
}
