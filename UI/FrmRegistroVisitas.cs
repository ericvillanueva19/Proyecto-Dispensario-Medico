using System;
using System.Windows.Forms;
using DispensarioMedico.Entities;
using DispensarioMedico.BLL;
using System.Collections.Generic;

namespace DispensarioMedico.UI
{
    public partial class FrmRegistroVisitas : Form
    {
        private RegistroVisitaBLL bll = new RegistroVisitaBLL();
        
        // Dependencias para llenar Combos
        private MedicoBLL medicoBLL = new MedicoBLL();
        private PacienteBLL pacienteBLL = new PacienteBLL();
        private MedicamentoBLL medicamentoBLL = new MedicamentoBLL();

        public FrmRegistroVisitas()
        {
            InitializeComponent();
        }

        private void FrmRegistroVisitas_Load(object sender, EventArgs e)
        {
            CargarCombos();
            LimpiarRegistro();
            LimpiarConsulta();
        }

        private void CargarCombos()
        {
            try
            {
                // Combos para Registro
                cmbMedicoReg.DataSource = medicoBLL.Listar();
                cmbMedicoReg.DisplayMember = "Nombre";
                cmbMedicoReg.ValueMember = "ID";
                cmbMedicoReg.SelectedIndex = -1;

                cmbPacienteReg.DataSource = pacienteBLL.Listar();
                cmbPacienteReg.DisplayMember = "Nombre";
                cmbPacienteReg.ValueMember = "ID";
                cmbPacienteReg.SelectedIndex = -1;

                cmbMedicamentoReg.DataSource = medicamentoBLL.Listar();
                cmbMedicamentoReg.DisplayMember = "Descripcion";
                cmbMedicamentoReg.ValueMember = "ID";
                cmbMedicamentoReg.SelectedIndex = -1;

                // Combos para Consulta
                cmbMedicoCons.DataSource = medicoBLL.Listar();
                cmbMedicoCons.DisplayMember = "Nombre";
                cmbMedicoCons.ValueMember = "ID";
                cmbMedicoCons.SelectedIndex = -1;

                cmbPacienteCons.DataSource = pacienteBLL.Listar();
                cmbPacienteCons.DisplayMember = "Nombre";
                cmbPacienteCons.ValueMember = "ID";
                cmbPacienteCons.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los listados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarRegistro()
        {
            cmbMedicoReg.SelectedIndex = -1;
            cmbPacienteReg.SelectedIndex = -1;
            cmbMedicamentoReg.SelectedIndex = -1;
            txtSintomas.Text = string.Empty;
            txtRecomendaciones.Text = string.Empty;
            dtpFechaReg.Value = DateTime.Now;
            dtpHoraReg.Value = DateTime.Now;
        }

        private void LimpiarConsulta()
        {
            cmbMedicoCons.SelectedIndex = -1;
            cmbPacienteCons.SelectedIndex = -1;
            chkFiltroFecha.Checked = false;
            dtpFechaCons.Value = DateTime.Now;
            dtpFechaCons.Enabled = false;
            dgvVisitas.DataSource = new List<RegistroVisita>(); // vaciar grilla
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                RegistroVisita visita = new RegistroVisita
                {
                    Fecha_Visita = dtpFechaReg.Value.Date,
                    Hora_Visita = dtpHoraReg.Value.TimeOfDay,
                    Sintomas = txtSintomas.Text.Trim(),
                    Recomendaciones = txtRecomendaciones.Text.Trim(),
                    Estado = true
                };

                if (cmbMedicoReg.SelectedValue != null) visita.MedicoID = (int)cmbMedicoReg.SelectedValue;
                if (cmbPacienteReg.SelectedValue != null) visita.PacienteID = (int)cmbPacienteReg.SelectedValue;
                if (cmbMedicamentoReg.SelectedValue != null) visita.MedicamentoID = (int)cmbMedicamentoReg.SelectedValue;

                bll.Guardar(visita);
                MessageBox.Show("Visita registrada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarRegistro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiarReg_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int? medicoID = cmbMedicoCons.SelectedValue != null ? (int?)cmbMedicoCons.SelectedValue : null;
                int? pacienteID = cmbPacienteCons.SelectedValue != null ? (int?)cmbPacienteCons.SelectedValue : null;
                DateTime? fecha = chkFiltroFecha.Checked ? (DateTime?)dtpFechaCons.Value.Date : null;

                var lista = bll.Consultar(medicoID, pacienteID, fecha);
                dgvVisitas.DataSource = lista;

                // Ocultar IDs crudos en UI
                if (dgvVisitas.Columns["MedicoID"] != null) dgvVisitas.Columns["MedicoID"].Visible = false;
                if (dgvVisitas.Columns["PacienteID"] != null) dgvVisitas.Columns["PacienteID"].Visible = false;
                if (dgvVisitas.Columns["MedicamentoID"] != null) dgvVisitas.Columns["MedicamentoID"].Visible = false;

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros con los criterios especificados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarCons_Click(object sender, EventArgs e)
        {
            LimpiarConsulta();
        }

        private void chkFiltroFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaCons.Enabled = chkFiltroFecha.Checked;
        }
    }
}
