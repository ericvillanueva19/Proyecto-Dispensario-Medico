namespace DispensarioMedico.UI
{
    partial class FrmUbicacion
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
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblEstante = new System.Windows.Forms.Label();
            this.txtEstante = new System.Windows.Forms.TextBox();
            this.lblTramo = new System.Windows.Forms.Label();
            this.txtTramo = new System.Windows.Forms.TextBox();
            this.lblCelda = new System.Windows.Forms.Label();
            this.txtCelda = new System.Windows.Forms.TextBox();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnInactivar = new System.Windows.Forms.Button();
            this.dgvUbicacion = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUbicacion)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(23, 27);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 0;
            this.lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(95, 24);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(434, 20);
            this.txtDescripcion.TabIndex = 1;
            // 
            // lblEstante
            // 
            this.lblEstante.AutoSize = true;
            this.lblEstante.Location = new System.Drawing.Point(23, 60);
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(46, 13);
            this.lblEstante.TabIndex = 2;
            this.lblEstante.Text = "Estante:";
            // 
            // txtEstante
            // 
            this.txtEstante.Location = new System.Drawing.Point(95, 57);
            this.txtEstante.Name = "txtEstante";
            this.txtEstante.Size = new System.Drawing.Size(100, 20);
            this.txtEstante.TabIndex = 3;
            // 
            // lblTramo
            // 
            this.lblTramo.AutoSize = true;
            this.lblTramo.Location = new System.Drawing.Point(205, 60);
            this.lblTramo.Name = "lblTramo";
            this.lblTramo.Size = new System.Drawing.Size(40, 13);
            this.lblTramo.TabIndex = 4;
            this.lblTramo.Text = "Tramo:";
            // 
            // txtTramo
            // 
            this.txtTramo.Location = new System.Drawing.Point(251, 57);
            this.txtTramo.Name = "txtTramo";
            this.txtTramo.Size = new System.Drawing.Size(100, 20);
            this.txtTramo.TabIndex = 5;
            // 
            // lblCelda
            // 
            this.lblCelda.AutoSize = true;
            this.lblCelda.Location = new System.Drawing.Point(367, 60);
            this.lblCelda.Name = "lblCelda";
            this.lblCelda.Size = new System.Drawing.Size(37, 13);
            this.lblCelda.TabIndex = 6;
            this.lblCelda.Text = "Celda:";
            // 
            // txtCelda
            // 
            this.txtCelda.Location = new System.Drawing.Point(410, 57);
            this.txtCelda.Name = "txtCelda";
            this.txtCelda.Size = new System.Drawing.Size(100, 20);
            this.txtCelda.TabIndex = 7;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.Location = new System.Drawing.Point(530, 60);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(59, 17);
            this.chkEstado.TabIndex = 8;
            this.chkEstado.Text = "Activo";
            this.chkEstado.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(26, 95);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(107, 95);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(188, 95);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnInactivar
            // 
            this.btnInactivar.Location = new System.Drawing.Point(269, 95);
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.Size = new System.Drawing.Size(75, 23);
            this.btnInactivar.TabIndex = 12;
            this.btnInactivar.Text = "Inactivar";
            this.btnInactivar.UseVisualStyleBackColor = true;
            this.btnInactivar.Click += new System.EventHandler(this.btnInactivar_Click);
            // 
            // dgvUbicacion
            // 
            this.dgvUbicacion.AllowUserToAddRows = false;
            this.dgvUbicacion.AllowUserToDeleteRows = false;
            this.dgvUbicacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUbicacion.Location = new System.Drawing.Point(26, 134);
            this.dgvUbicacion.MultiSelect = false;
            this.dgvUbicacion.Name = "dgvUbicacion";
            this.dgvUbicacion.ReadOnly = true;
            this.dgvUbicacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUbicacion.Size = new System.Drawing.Size(563, 250);
            this.dgvUbicacion.TabIndex = 13;
            this.dgvUbicacion.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUbicacion_CellDoubleClick);
            // 
            // FrmUbicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 410);
            this.Controls.Add(this.dgvUbicacion);
            this.Controls.Add(this.btnInactivar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.chkEstado);
            this.Controls.Add(this.txtCelda);
            this.Controls.Add(this.lblCelda);
            this.Controls.Add(this.txtTramo);
            this.Controls.Add(this.lblTramo);
            this.Controls.Add(this.txtEstante);
            this.Controls.Add(this.lblEstante);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Name = "FrmUbicacion";
            this.Text = "Mantenimiento de Ubicaciones";
            this.Load += new System.EventHandler(this.FrmUbicacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUbicacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblEstante;
        private System.Windows.Forms.TextBox txtEstante;
        private System.Windows.Forms.Label lblTramo;
        private System.Windows.Forms.TextBox txtTramo;
        private System.Windows.Forms.Label lblCelda;
        private System.Windows.Forms.TextBox txtCelda;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnInactivar;
        private System.Windows.Forms.DataGridView dgvUbicacion;
    }
}
