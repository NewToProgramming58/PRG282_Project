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
        public bool students = false;
        public bool modules = false;
        int currentStudent = 0;
        string currentModule = "";

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
            if (ValidateInput())
            {
                return;
            }

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
                        dgvStudentOutput.Rows[dgvStudentOutput.Rows.Count - 1].Selected = true;
                        if (dgvStudentOutput.CurrentRow != null)
                        {
                            dgvStudentOutput.CurrentCell =
                            dgvStudentOutput
                            .Rows[dgvStudentOutput.Rows.Count - 1]
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
                    dgvStudentOutput.Rows[dgvStudentOutput.Rows.Count - 1].Selected = true;
                    if (dgvStudentOutput.CurrentRow != null)
                    {
                        dgvStudentOutput.CurrentCell =
                            dgvStudentOutput
                            .Rows[dgvStudentOutput.Rows.Count - 1]
                            .Cells[dgvStudentOutput.CurrentCell.ColumnIndex];
                    }
                }
            }
        }

        //UPDATE
        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                return;
            }

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

                    if (changeResult == DialogResult.Yes)
                    {
                        handler.Update(sm);
                        ShowStudentModules();
                    }

                    return;
                }

                DialogResult result = MessageBox.Show($"Are you sure you want to update this record from StudentModules?\n\n" +
                    $"Status: {dgvStudentOutput.SelectedRows[0].Cells[3].Value} To {cboStudentModuleStatus.Text}",
                    "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    handler.Update(sm);
                    ShowStudentModules();
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
            try
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
            catch (Exception)
            {
                MessageBox.Show("The searched value must be a number", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Updates the richtextbox values when module code changes on StudentModules.
        private void cboStudentModuleCode_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = handler.GetData(Tables.Module, handler.addCondition("Module Code", Operator.Equals, cboStudentModuleCode.Text));
            if (dt.Rows.Count > 0)
            {
                rtbModuleDetailStudent.Text = $"{dt.Rows[0][1]}\n\n{dt.Rows[0][2]}";
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
                    .Rows[dgvStudentOutput.Rows.Count - 1]
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
            if (dgvStudentOutput.SelectedRows[0].Index < dgvStudentOutput.Rows.Count - 1)
            {
                dgvStudentOutput.CurrentCell = dgvStudentOutput.Rows[dgvStudentOutput.SelectedRows[0].Index + 1].Cells[0];
            }
            else
            {
                btnStudentFirst_Click(sender, e);
            }
        }

//==================================================================================================================================================


// Operations on Module page
//==================================================================================================================================================

        //REFRESH
        private void btnModuleRead_Click(object sender, EventArgs e)
        {
            ShowModule();
            txtModuleSearch.Text = "";
        }

        //INSERT
        private void btnModuleInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                return;
            }

            if (ModuleResources)
            {
                // Reads the values.
                Resource resource = new Resource(rtbModuleResource.Text, currentModule);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Resources?\n\n" +
                    $"Module Code: \t{resource.Code}\n" +
                    $"Resource: \n\n{resource.Coderesource}"
                    , "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Insert(resource);
                    ShowModuleDetails(false);
                }
            }
            else if (ModuleStudents)
            {
                StudentModule sm = new StudentModule(int.Parse(txtModuleStudentNumber.Text), currentModule, cboModuleStudentStatus.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Module: {sm.ModuleCode}?\n\n" +
                    $"Student number: \t{sm.StudentNumber}\n" +
                    $"Status: \t\t{sm.Status}"
                    , "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Insert(sm);
                    ShowModuleDetails(true);
                }
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
                }
            }
        }

        //UPDATE
        private void btnModuleUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                return;
            }

            if (ModuleResources)
            {
                // Reads the values.
                Resource resource = new Resource(rtbModuleResource.Text, currentModule);
                //dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString()
                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to update this record from Modules?\n\n" +
                $"Module Code: \t{resource.Code}\n" +
                $"Resource: \n\n{dgvModuleOutput.SelectedRows[0].Cells[1].Value}\n\nTO\n\n{resource.Coderesource}",
                "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Update(resource, dgvModuleOutput.SelectedRows[0].Cells[1].Value.ToString());

                    int index = dgvModuleOutput.SelectedRows[0].Index;

                    ShowModuleDetails(false);
                    dgvModuleOutput.Rows[index].Selected = true;
                    if (dgvModuleOutput.CurrentRow != null)
                    {
                        dgvModuleOutput.CurrentCell =
                            dgvModuleOutput
                            .Rows[index]
                            .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
                    }
                }
            }
            else if (ModuleStudents)
            {
                StudentModule studentModule = new StudentModule(int.Parse(dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString()), currentModule, cboModuleStudentStatus.Text);
                // We have to make sure no duplicate modules are entered so we create a list and see if our newly added trecord is already in the list.
                DialogResult changeResult = DialogResult.No;
                if (cboStudentModuleCode.Text != dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString())
                {
                    changeResult = MessageBox.Show($"You changed the Student Number when wanting to update.\n" +
                        $"Instead of updating the Student Number try to insert a new field.\n" +
                        $"Would you still like to change the status from {dgvModuleOutput.SelectedRows[0].Cells[3].Value} To {cboModuleStudentStatus.Text}?",
                        "Update problem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (changeResult == DialogResult.Yes)
                    {
                        handler.Update(studentModule);
                        ShowModuleDetails(true);
                    }

                    return;
                }
                
                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to update this record drom Student Modules?\n\n" +
                $"Student Number: \t{studentModule.StudentNumber}\n" +
                $"Module Code: \t{currentModule}\n" +
                $"Status: \t\t{dgvModuleOutput.SelectedRows[0].Cells[3].Value} TO {studentModule.Status}",
                "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Update(studentModule);

                    int index = dgvModuleOutput.SelectedRows[0].Index;

                    ShowModuleDetails(true);
                    dgvModuleOutput.Rows[index].Selected = true;
                    if (dgvModuleOutput.CurrentRow != null)
                    {
                        dgvModuleOutput.CurrentCell =
                            dgvModuleOutput
                            .Rows[index]
                            .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
                    }
                }

            }
            else
            {
                // Reads the values.
                Module module = new Module(dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString(), txtModuleName.Text, rtbModuleDescription.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to update this record from Modules?\n\n" +
                $"Module Code: \t{dgvModuleOutput.SelectedRows[0].Cells[0].Value} TO {module.Code}\n" +
                $"Name: \t{dgvModuleOutput.SelectedRows[0].Cells[1].Value} TO {module.Name}\n\n" +
                $"Description: \n{dgvModuleOutput.SelectedRows[0].Cells[2].Value}\n\nTO\n\n{module.Description}",
                "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Update(module);

                    int index = dgvModuleOutput.SelectedRows[0].Index;

                    ShowModule();
                    dgvModuleOutput.Rows[index].Selected = true;
                    if (dgvModuleOutput.CurrentRow != null)
                    {
                        dgvModuleOutput.CurrentCell =
                            dgvModuleOutput
                            .Rows[index]
                            .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
                    }
                }
            }
        }

        //DELETE
        private void btnModuleDelete_Click(object sender, EventArgs e)
        {
            if (ModuleResources)
            {
                // Reads the values.
                Resource resource = new Resource(dgvModuleOutput.SelectedRows[0].Cells[1].Value.ToString(), currentModule);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to delete this record from Resources?\n\n" +
                    $"Module Code: \t{resource.Code}\n" +
                    $"Resource:\n\n{resource.Coderesource}"
                    , "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.Resource, "", resource);
                    ShowModuleDetails(false);
                }
            }
            else if (ModuleStudents)
            {
                // Reads the values.
                StudentModule sm = new StudentModule(int.Parse(txtModuleStudentNumber.Text), currentModule, cboModuleStudentStatus.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to insert this record into Module: {sm.ModuleCode}?\n\n" +
                    $"Student number: \t{sm.StudentNumber}\n" +
                    $"Status: \t\t{sm.Status}"
                    , "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.StudentModules, handler.addCondition("Student Number", Operator.Equals, int.Parse(txtModuleStudentNumber.Text)), sm);
                    ShowModuleDetails(true);
                }
            }
            else
            {
                // Reads the values.
                Module module = new Module(dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString(), txtModuleName.Text, rtbModuleDescription.Text);

                // Asks the user if he/she is sure.
                DialogResult result = MessageBox.Show($"Are you sure you want to delete this record into Modules?\n\n" +
                    $"Module Code: \t{module.Code}\n\n" +
                    $"Name: \t\t{module.Name}\n\n" +
                    $"Description: \n{module.Description}"
                    , "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                // If yes, insert the record, refresh the datagridview, and select that newly inserted record.
                if (result == DialogResult.Yes)
                {
                    handler.Delete(Tables.Module, handler.addCondition("Module Code", Operator.Equals, module.Code));
                    ShowModule();
                }
            }
        }

        //SEARCH
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

        //-------------------------------------------------------------------------------------------------------------------------------------------------

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
                {
                    txtModuleStudentNumber.Text = dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString();
                    cboModuleStudentStatus.Text = dgvModuleOutput.SelectedRows[0].Cells[3].Value.ToString();
                }
                else if (ModuleResources)
                {
                    rtbModuleResource.Text = dgvModuleOutput.SelectedRows[0].Cells[1].Value.ToString();
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
            if (modules)
            {
                // When double clicking a module, a form is going to display to ask which details the user wants to see.
                ModuleDetailOption moduleFrm = new ModuleDetailOption();
                moduleFrm.Show();
                moduleFrm.GetForm(this);
                this.Enabled = false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------

        //NAVIGATION
        private void btnModuleFirst_Click(object sender, EventArgs e)
        {
            if (dgvModuleOutput.CurrentRow != null)
            {
                dgvModuleOutput.CurrentCell =
                    dgvModuleOutput
                    .Rows[0]
                    .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
            }
        }
        private void btnModulePrevious_Click(object sender, EventArgs e)
        {
            if (dgvModuleOutput.SelectedRows[0].Index > 0)
            {
                dgvModuleOutput.CurrentCell = dgvModuleOutput.Rows[dgvModuleOutput.SelectedRows[0].Index - 1].Cells[0];
            }
            else
            {
                btnModuleLast_Click(sender, e);
            }
        }

        private void btnModuleNext_Click(object sender, EventArgs e)
        {
            if (dgvModuleOutput.SelectedRows[0].Index < dgvModuleOutput.Rows.Count - 1)
            {
                dgvModuleOutput.CurrentCell = dgvModuleOutput.Rows[dgvModuleOutput.SelectedRows[0].Index + 1].Cells[0];
            }
            else
            {
                btnModuleFirst_Click(sender, e);
            }
        }

        private void btnModuleLast_Click(object sender, EventArgs e)
        {
            if (dgvModuleOutput.CurrentRow != null)
            {
                dgvModuleOutput.CurrentCell =
                    dgvModuleOutput
                    .Rows[dgvModuleOutput.Rows.Count - 1]
                    .Cells[dgvModuleOutput.CurrentCell.ColumnIndex];
            }
        }

//=================================================================================================================================================


// Methods for changing what is displayed to the user, depending on what table is shown.
// This is because multiple types of tables are shown on a single form.
//=================================================================================================================================================
        public void ShowStudent()
        {
            currentStudent = 0;
            StudentModules = false;
            ModuleStudents = false;
            ModuleResources = false;
            students = true;
            modules = false;

            tbcDetails.SelectTab(0);
            dgvStudentOutput.DataSource = handler.GetData(Tables.Student);
            if (dgvStudentOutput.Rows.Count > 0)
            {
                dgvStudentOutput.Rows[0].Selected = true;
            }

            lblStudentFor.Visible = false;

            pnlStudent.Show();
            pnlStudentModules.Hide();
            pnlStudentSearch.Show();

            dgvStudentOutput.Columns[dgvStudentOutput.Columns.Count - 1].Visible = false;
        }

        public void ShowStudentModules()
        {
            StudentModules = true;
            ModuleStudents = false;
            ModuleResources = false;
            students = false;
            modules = false;
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
            DataTable dt = handler.GetData(Tables.Module);           

            cboStudentModuleCode.DataSource = dt;
            cboStudentModuleCode.DisplayMember = "Module Code";
            cboStudentModuleCode.ValueMember = "Module Code";

            lblStudentFor.Visible = true;
            lblStudentFor.Text = "";
            lblStudentFor.Text = $"For Student: {currentStudent}";

            pnlStudentModules.Show();
            pnlStudent.Hide();
            pnlStudentSearch.Hide();
        }

        public void ShowModule()
        {
            currentModule = "";
            StudentModules = false;
            ModuleStudents = false;
            ModuleResources = false;
            students = false;
            modules = true;

            dgvModuleOutput.DataSource = handler.GetData(Tables.Module);
            if (dgvModuleOutput.Rows.Count > 0)
            {
                dgvModuleOutput.Rows[0].Selected = true;
            }

            lblModuleFor.Visible = false;

            pnlModuleSearch.Show();
            pnlModule.Show();
            pnlModuleStudents.Hide();
            pnlModuleResources.Hide();
        }

        public void ShowModuleDetails(bool student)
        {
            // When Details are shown it has to determine which details to show, Students or Resources.
            // For this we make use of student.
            StudentModules = false;
            ModuleStudents = false;
            ModuleResources = false;
            students = false;
            modules = false;

            if (student)
            {
                ModuleStudents = true;
                if (currentModule == "")
                {
                    currentModule = dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString();
                }
                dgvModuleOutput.DataSource = handler.GetData(table: Tables.StudentModules, code: currentModule, tableObject: new StudentModule(currentModule));

                pnlModuleSearch.Hide();
                pnlModuleResources.Hide();
                pnlModule.Hide();
                pnlModuleStudents.Show();
            }
            else
            {
                ModuleResources = true;
                if (currentModule == "") {
                    currentModule = dgvModuleOutput.SelectedRows[0].Cells[0].Value.ToString();
                }
                dgvModuleOutput.DataSource = handler.GetData(Tables.Resource, handler.addCondition("Module Code", Operator.Equals, txtModuleCode.Text));

                pnlModuleSearch.Hide();
                pnlModuleResources.Show();
                pnlModule.Hide();
                pnlModuleStudents.Hide();
            }

            lblModuleFor.Visible = true;
            lblStudentFor.Text = "";
            lblModuleFor.Text = $"For Module: {currentModule}";
        }
//=================================================================================================================================================

        // Validates the input where needed.
        public bool ValidateInput()
        {
            bool problems = false;

            // Validate Student input.
            if (students)
            {
                // Checks if there are blank values, else it checks for numbers.
                if ((txtStudentName.Text == "") || (txtStudentSurname.Text == "") || (cboStudentGender.Text == "") || (txtStudentPhone.Text == "") || (rtbStudentAddress.Text == ""))
                {
                    problems = true;
                    MessageBox.Show("Some values were not filled in.\nPlease make sure there are no blank values.",
                        "Blank values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return problems;
                }

                // We check if the name, surname, or gender contains numbers
                List<string> numbers = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    numbers.Add(i.ToString());
                }

                foreach (string item in numbers)
                {
                    if ((txtStudentSurname.Text.Contains(item)) || (txtStudentName.Text.Contains(item)) || (cboStudentGender.Text.Contains(item)))
                    {
                        problems = true;
                        MessageBox.Show("Names, surnames, and genders cannot contain numbers.",
                            "Text contains numbers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return problems;
                    }
                }

                // Checks if the phone number is the correct length
                string newPhone = txtStudentPhone.Text.Replace("-", "").Replace(" ", "").Replace("+", "");
                if (!((newPhone.Length == 11) || (newPhone.Length == 10)))
                {
                    problems = true;
                    MessageBox.Show("The number is incorrect.\nMake sure it is 10 or 11 digits for example.\n+2778-261-6209 or 078 261 6209",
                        "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return problems;
                }
            }

            if (modules)
            {
                // Checks if the Module code is the correct length.
                if (txtModuleCode.Text.Length != 6)
                {
                    problems = true;
                    MessageBox.Show("The Module Code is in the incorrect format.\nMake sure it is 6 digits and in the format:\nPRG1x1",
                        "Phone number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return problems;
                }
            }

            return problems;
        }
    }
}
