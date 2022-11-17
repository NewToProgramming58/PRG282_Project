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
        int currentStudent = 0;

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

        private void Details_Shown(object sender, EventArgs e)
        {
            ShowStudent();
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

// Operations on Student page
//==================================================================================================================================================
        
        //REFRESH
        private void btnStudentRead_Click(object sender, EventArgs e)
        {
            ShowStudent();
            txtStudentSearch.Text = "";
        }

        //INSERT
        private void btnStudentInsert_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                // We have to make sure no duplicate modules are entered so we create a list and see if our newly added trecord is already in the list.
                List<string> modules = new List<string>();
                for (int i = 0; i < dgvStudentOutput.Rows.Count - 1; i++)
                {
                    modules.Add(dgvStudentOutput.Rows[i].Cells[0].Value.ToString());
                }

                if (modules.Contains(cboStudentModuleCode.Text))
                {
                    MessageBox.Show("The Module already exists under this user, please try another one.", "Module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    StudentModule sm = new StudentModule(currentStudent, cboStudentModuleCode.Text, cboStudentModuleStatus.Text);

                    // Asks the user if he/she is sure.
                    DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into StudentModules?\n\n" +
                         $"Module Code: \t{cboStudentModuleCode.Text}\n" +
                         $"Status: \t\t{cboStudentModuleStatus.Text}\n",
                         "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                    if (result == DialogResult.Yes)
                    {
                        handler.Insert(sm);
                        ShowStudentModules();
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
            else
            {
                // Converts our image to a format that can be used in SQL server.
                string base64ImageRepresentation;
                using (var ms = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(ptbStudentImage.Image))
                    {
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        base64ImageRepresentation = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                    }
                }

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
                    $"Address: \t\t{rtbStudentAddress.Text}",
                    "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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

        //UPDATE
        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                int index = dgvStudentOutput.SelectedRows[0].Index;
                StudentModule sm = new StudentModule(currentStudent, dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString(), cboStudentModuleStatus.Text);
                DialogResult changeResult = DialogResult.No;
                if (cboStudentModuleCode.Text != dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString())
                {
                    changeResult = MessageBox.Show($"You changed the Module code when wanting to update.\n" +
                        $"Instead of updating the Module Code try to insert a new field.\n" +
                        $"Would you still like to change the status from {dgvStudentOutput.SelectedRows[0].Cells[3].Value} To {cboStudentModuleStatus.Text}?",
                        "Update problem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }

                if (changeResult == DialogResult.Yes)
                {
                    handler.Update(sm);
                    ShowStudentModules();
                }
                else
                {
                    DialogResult result = MessageBox.Show($"Are you sure you want to update this record from StudentModules?\n\n" +
                        $"Status: {dgvStudentOutput.SelectedRows[0].Cells[3].Value} To {cboStudentModuleStatus.Text}",
                        "Update problem", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        handler.Update(sm);
                        ShowStudentModules();
                    }
                }

                dgvStudentOutput.Rows[index].Selected = true;
                if (dgvStudentOutput.CurrentRow != null)
                {
                    dgvStudentOutput.CurrentCell =
                        dgvStudentOutput
                        .Rows[index]
                        .Cells[dgvStudentOutput.CurrentCell.ColumnIndex];
                }
            }
            else
            {
                // Converts our image to a format that can be used in SQL server.
                string base64ImageRepresentation;
                using (var ms = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(ptbStudentImage.Image))
                    {
                        bitmap.Save(ms, ImageFormat.Jpeg);
                        base64ImageRepresentation = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                    }
                }

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

        //DELETE
        private void btnStudentDelete_Click(object sender, EventArgs e)
        {
            if (StudentModules)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to delete this record from StudentModules?\n\n" +
                  $"Module Code: \t{dgvStudentOutput.SelectedRows[0].Cells[0].Value}\n" +
                  $"Name: \t\t{dgvStudentOutput.SelectedRows[0].Cells[1].Value}\n" +
                  $"Description: \n{dgvStudentOutput.SelectedRows[0].Cells[2].Value}\n" +
                  $"Status: \t\t{dgvStudentOutput.SelectedRows[0].Cells[3].Value}\n",
                  "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.StudentModules, handler.addCondition("Module Code", Operator.Equals, dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString()));
                    ShowStudentModules();
                }
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
                   "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.Student, $"WHERE [Student Number] = {int.Parse(txtStudentNumber.Text)}");///////////////////////////////////////////////
                    ShowStudent();
                }
            }
        }

        //SEARCH
        private void btnStudentSearch_Click(object sender, EventArgs e)
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

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        // Dynamically Updates the input values when selection changes.
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
                    cboStudentModuleCode.Text = dgvStudentOutput.SelectedRows[0].Cells[0].Value.ToString();
                    rtbModuleDetailStudent.Text = dgvStudentOutput.SelectedRows[0].Cells[1].Value.ToString() + "\n\n" + dgvStudentOutput.SelectedRows[0].Cells[2].Value.ToString();
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

        // Views Relationships when double clicking a field.
        private void dgvStudentOutput_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // We check if a student's module details are already displayed, so that the user can't double click it again.
            if (StudentModules == false)
            {
                ShowStudentModules();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------
        
        //NAVIGATION
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

//==================================================================================================================================================

        // Dynamically Updates the input values when selection changes.
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

        // Views Relationships when double clicking a field.
        private void dgvModuleOutput_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // When double clicking a module, a form is going to display to ask which details the user wants to see.
            ModuleDetailOption moduleFrm = new ModuleDetailOption();
            moduleFrm.Show();
            moduleFrm.GetForm(this);
            this.Enabled = false;
        }

// Methods for changing what is displayed to the user, depending on what table is shown.
// This is because multiple types of tables are shown on a single form.
//===========================================================================================================================================
        public void ShowStudent()
        {
            currentStudent = 0;
            StudentModules = false;
            ModuleResources = false;
            ModuleStudents = false;

            tbcDetails.SelectTab(0);
            dgvStudentOutput.DataSource = handler.GetData(Tables.Student);
            if (dgvStudentOutput.Rows.Count > 0)
            {
                dgvStudentOutput.Rows[0].Selected = true;
            }
            pnlStudent.Show();
            pnlStudentModules.Hide();
            pnlStudentSearch.Show();

            dgvStudentOutput.Columns[dgvStudentOutput.Columns.Count - 1].Visible = false;
        }

        public void ShowStudentModules()
        {
            StudentModules = true;
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
            cboStudentModuleCode.DataSource = handler.GetData(Tables.Module);
            cboStudentModuleCode.DisplayMember = "Module Code";
            cboStudentModuleCode.ValueMember = "Module Code";           
            pnlStudentModules.Show();
            pnlStudent.Hide();
            pnlStudentSearch.Hide();
        }

        public void ShowModule()
        {
            ModuleStudents = false;
            ModuleResources = false;
            StudentModules = false;

            dgvModuleOutput.DataSource = handler.GetData(Tables.Module);
            if (dgvModuleOutput.Rows.Count > 0)
            {
                dgvModuleOutput.Rows[0].Selected = true;
            }
            pnlModule.Show();
            pnlModuleStudents.Hide();
            pnlModuleResources.Hide();
        }

        public void ShowModuleDetails(bool student)
        {
            // When Details are shown it has to determine which details to show, Students or Resources.
            // For this we make use of student.
            if (student)
            {////////////////////////////////////////////////////////////////////DETAILS
                dgvModuleOutput.DataSource = handler.GetData(Tables.StudentModules, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));
                pnlModuleStudents.Show();
                pnlModule.Hide();
            }
            else
            {/////////////////////////////////////////////////////////////////////DETAILS
                dgvModuleOutput.DataSource = handler.GetData(Tables.Resource, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));
                pnlModuleResources.Show();
                pnlModule.Hide();
            }
        }
