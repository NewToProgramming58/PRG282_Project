namespace PRG2x2_Project
{
    partial class Details
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbcDetails = new System.Windows.Forms.TabControl();
            this.tpgStudents = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnStudentDelete = new System.Windows.Forms.Button();
            this.btnStudentUpdate = new System.Windows.Forms.Button();
            this.btnStudentInsert = new System.Windows.Forms.Button();
            this.btnStudentRead = new System.Windows.Forms.Button();
            this.dgvStudentOutput = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbStudentTutorial = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpgModules = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnModuleDelete = new System.Windows.Forms.Button();
            this.btnModuleUpdate = new System.Windows.Forms.Button();
            this.btnModuleInsert = new System.Windows.Forms.Button();
            this.btnModuleRead = new System.Windows.Forms.Button();
            this.dgvModuleOutput = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rtbModuleTutorial = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbcDetails.SuspendLayout();
            this.tpgStudents.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentOutput)).BeginInit();
            this.panel1.SuspendLayout();
            this.tpgModules.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModuleOutput)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcDetails
            // 
            this.tbcDetails.Controls.Add(this.tpgStudents);
            this.tbcDetails.Controls.Add(this.tpgModules);
            this.tbcDetails.Location = new System.Drawing.Point(0, 0);
            this.tbcDetails.Name = "tbcDetails";
            this.tbcDetails.SelectedIndex = 0;
            this.tbcDetails.Size = new System.Drawing.Size(801, 387);
            this.tbcDetails.TabIndex = 0;
            // 
            // tpgStudents
            // 
            this.tpgStudents.Controls.Add(this.panel2);
            this.tpgStudents.Controls.Add(this.panel1);
            this.tpgStudents.Location = new System.Drawing.Point(4, 22);
            this.tpgStudents.Name = "tpgStudents";
            this.tpgStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tpgStudents.Size = new System.Drawing.Size(793, 361);
            this.tpgStudents.TabIndex = 0;
            this.tpgStudents.Text = "Students";
            this.tpgStudents.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.dgvStudentOutput);
            this.panel2.Location = new System.Drawing.Point(0, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(793, 290);
            this.panel2.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.PeachPuff;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btnStudentDelete);
            this.panel6.Controls.Add(this.btnStudentUpdate);
            this.panel6.Controls.Add(this.btnStudentInsert);
            this.panel6.Controls.Add(this.btnStudentRead);
            this.panel6.Location = new System.Drawing.Point(523, 204);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(270, 86);
            this.panel6.TabIndex = 14;
            // 
            // btnStudentDelete
            // 
            this.btnStudentDelete.ForeColor = System.Drawing.Color.Red;
            this.btnStudentDelete.Location = new System.Drawing.Point(145, 53);
            this.btnStudentDelete.Name = "btnStudentDelete";
            this.btnStudentDelete.Size = new System.Drawing.Size(96, 23);
            this.btnStudentDelete.TabIndex = 13;
            this.btnStudentDelete.Text = "Delete Record";
            this.btnStudentDelete.UseVisualStyleBackColor = true;
            // 
            // btnStudentUpdate
            // 
            this.btnStudentUpdate.Location = new System.Drawing.Point(33, 53);
            this.btnStudentUpdate.Name = "btnStudentUpdate";
            this.btnStudentUpdate.Size = new System.Drawing.Size(91, 23);
            this.btnStudentUpdate.TabIndex = 12;
            this.btnStudentUpdate.Text = "Update Record";
            this.btnStudentUpdate.UseVisualStyleBackColor = true;
            // 
            // btnStudentInsert
            // 
            this.btnStudentInsert.Location = new System.Drawing.Point(145, 14);
            this.btnStudentInsert.Name = "btnStudentInsert";
            this.btnStudentInsert.Size = new System.Drawing.Size(96, 23);
            this.btnStudentInsert.TabIndex = 11;
            this.btnStudentInsert.Text = "Add Record";
            this.btnStudentInsert.UseVisualStyleBackColor = true;
            // 
            // btnStudentRead
            // 
            this.btnStudentRead.Location = new System.Drawing.Point(33, 14);
            this.btnStudentRead.Name = "btnStudentRead";
            this.btnStudentRead.Size = new System.Drawing.Size(91, 23);
            this.btnStudentRead.TabIndex = 10;
            this.btnStudentRead.Text = "Read";
            this.btnStudentRead.UseVisualStyleBackColor = true;
            this.btnStudentRead.Click += new System.EventHandler(this.btnStudentRead_Click);
            // 
            // dgvStudentOutput
            // 
            this.dgvStudentOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudentOutput.Location = new System.Drawing.Point(-1, -2);
            this.dgvStudentOutput.Name = "dgvStudentOutput";
            this.dgvStudentOutput.Size = new System.Drawing.Size(525, 292);
            this.dgvStudentOutput.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.rtbStudentTutorial);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 71);
            this.panel1.TabIndex = 0;
            // 
            // rtbStudentTutorial
            // 
            this.rtbStudentTutorial.Location = new System.Drawing.Point(324, 6);
            this.rtbStudentTutorial.Name = "rtbStudentTutorial";
            this.rtbStudentTutorial.Size = new System.Drawing.Size(460, 54);
            this.rtbStudentTutorial.TabIndex = 1;
            this.rtbStudentTutorial.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student Details";
            // 
            // tpgModules
            // 
            this.tpgModules.Controls.Add(this.panel3);
            this.tpgModules.Controls.Add(this.panel4);
            this.tpgModules.Location = new System.Drawing.Point(4, 22);
            this.tpgModules.Name = "tpgModules";
            this.tpgModules.Padding = new System.Windows.Forms.Padding(3);
            this.tpgModules.Size = new System.Drawing.Size(793, 361);
            this.tpgModules.TabIndex = 1;
            this.tpgModules.Text = "Modules";
            this.tpgModules.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.dgvModuleOutput);
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(793, 290);
            this.panel3.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.PeachPuff;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btnModuleDelete);
            this.panel5.Controls.Add(this.btnModuleUpdate);
            this.panel5.Controls.Add(this.btnModuleInsert);
            this.panel5.Controls.Add(this.btnModuleRead);
            this.panel5.Location = new System.Drawing.Point(523, 204);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(273, 86);
            this.panel5.TabIndex = 19;
            // 
            // btnModuleDelete
            // 
            this.btnModuleDelete.ForeColor = System.Drawing.Color.Red;
            this.btnModuleDelete.Location = new System.Drawing.Point(145, 53);
            this.btnModuleDelete.Name = "btnModuleDelete";
            this.btnModuleDelete.Size = new System.Drawing.Size(96, 23);
            this.btnModuleDelete.TabIndex = 13;
            this.btnModuleDelete.Text = "Delete Record";
            this.btnModuleDelete.UseVisualStyleBackColor = true;
            // 
            // btnModuleUpdate
            // 
            this.btnModuleUpdate.Location = new System.Drawing.Point(33, 53);
            this.btnModuleUpdate.Name = "btnModuleUpdate";
            this.btnModuleUpdate.Size = new System.Drawing.Size(91, 23);
            this.btnModuleUpdate.TabIndex = 12;
            this.btnModuleUpdate.Text = "Update Record";
            this.btnModuleUpdate.UseVisualStyleBackColor = true;
            // 
            // btnModuleInsert
            // 
            this.btnModuleInsert.Location = new System.Drawing.Point(145, 14);
            this.btnModuleInsert.Name = "btnModuleInsert";
            this.btnModuleInsert.Size = new System.Drawing.Size(96, 23);
            this.btnModuleInsert.TabIndex = 11;
            this.btnModuleInsert.Text = "Add Record";
            this.btnModuleInsert.UseVisualStyleBackColor = true;
            // 
            // btnModuleRead
            // 
            this.btnModuleRead.Location = new System.Drawing.Point(33, 14);
            this.btnModuleRead.Name = "btnModuleRead";
            this.btnModuleRead.Size = new System.Drawing.Size(91, 23);
            this.btnModuleRead.TabIndex = 10;
            this.btnModuleRead.Text = "Read";
            this.btnModuleRead.UseVisualStyleBackColor = true;
            // 
            // dgvModuleOutput
            // 
            this.dgvModuleOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModuleOutput.Location = new System.Drawing.Point(-1, -2);
            this.dgvModuleOutput.Name = "dgvModuleOutput";
            this.dgvModuleOutput.Size = new System.Drawing.Size(525, 292);
            this.dgvModuleOutput.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.rtbModuleTutorial);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(793, 71);
            this.panel4.TabIndex = 1;
            // 
            // rtbModuleTutorial
            // 
            this.rtbModuleTutorial.Location = new System.Drawing.Point(324, 6);
            this.rtbModuleTutorial.Name = "rtbModuleTutorial";
            this.rtbModuleTutorial.Size = new System.Drawing.Size(460, 54);
            this.rtbModuleTutorial.TabIndex = 1;
            this.rtbModuleTutorial.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(4, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Module Details";
            // 
            // Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(800, 386);
            this.Controls.Add(this.tbcDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Details_FormClosed);
            this.tbcDetails.ResumeLayout(false);
            this.tpgStudents.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentOutput)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tpgModules.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModuleOutput)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcDetails;
        private System.Windows.Forms.TabPage tpgStudents;
        private System.Windows.Forms.TabPage tpgModules;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtbStudentTutorial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox rtbModuleTutorial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnStudentDelete;
        private System.Windows.Forms.Button btnStudentUpdate;
        private System.Windows.Forms.Button btnStudentInsert;
        private System.Windows.Forms.Button btnStudentRead;
        private System.Windows.Forms.DataGridView dgvStudentOutput;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnModuleDelete;
        private System.Windows.Forms.Button btnModuleUpdate;
        private System.Windows.Forms.Button btnModuleInsert;
        private System.Windows.Forms.Button btnModuleRead;
        private System.Windows.Forms.DataGridView dgvModuleOutput;
    }
}