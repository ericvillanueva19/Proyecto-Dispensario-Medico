namespace DispensarioMedico.UI
{
    partial class FrmRegistroVisitas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRegistro = new System.Windows.Forms.TabPage();
            this.tabConsulta = new System.Windows.Forms.TabPage();
            
            // Controles de Registro
            this.lblMedicoReg = new System.Windows.Forms.Label();
            this.cmbMedicoReg = new System.Windows.Forms.ComboBox();
            this.lblPacienteReg = new System.Windows.Forms.Label();
            this.cmbPacienteReg = new System.Windows.Forms.ComboBox();
            this.lblFechaReg = new System.Windows.Forms.Label();
            this.dtpFechaReg = new System.Windows.Forms.DateTimePicker();
            this.lblHoraReg = new System.Windows.Forms.Label();
            this.dtpHoraReg = new System.Windows.Forms.DateTimePicker();
            this.lblSintomas = new System.Windows.Forms.Label();
            this.txtSintomas = new System.Windows.Forms.TextBox();
            this.lblMedicamentoReg = new System.Windows.Forms.Label();
            this.cmbMedicamentoReg = new System.Windows.Forms.ComboBox();
            this.lblRecomendaciones = new System.Windows.Forms.Label();
            this.txtRecomendaciones = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiarReg = new System.Windows.Forms.Button();

            // Controles de Consulta
            this.lblMedicoCons = new System.Windows.Forms.Label();
            this.cmbMedicoCons = new System.Windows.Forms.ComboBox();
            this.lblPacienteCons = new System.Windows.Forms.Label();
            this.cmbPacienteCons = new System.Windows.Forms.ComboBox();
            this.chkFiltroFecha = new System.Windows.Forms.CheckBox();
            this.dtpFechaCons = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiarCons = new System.Windows.Forms.Button();
            this.dgvVisitas = new System.Windows.Forms.DataGridView();

            this.tabControl1.SuspendLayout();
            this.tabRegistro.SuspendLayout();
            this.tabConsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitas)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabRegistro);
            this.tabControl1.Controls.Add(this.tabConsulta);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 500);
            this.tabControl1.TabIndex = 0;

            // 
            // tabRegistro
            // 
            this.tabRegistro.Controls.Add(this.btnLimpiarReg);
            this.tabRegistro.Controls.Add(this.btnGuardar);
            this.tabRegistro.Controls.Add(this.txtRecomendaciones);
            this.tabRegistro.Controls.Add(this.lblRecomendaciones);
            this.tabRegistro.Controls.Add(this.cmbMedicamentoReg);
            this.tabRegistro.Controls.Add(this.lblMedicamentoReg);
            this.tabRegistro.Controls.Add(this.txtSintomas);
            this.tabRegistro.Controls.Add(this.lblSintomas);
            this.tabRegistro.Controls.Add(this.dtpHoraReg);
            this.tabRegistro.Controls.Add(this.lblHoraReg);
            this.tabRegistro.Controls.Add(this.dtpFechaReg);
            this.tabRegistro.Controls.Add(this.lblFechaReg);
            this.tabRegistro.Controls.Add(this.cmbPacienteReg);
            this.tabRegistro.Controls.Add(this.lblPacienteReg);
            this.tabRegistro.Controls.Add(this.cmbMedicoReg);
            this.tabRegistro.Controls.Add(this.lblMedicoReg);
            this.tabRegistro.Location = new System.Drawing.Point(4, 22);
            this.tabRegistro.Name = "tabRegistro";
            this.tabRegistro.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistro.Size = new System.Drawing.Size(792, 474);
            this.tabRegistro.TabIndex = 0;
            this.tabRegistro.Text = "Registro de Visita";
            this.tabRegistro.UseVisualStyleBackColor = true;

            // --- Elementos TabRegistro ---
            this.lblMedicoReg.AutoSize = true;
            this.lblMedicoReg.Location = new System.Drawing.Point(20, 20);
            this.lblMedicoReg.Text = "Médico:";
            
            this.cmbMedicoReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicoReg.Location = new System.Drawing.Point(80, 17);
            this.cmbMedicoReg.Size = new System.Drawing.Size(200, 21);

            this.lblPacienteReg.AutoSize = true;
            this.lblPacienteReg.Location = new System.Drawing.Point(320, 20);
            this.lblPacienteReg.Text = "Paciente:";

            this.cmbPacienteReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacienteReg.Location = new System.Drawing.Point(380, 17);
            this.cmbPacienteReg.Size = new System.Drawing.Size(200, 21);

            this.lblFechaReg.AutoSize = true;
            this.lblFechaReg.Location = new System.Drawing.Point(20, 60);
            this.lblFechaReg.Text = "Fecha:";

            this.dtpFechaReg.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaReg.Location = new System.Drawing.Point(80, 57);
            this.dtpFechaReg.Size = new System.Drawing.Size(120, 20);

            this.lblHoraReg.AutoSize = true;
            this.lblHoraReg.Location = new System.Drawing.Point(220, 60);
            this.lblHoraReg.Text = "Hora:";

            this.dtpHoraReg.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraReg.ShowUpDown = true;
            this.dtpHoraReg.Location = new System.Drawing.Point(260, 57);
            this.dtpHoraReg.Size = new System.Drawing.Size(100, 20);

            this.lblMedicamentoReg.AutoSize = true;
            this.lblMedicamentoReg.Location = new System.Drawing.Point(380, 60);
            this.lblMedicamentoReg.Text = "Medicamento Recetado:";

            this.cmbMedicamentoReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicamentoReg.Location = new System.Drawing.Point(510, 57);
            this.cmbMedicamentoReg.Size = new System.Drawing.Size(200, 21);

            this.lblSintomas.AutoSize = true;
            this.lblSintomas.Location = new System.Drawing.Point(20, 100);
            this.lblSintomas.Text = "Síntomas:";

            this.txtSintomas.Multiline = true;
            this.txtSintomas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSintomas.Location = new System.Drawing.Point(23, 120);
            this.txtSintomas.Size = new System.Drawing.Size(350, 150);

            this.lblRecomendaciones.AutoSize = true;
            this.lblRecomendaciones.Location = new System.Drawing.Point(400, 100);
            this.lblRecomendaciones.Text = "Recomendaciones:";

            this.txtRecomendaciones.Multiline = true;
            this.txtRecomendaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecomendaciones.Location = new System.Drawing.Point(403, 120);
            this.txtRecomendaciones.Size = new System.Drawing.Size(350, 150);

            this.btnGuardar.Location = new System.Drawing.Point(23, 290);
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnLimpiarReg.Location = new System.Drawing.Point(140, 290);
            this.btnLimpiarReg.Size = new System.Drawing.Size(100, 30);
            this.btnLimpiarReg.Text = "Limpiar";
            this.btnLimpiarReg.Click += new System.EventHandler(this.btnLimpiarReg_Click);


            // 
            // tabConsulta
            // 
            this.tabConsulta.Controls.Add(this.dgvVisitas);
            this.tabConsulta.Controls.Add(this.btnLimpiarCons);
            this.tabConsulta.Controls.Add(this.btnBuscar);
            this.tabConsulta.Controls.Add(this.dtpFechaCons);
            this.tabConsulta.Controls.Add(this.chkFiltroFecha);
            this.tabConsulta.Controls.Add(this.cmbPacienteCons);
            this.tabConsulta.Controls.Add(this.lblPacienteCons);
            this.tabConsulta.Controls.Add(this.cmbMedicoCons);
            this.tabConsulta.Controls.Add(this.lblMedicoCons);
            this.tabConsulta.Location = new System.Drawing.Point(4, 22);
            this.tabConsulta.Name = "tabConsulta";
            this.tabConsulta.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsulta.Size = new System.Drawing.Size(792, 474);
            this.tabConsulta.TabIndex = 1;
            this.tabConsulta.Text = "Consulta Avanzada";
            this.tabConsulta.UseVisualStyleBackColor = true;

            // --- Elementos TabConsulta ---
            this.lblMedicoCons.AutoSize = true;
            this.lblMedicoCons.Location = new System.Drawing.Point(20, 20);
            this.lblMedicoCons.Text = "Médico:";

            this.cmbMedicoCons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicoCons.Location = new System.Drawing.Point(70, 17);
            this.cmbMedicoCons.Size = new System.Drawing.Size(200, 21);

            this.lblPacienteCons.AutoSize = true;
            this.lblPacienteCons.Location = new System.Drawing.Point(290, 20);
            this.lblPacienteCons.Text = "Paciente:";

            this.cmbPacienteCons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacienteCons.Location = new System.Drawing.Point(350, 17);
            this.cmbPacienteCons.Size = new System.Drawing.Size(200, 21);

            this.chkFiltroFecha.AutoSize = true;
            this.chkFiltroFecha.Location = new System.Drawing.Point(20, 60);
            this.chkFiltroFecha.Text = "Filtrar por Fecha:";
            this.chkFiltroFecha.CheckedChanged += new System.EventHandler(this.chkFiltroFecha_CheckedChanged);

            this.dtpFechaCons.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCons.Location = new System.Drawing.Point(120, 58);
            this.dtpFechaCons.Size = new System.Drawing.Size(120, 20);
            this.dtpFechaCons.Enabled = false;

            this.btnBuscar.Location = new System.Drawing.Point(270, 55);
            this.btnBuscar.Size = new System.Drawing.Size(100, 25);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);

            this.btnLimpiarCons.Location = new System.Drawing.Point(380, 55);
            this.btnLimpiarCons.Size = new System.Drawing.Size(100, 25);
            this.btnLimpiarCons.Text = "Limpiar Filtros";
            this.btnLimpiarCons.Click += new System.EventHandler(this.btnLimpiarCons_Click);

            this.dgvVisitas.AllowUserToAddRows = false;
            this.dgvVisitas.AllowUserToDeleteRows = false;
            this.dgvVisitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisitas.Location = new System.Drawing.Point(20, 100);
            this.dgvVisitas.Name = "dgvVisitas";
            this.dgvVisitas.ReadOnly = true;
            this.dgvVisitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisitas.Size = new System.Drawing.Size(750, 350);

            // 
            // FrmRegistroVisitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmRegistroVisitas";
            this.Text = "Gestión de Visitas";
            this.Load += new System.EventHandler(this.FrmRegistroVisitas_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabRegistro.ResumeLayout(false);
            this.tabRegistro.PerformLayout();
            this.tabConsulta.ResumeLayout(false);
            this.tabConsulta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRegistro;
        private System.Windows.Forms.TabPage tabConsulta;
        
        private System.Windows.Forms.Label lblMedicoReg;
        private System.Windows.Forms.ComboBox cmbMedicoReg;
        private System.Windows.Forms.Label lblPacienteReg;
        private System.Windows.Forms.ComboBox cmbPacienteReg;
        private System.Windows.Forms.Label lblFechaReg;
        private System.Windows.Forms.DateTimePicker dtpFechaReg;
        private System.Windows.Forms.Label lblHoraReg;
        private System.Windows.Forms.DateTimePicker dtpHoraReg;
        private System.Windows.Forms.Label lblMedicamentoReg;
        private System.Windows.Forms.ComboBox cmbMedicamentoReg;
        private System.Windows.Forms.Label lblSintomas;
        private System.Windows.Forms.TextBox txtSintomas;
        private System.Windows.Forms.Label lblRecomendaciones;
        private System.Windows.Forms.TextBox txtRecomendaciones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiarReg;

        private System.Windows.Forms.Label lblMedicoCons;
        private System.Windows.Forms.ComboBox cmbMedicoCons;
        private System.Windows.Forms.Label lblPacienteCons;
        private System.Windows.Forms.ComboBox cmbPacienteCons;
        private System.Windows.Forms.CheckBox chkFiltroFecha;
        private System.Windows.Forms.DateTimePicker dtpFechaCons;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiarCons;
        private System.Windows.Forms.DataGridView dgvVisitas;
    }
}