//===========================================================================================================================================

        private void Details_Activated(object sender, EventArgs e)
        {

        }
        private void cboStudentModuleCode_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = handler.GetData(Tables.Module, handler.addCondition("Module Code", Operator.Equals, cboStudentModuleCode.Text));
            if (dt.Rows.Count > 0)
            {
                rtbModuleDetailStudent.Text = $"{dt.Rows[0][1]}\n\n{dt.Rows[0][2]}";
            }
        }

        private void btnModuleInsert_Click(object sender, EventArgs e)
        {
            if (ModuleResources)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                // Reads the values.
                Module module = new Module(txtModuleCode.Text, txtModuleName.Text, rtbModuleDescription.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Modules?\n\n" +
                    $"Module Code: \t{module.Code}\n\n" +
                    $"Name: \t\t{module.Name}\n\n" +
                    $"Description: \n{module.Description}"
                    , "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Insert(module);
                    ShowModule();
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //dgvModuleOutput.Rows[dgvModuleOutput.Rows.Count - 2].Selected = true;
                    //if (dgvModuleOutput.CurrentRow != null)
                    //{
                    //    dgvModuleOutput.CurrentCell =
                    //        dgvModuleOutput
                    //        .Rows[dgvModuleOutput.Rows.Count - 2]
                    //        .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
                    //}
                }
            }
        }

        private void btnModuleUpdate_Click(object sender, EventArgs e)
        {
            if (ModuleResources)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                // Reads the values.
                Module module = new Module(dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString(), txtModuleName.Text, rtbModuleDescription.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Students?\n\n" +
                    $"Module Code: \t{dgvModuleOutput.SelectedRows[0].Cells[0].Value}\n\n" +
                    $"Name: \t\t{module.Name}\n\n" +
                    $"Description: \n{module.Description}"
                    , "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Update(module);
                    ShowModule();
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //dgvModuleOutput.Rows[dgvModuleOutput.Rows.Count - 2].Selected = true;
                    //if (dgvModuleOutput.CurrentRow != null)
                    //{
                    //    dgvModuleOutput.CurrentCell =
                    //        dgvModuleOutput
                    //        .Rows[dgvModuleOutput.Rows.Count - 2]
                    //        .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
                    //}
                }
            }
        }

        private void btnModuleRead_Click(object sender, EventArgs e)
        {
            ShowModule();
            txtModuleSearch.Text = "";
        }

        private void btnModuleDelete_Click(object sender, EventArgs e)
        {
            if (ModuleResources)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                // Reads the values.
                Module module = new Module(dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString(), txtModuleName.Text, rtbModuleDescription.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Students?\n\n" +
                    $"Module Code: \t{module.Code}\n\n" +
                    $"Name: \t\t{module.Name}\n\n" +
                    $"Description: \n{module.Description}"
                    , "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.Module, handler.addCondition("Module Code", Operator.Equals, module.Code));
                    ShowModule();
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //dgvModuleOutput.Rows[dgvModuleOutput.Rows.Count - 2].Selected = true;
                    //if (dgvModuleOutput.CurrentRow != null)
                    //{
                    //    dgvModuleOutput.CurrentCell =
                    //        dgvModuleOutput
                    //        .Rows[dgvModuleOutput.Rows.Count - 2]
                    //        .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
                    //}
                }
            }
        }

        // This button allows us to upload an image to our picturebox.
        private void btnStudentUploadFile_Click(object sender, EventArgs e)
        {
            // Create a OpenFileDialog so the user can browse their files.
            OpenFileDialog fdl = new OpenFileDialog();
            // Sets the initial place the openfiledialog starts searching for files.
            fdl.InitialDirectory = @"C:\Users\jacqu\Pictures\";
            fdl.Title = "Image Select";
            // This filters it so only images of type jpeg, jpg, or png can be selected and seen.
            fdl.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            // If user enters a custom filename or path, it first checks if it exists.
            fdl.CheckFileExists = true;
            fdl.CheckPathExists = true;

            if (fdl.ShowDialog() == DialogResult.OK)
            {
                ptbStudentImage.Image = Image.FromFile(fdl.FileName);
            }
        }

        private void btnModuleSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = handler.GetData(Tables.Module, handler.addCondition("Module Code", Operator.Like, txtModuleSearch.Text));
            if (dt.Rows.Count > 0)
            {
                dgvModuleOutput.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No Modules found", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
