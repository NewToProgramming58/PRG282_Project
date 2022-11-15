﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG2x2_Project
{
    public partial class Details : Form
    {
        bool StudentModules = false;
        DataHandler handler = new DataHandler();
        private Login frm;
        public Details()
        {
            InitializeComponent();
        }

        private void Details_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Show();
            this.Hide();
        }

        public void StoreLoginForm(Login form)
        {
            frm = form;
        }

        private void btnStudentRead_Click(object sender, EventArgs e)
        {
            ShowStudentModules();///////////////////////////////////////////////////////////////////
        }

        private void btnStudentInsert_Click(object sender, EventArgs e)
        {
            ShowStudent();//////////////////////////////////////////////////////////////////////////
        }


        private void Details_Shown(object sender, EventArgs e)
        {
            ShowStudent();
        }

        private void dgvStudentOutput_SelectionChanged(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                txtStudentModuleCode.Text = dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString();
                txtStudentModuleName.Text = dgvStudentOutput.SelectedRows[0].Cells[1].Value.ToString();
                rtbStudentModuleDescription.Text = dgvStudentOutput.SelectedRows[0].Cells[2].Value.ToString();
                cboStudentModuleStatus.Text = dgvStudentOutput.SelectedRows[0].Cells[3].Value.ToString();
            }
            else
            {
                txtStudentNumber.Text = dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString();
                txtStudentName.Text = dgvStudentOutput.SelectedRows[0].Cells[1].Value.ToString();
                txtStudentSurname.Text = dgvStudentOutput.SelectedRows[0].Cells[2].Value.ToString();
                dtpStudentDate.Value = DateTime.Parse(dgvStudentOutput.SelectedRows[0].Cells[3].Value.ToString());
                cboStudentGender.Text = dgvStudentOutput.SelectedRows[0].Cells[4].Value.ToString();
                txtStudentPhone.Text = dgvStudentOutput.SelectedRows[0].Cells[5].Value.ToString();
                rtbStudentAddress.Text = dgvStudentOutput.SelectedRows[0].Cells[6].Value.ToString();
                ptbStudentImage.Image = Image.FromFile(dgvStudentOutput.SelectedRows[0].Cells[7].Value.ToString());
            }
        }

        // Methods for changing what is displayed to the user.
        public void ShowStudent()
        {
            StudentModules = false;
            tbcDetails.SelectTab(0);
            // DISPLAY STUDENTS.////////////////////////////////////////////////////////////////////////////////////////////////
            if (dgvStudentOutput.Rows.Count > 0)
            {
                dgvStudentOutput.Rows[0].Selected = true;
            }
            pnlStudent.Show();
            pnlStudentModules.Hide();
            lblSearch.Text = "Student Number:";
        }

        public void ShowStudentModules()
        {
            StudentModules = true;
            // DISPLAY Module code, module name, module description, status////////////////////////////////////////////////////
            if (dgvStudentOutput.Rows.Count > 0)
            {
                dgvStudentOutput.Rows[0].Selected = true;
            }
            pnlStudentModules.Show();
            pnlStudent.Hide();
            lblSearch.Text = "Module code:";
        }
    }
}
