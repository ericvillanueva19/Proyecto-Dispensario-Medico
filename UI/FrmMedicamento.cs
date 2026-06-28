using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;

namespace DispensarioMedico.UI
{
    public partial class FrmMedicamento : Form
    {
        private MedicamentoBLL bll = new MedicamentoBLL();
        
        // BLLs de dependencias para los Combobox
        private TipoFarmacoBLL tipoFarmacoBLL = new TipoFarmacoBLL();
        private MarcaBLL marcaBLL = new MarcaBLL();
        private UbicacionBLL ubicacionBLL = new UbicacionBLL();

        private int idActual = 0;

        public FrmMedicamento()
        {
            InitializeComponent();
        }

        private void FrmMedicamento_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarDatos();
        }

        private void CargarCombos()
        {
            try
            {
                cmbTipoFarmaco.DataSource = tipoFarmacoBLL.Listar();
                cmbTipoFarmaco.DisplayMember = "Descripcion";
                cmbTipoFarmaco.ValueMember = "ID";
                cmbTipoFarmaco.SelectedIndex = -1;

                cmbMarca.DataSource = marcaBLL.Listar();
                cmbMarca.DisplayMember = "Descripcion";
                cmbMarca.ValueMember = "ID";
                cmbMarca.SelectedIndex = -1;

                cmbUbicacion.DataSource = ubicacionBLL.Listar();
                cmbUbicacion.DisplayMember = "Descripcion";
                cmbUbicacion.ValueMember = "ID";
                cmbUbicacion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los listados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            try
            {
                dgvMedicamento.DataSource = bll.Listar();
                
                // Ocultar columnas de IDs crudos para una vista más limpia
                if(dgvMedicamento.Columns["TipoFarmacoID"] != null) dgvMedicamento.Columns["TipoFarmacoID"].Visible = false;
                if(dgvMedicamento.Columns["MarcaID"] != null) dgvMedicamento.Columns["MarcaID"].Visible = false;
                if(dgvMedicamento.Columns["UbicacionID"] != null) dgvMedicamento.Columns["UbicacionID"].Visible = false;

                dgvMedicamento.ClearSelection();
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
            cmbTipoFarmaco.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbUbicacion.SelectedIndex = -1;
            txtDosis.Text = string.Empty;
            chkEstado.Checked = true;
            dgvMedicamento.ClearSelection();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Medicamento med = new Medicamento
                {
                    ID = idActual,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Dosis = txtDosis.Text.Trim(),
                    Estado = chkEstado.Checked
                };

                // Asignar IDs de combos
                if (cmbTipoFarmaco.SelectedValue != null)
                    med.TipoFarmacoID = (int)cmbTipoFarmaco.SelectedValue;
                
                if (cmbMarca.SelectedValue != null)
                    med.MarcaID = (int)cmbMarca.SelectedValue;
                
                if (cmbUbicacion.SelectedValue != null)
                    med.UbicacionID = (int)cmbUbicacion.SelectedValue;

                bll.Guardar(med);
                MessageBox.Show("Medicamento guardado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Seleccione un medicamento para inactivar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de inactivar este medicamento?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bll.Inactivar(idActual);
                    MessageBox.Show("Medicamento inactivado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMedicamento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMedicamento.Rows[e.RowIndex];
                idActual = Convert.ToInt32(row.Cells["ID"].Value);
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtDosis.Text = row.Cells["Dosis"].Value.ToString();
                
                cmbTipoFarmaco.SelectedValue = row.Cells["TipoFarmacoID"].Value;
                cmbMarca.SelectedValue = row.Cells["MarcaID"].Value;
                cmbUbicacion.SelectedValue = row.Cells["UbicacionID"].Value;

                chkEstado.Checked = Convert.ToBoolean(row.Cells["Estado"].Value);
            }
        }
    }
}
