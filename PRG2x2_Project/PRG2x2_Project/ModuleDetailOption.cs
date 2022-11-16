using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG2x2_Project
{
    public partial class ModuleDetailOption : Form
    {
        Details mainForm;
        public ModuleDetailOption()
        {
            InitializeComponent();
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            mainForm.ShowModuleDetails(true);
            this.Close();
        }

        private void btnResources_Click(object sender, EventArgs e)
        {
            mainForm.ShowModuleDetails(false);
            this.Close();
        }

        public void GetForm(Details form)
        {
            mainForm = form;
        }

        private void ModuleDetailOption_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
        }
    }
}
