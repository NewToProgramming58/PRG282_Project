using System;
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
            handler.GetData(Tables.Student);
        }
    }
}
