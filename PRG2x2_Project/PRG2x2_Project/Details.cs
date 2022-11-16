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
                // Converts our image to a format that can be used in SQL server.
                ptbStudentImage.Image.Save(@"image.png", ImageFormat.Png);
                byte[] imageArray = System.IO.File.ReadAllBytes(@"image");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                // Reads the values.
                Student st = new Student( 
                    txtStudentName.Text, 
                    txtStudentSurname.Text, 
                    dtpStudentDate.Value, 
                    cboStudentGender.Text,
                    txtStudentPhone.Text, 
                    rtbStudentAddress.Text,
                    base64ImageRepresentation);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Students?\n\n" +
                    $"Name: \t\t{txtStudentName.Text}\n" +
                    $"Surname: \t{txtStudentSurname.Text}\n" +
                    $"Date of Birth: \t{dtpStudentDate.Value.ToString("yyyy/MM/dd")}\n" +
                    $"Gender: \t\t{cboStudentGender.Text}" +
                    $"\nPhone: \t\t{txtStudentPhone.Text}\n" +
                    $"Address: \t\t{rtbStudentAddress.Text}", "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Insert(st);
                    ShowStudent();
                    dgvStudentOutput.Rows[dgvStudentOutput.Rows.Count - 2].Selected = true;
                    if (dgvStudentOutput.CurrentRow != null)
                    {
                        dgvStudentOutput.CurrentCell =
                            dgvStudentOutput
                            .Rows[dgvStudentOutput.Rows.Count - 2]
                            .Cells[dgvStudentOutput.CurrentCell.ColumnIndex];
                    }
                }
            }
        }

        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Converts our image to a format that can be used in SQL server.
                ptbStudentImage.Image.Save(@"image.png", ImageFormat.Png);
                byte[] imageArray = System.IO.File.ReadAllBytes(@"image");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                Student st = new Student(int.Parse(txtStudentNumber.Text), 
                    txtStudentName.Text, 
                    txtStudentSurname.Text, 
                    dtpStudentDate.Value,
                    cboStudentGender.Text,
                    txtStudentPhone.Text, 
                    rtbStudentAddress.Text,
                    base64ImageRepresentation);
                

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to update this record from Students?\n\n" +
                    $"Name: \t\t{dgvStudentOutput.SelectedRows[0].Cells[1].Value} TO {txtStudentName.Text}\n" +
                    $"Surname: \t{dgvStudentOutput.SelectedRows[0].Cells[2].Value} TO {txtStudentSurname.Text}\n" +
                    $"Date of Birth: \t{DateTime.Parse(dgvStudentOutput.SelectedRows[0].Cells[3].Value.ToString()).ToString("yyyy/MM/dd")} TO {dtpStudentDate.Value.ToString("yyyy/MM/dd")}\n" +
                    $"Gender: \t\t{dgvStudentOutput.SelectedRows[0].Cells[4].Value} TO {cboStudentGender.Text}" +
                    $"\nPhone: \t\t{dgvStudentOutput.SelectedRows[0].Cells[5].Value} TO {txtStudentPhone.Text}\n" +
                    $"Address: \t\t{dgvStudentOutput.SelectedRows[0].Cells[6].Value} TO {rtbStudentAddress.Text}", 
                    "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Update(st);
                    int index = dgvStudentOutput.SelectedRows[0].Index;
                    ShowStudent();
                    dgvStudentOutput.Rows[index].Selected = true;
                    if (dgvStudentOutput.CurrentRow != null)
                    {
                        dgvStudentOutput.CurrentCell =
                            dgvStudentOutput
                            .Rows[index]
                            .Cells[dgvStudentOutput.CurrentCell.ColumnIndex];
                    }
                }
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
            // There is a period when changing selection, when there is no record selected, which would give an error, so we test it.
            if (dgvStudentOutput.SelectedRows.Count > 0)
            {
                // We make use of different inputs depending on what table is currently displayed on the datagridview.
                // The Students or their module details.
                // We make use of StudentModules to determine that.
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

                    // Convert the base 64 string to a stream and the stream to an image that can be displayed on the form.
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
            // We check if a student's module details are already displayed, so that the user can't double click it again.
            if (StudentModules == false)
            {
                ShowStudentModules();
            }
        }

        private void dgvModuleOutput_SelectionChanged(object sender, EventArgs e)
        {
            // There is a period when changing selection, when there is no record selected, which would give an error, so we test it.
            if (dgvModuleOutput.SelectedRows.Count > 0)
            {
                // We make use of different inputs depending on what table is currently displayed on the datagridview.
                // The Modules or their details regarding students or resources.
                // We make use of ModuleStudents and ModuleResources to determine that.
                if (ModuleStudents)
                {//////////////////////////////////////////////////////////////////////////////////////////
                    txtStudentNumber.Text = dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString();
                    txtStudentName.Text = dgvStudentOutput.SelectedRows[0].Cells[1].Value.ToString();
                    txtStudentSurname.Text = dgvStudentOutput.SelectedRows[0].Cells[2].Value.ToString();
                    dtpStudentDate.Value = DateTime.Parse(dgvStudentOutput.SelectedRows[0].Cells[3].Value.ToString());
                    cboStudentGender.Text = dgvStudentOutput.SelectedRows[0].Cells[4].Value.ToString();
                    txtStudentPhone.Text = dgvStudentOutput.SelectedRows[0].Cells[5].Value.ToString();
                    rtbStudentAddress.Text = dgvStudentOutput.SelectedRows[0].Cells[6].Value.ToString();

                    // Convert the base 64 string to a stream and the stream to an image that can be displayed on the form.
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
            // When double clicking a module, a form is going to display to ask which details the user wants to see.
            moduleFrm.Show();
            moduleFrm.GetForm(this);
            this.Enabled = false;
        }

        // Methods for changing what is displayed to the user, depending on what table is shown.
        // This is because multiple types of tables are shown on a single form.
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
