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
        public bool Students = false;
        public bool StudentModules = false;
        public bool Modules = false;
        public bool ModuleStudents = false;
        public bool ModuleResources = false;
        ModuleDetailOption moduleFrm = new ModuleDetailOption();
        int currentStudent = 0;

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
                    $"Gender: \t\t{cboStudentGender.Text}\n" +
                    $"Phone: \t\t{txtStudentPhone.Text}\n" +
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
                StudentModule sm = new StudentModule(currentStudent, dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString(), cboStudentModuleStatus.Text);
                handler.Update(sm);
                ShowStudentModules();
                MessageBox.Show("Updated");
            }
            else
            {////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Converts our image to a format that can be used in SQL server.
                ptbStudentImage.Image.Save(@"image.png", ImageFormat.Png);
                byte[] imageArray = System.IO.File.ReadAllBytes(@"image");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                // Reads the values.
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
                    $"Gender: \t\t{dgvStudentOutput.SelectedRows[0].Cells[4].Value} TO {cboStudentGender.Text}\n" +
                    $"Phone: \t\t{dgvStudentOutput.SelectedRows[0].Cells[5].Value} TO {txtStudentPhone.Text}\n" +
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
                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to delete this record from Students?\n\n" +
                   $"Name: \t\t{dgvStudentOutput.SelectedRows[0].Cells[1].Value}\n" +
                   $"Surname: \t{dgvStudentOutput.SelectedRows[0].Cells[2].Value}\n" +
                   $"Date of Birth: \t{DateTime.Parse(dgvStudentOutput.SelectedRows[0].Cells[3].Value.ToString()).ToString("yyyy/MM/dd")}\n" +
                   $"Gender: \t\t{dgvStudentOutput.SelectedRows[0].Cells[4].Value}\n" +
                   $"Phone: \t\t{dgvStudentOutput.SelectedRows[0].Cells[5].Value}\n" +
                   $"Address: \t\t{dgvStudentOutput.SelectedRows[0].Cells[6].Value}\n\n" +
                   $"NOTE: Deleting this record will delete all modules linked to this student as well.",
                   "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.Student, $"WHERE [Student Number] = {int.Parse(txtStudentNumber.Text)}");
                    ShowStudent();
                }
            }
        }

        private void btnStudentSearch_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {////////////////////////////////////////////////////////////////////////////////////////////////
                DataTable dt = handler.GetData(Tables.StudentModuleDetails, handler.addCondition("Module Code", Operator.Like, txtStudentSearch.Text));
                if (dt.Rows.Count > 0)
                {
                    dgvStudentOutput.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No modules found", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                DataTable dt = handler.GetData(Tables.Student, handler.addCondition("Student Number", Operator.Like, int.Parse(txtStudentSearch.Text)));
                if (dt.Rows.Count > 0)
                {
                    dgvStudentOutput.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No students found", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            // Resets all values that indicate what we should view.
            currentStudent = 0;
            Students = true;
            StudentModules = false;
            Modules = false;
            ModuleStudents = false;
            ModuleResources = false;

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
            // Resets all values that indicate what we should view.
            Students = false;
            StudentModules = true;
            Modules = false;
            ModuleStudents = false;
            ModuleResources = false;

            // Checks if there is already a selected student for if we refresh the datagrid.
            if (currentStudent == 0)
            {
                currentStudent = int.Parse(dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString());
            }
            Student student = new Student(currentStudent);
            dgvStudentOutput.DataSource = handler.GetData(Tables.StudentModuleDetails, "", student);
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
            // Resets all values that indicate what we should view.
            currentStudent = 0;
            Students = false;
            StudentModules = false;
            Modules = true;
            ModuleStudents = false;
            ModuleResources = false;

            dgvModuleOutput.DataSource = handler.GetData(Tables.Module);
            if (dgvModuleOutput.Rows.Count > 0)
            {
                dgvModuleOutput.Rows[0].Selected = true;
            }
            //Panels///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            pnlModule.Show();
            pnlModuleStudents.Hide();
        }

        public void ShowModuleDetails(bool student)
        {
            // Resets all values that indicate what we should view.
            currentStudent = 0;
            Students = false;
            StudentModules = false;
            Modules = false;
            ModuleStudents = false;
            ModuleResources = false;

            // When Details are shown it has to determine which details to show, Students or Resources.
            // For this we make use of student.
            if (student)
            {////////////////////////////////////////////////////////////////////DETAILS
                ModuleStudents = true;
                dgvModuleOutput.DataSource = handler.GetData(Tables.StudentModules, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));
                pnlModuleStudents.Show();
                pnlModule.Hide();
            }
            else
            {/////////////////////////////////////////////////////////////////////DETAILS
                ModuleResources = true;
                dgvModuleOutput.DataSource = handler.GetData(Tables.Resource, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));
                pnlModuleStudents.Show();//////////////////////////////////////////////
                pnlModule.Hide();
            }
        }
        private void btnStudentLast_Click(object sender, EventArgs e)
        {
            if (dgvStudentOutput.CurrentRow != null)
            {
                dgvStudentOutput.CurrentCell =
                    dgvStudentOutput
                    .Rows[dgvStudentOutput.Rows.Count - 2]
                    .Cells[dgvStudentOutput.CurrentCell.ColumnIndex];
            }
        }

        private void btnStudentFirst_Click(object sender, EventArgs e)
        {
            if (dgvStudentOutput.CurrentRow != null)
            {
                dgvStudentOutput.CurrentCell =
                    dgvStudentOutput
                    .Rows[0]
                    .Cells[dgvStudentOutput.CurrentCell.ColumnIndex];
            }
        }

        private void btnStudentPrevious_Click(object sender, EventArgs e)
        {
            if (dgvStudentOutput.SelectedRows[0].Index > 0)
            {
                dgvStudentOutput.CurrentCell = dgvStudentOutput.Rows[dgvStudentOutput.SelectedRows[0].Index - 1].Cells[0];
            }
            else
            {
                btnStudentLast_Click(sender, e);
            }
        }

        private void btnStudentNext_Click(object sender, EventArgs e)
        {
            if (dgvStudentOutput.SelectedRows[0].Index < dgvStudentOutput.Rows.Count - 2)
            {
                dgvStudentOutput.CurrentCell = dgvStudentOutput.Rows[dgvStudentOutput.SelectedRows[0].Index + 1].Cells[0];
            }
            else
            {
                btnStudentFirst_Click(sender, e);
            }
        }

        // Validation method.
        private bool Validate()
        {
            bool noProblems = false;

            if (Students)
            {
                if ((txtStudentNumber.Text == "") || (txtStudentName.Text == "") || (txtStudentSurname.Text == "") || (dtpStudentDate.Value > DateTime.UtcNow.Date) || (cboStudentGender.Text == "") || (txtStudentPhone.Text == "") || (rtbStudentAddress.Text == ""))
                {
                    
                }
            }

            if (StudentModules)
            {

            }

            if (Modules)
            {

            }

            if (ModuleResources)
            {

            }

            if (ModuleStudents)
            {

            }

            return noProblems;
        }
    }
}
