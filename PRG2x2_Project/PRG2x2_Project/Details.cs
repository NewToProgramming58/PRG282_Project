using PRG2x2_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG2x2_Project
{
    public partial class Details : Form
    {
        public bool StudentModules = false;
        public bool ModuleStudents = false;
        public bool ModuleResources = false;
        ModuleDetailOption moduleFrm = new ModuleDetailOption();

        DataHandler handler = new DataHandler();
        private Login frm;
        public Details()
        {
            InitializeComponent();
            moduleFrm.Hide();
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

        private void Details_Shown(object sender, EventArgs e)
        {
            ShowStudent();
        }

        private void btnStudentRead_Click(object sender, EventArgs e)
        {
            ShowStudent();
        }

        private void btnStudentInsert_Click(object sender, EventArgs e)
        {           
            if (StudentModules)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                ptbStudentImage.Image.Save(@"image.png", ImageFormat.Png);
                byte[] imageArray = System.IO.File.ReadAllBytes(@"image");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                // Date does not work//////////////////////////////////////////////////////////////////////////////////////////////////
                Student st = new Student( 
                    txtStudentName.Text, 
                    txtStudentSurname.Text, 
                    dtpStudentDate.Value, 
                    cboStudentGender.Text,
                    txtStudentPhone.Text, 
                    rtbStudentAddress.Text,
                    base64ImageRepresentation);
                handler.Insert(st);
                MessageBox.Show("Inserted");
            }
        }

        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                Student st = new Student(int.Parse(txtStudentNumber.Text), 
                    txtStudentName.Text, 
                    txtStudentSurname.Text, 
                    dtpStudentDate.Value, 
                    txtStudentPhone.Text, 
                    rtbStudentAddress.Text, 
                    "");
                handler.Update(st);
                MessageBox.Show("Updated");
            }
        }

        private void btnStudentDelete_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            { 
                // REFERENTIAL INTEGRITY PROBLEMS////////////////////////////////////////////////////////////////////////////////////////////
                handler.Delete(Tables.Student, $"WHERE [Student Number] = {int.Parse(txtStudentNumber.Text)}");
                MessageBox.Show("Deleted");
                // Refreshes the table.
                ShowStudent();
            }
        }

        private void btnStudentSearch_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                //Student Modules///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                dgvStudentOutput.DataSource = handler.GetData(Tables.Student, handler.addCondition("Student Number", Operator.Like, int.Parse(txtStudentSearch.Text)));
            }
        }

        private void tbcDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcDetails.SelectedIndex == 0)
            {
                ShowStudent();
            }
            else
            {
                ShowModule();
            }
        }

        private void dgvStudentOutput_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudentOutput.SelectedRows.Count > 0)
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

                    string imageBase = dgvStudentOutput.SelectedRows[0].Cells[7].Value.ToString();
                    imageBase = imageBase.Substring(imageBase.IndexOf(",") + 1);
                    byte[] bytes = Convert.FromBase64String(imageBase);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        ptbStudentImage.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void dgvStudentOutput_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (StudentModules == false)
            {
                ShowStudentModules();
            }
        }

        private void dgvModuleOutput_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvModuleOutput.SelectedRows.Count > 0)
            {
                if (ModuleStudents)
                {//////////////////////////////////////////////////////////////////////////////////////////
                    txtStudentNumber.Text = dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString();
                    txtStudentName.Text = dgvStudentOutput.SelectedRows[0].Cells[1].Value.ToString();
                    txtStudentSurname.Text = dgvStudentOutput.SelectedRows[0].Cells[2].Value.ToString();
                    dtpStudentDate.Value = DateTime.Parse(dgvStudentOutput.SelectedRows[0].Cells[3].Value.ToString());
                    cboStudentGender.Text = dgvStudentOutput.SelectedRows[0].Cells[4].Value.ToString();
                    txtStudentPhone.Text = dgvStudentOutput.SelectedRows[0].Cells[5].Value.ToString();
                    rtbStudentAddress.Text = dgvStudentOutput.SelectedRows[0].Cells[6].Value.ToString();

                    string imageBase = dgvStudentOutput.SelectedRows[0].Cells[7].Value.ToString();
                    imageBase = imageBase.Substring(imageBase.IndexOf(",") + 1);
                    byte[] bytes = Convert.FromBase64String(imageBase);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        ptbStudentImage.Image = Image.FromStream(ms);
                    }
                    lblModuleSearch.Text = "Student Number";
                }
                else if (ModuleResources)
                {

                }
                else
                {
                    txtModuleCode.Text = dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString();
                    txtModuleName.Text = dgvModuleOutput.SelectedRows[0].Cells[1].Value.ToString();
                    rtbModuleDescription.Text = dgvModuleOutput.SelectedRows[0].Cells[2].Value.ToString();
                }
            }
        }

        private void dgvModuleOutput_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            moduleFrm.Show();
            moduleFrm.GetForm(this);
            this.Enabled = false;
        }

        // Methods for changing what is displayed to the user.
        public void ShowStudent()
        {
            StudentModules = false;
            tbcDetails.SelectTab(0);
            dgvStudentOutput.DataSource = handler.GetData(Tables.Student);
            if (dgvStudentOutput.Rows.Count > 0)
            {
                dgvStudentOutput.Rows[0].Selected = true;
            }
            pnlStudent.Show();
            pnlStudentModules.Hide();
            lblStudentSearch.Text = "Student Number:";
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
            lblStudentSearch.Text = "Module code:";
        }

        public void ShowModule()
        {
            dgvModuleOutput.DataSource = handler.GetData(Tables.Module);
            if (dgvModuleOutput.Rows.Count > 0)
            {
                dgvModuleOutput.Rows[0].Selected = true;
            }
            ModuleStudents = false;
            ModuleResources = false;
            //Panels///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            pnlModule.Show();
            pnlModuleStudents.Hide();
        }

        public void ShowModuleDetails(bool student)
        {
            if (student)
            {////////////////////////////////////////////////////////////////////DETAILS
                dgvModuleOutput.DataSource = handler.GetData(Tables.StudentModules, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));
                pnlModuleStudents.Show();
                pnlModule.Hide();
            }
            else
            {/////////////////////////////////////////////////////////////////////DETAILS
                dgvModuleOutput.DataSource = handler.GetData(Tables.Resource, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));
                pnlModuleStudents.Show();//////////////////////////////////////////////
                pnlModule.Hide();
            }
        }
    }
}
