using PRG2x2_Project.User_Classes;
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
    public partial class Login : Form
    {
        List<User> users = new List<User>();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string password = txbPassword.Text;

            if (username.Length != 0 && password.Length != 0) 
            {                
                User user = users.FirstOrDefault(x => x.Username == username);
                if (user != null)
                {
                    
                    if (password.Equals(user.Password))
                    {
                        MessageBox.Show("Login successfull");
                        Details frm = new Details();
                        this.Hide();
                        frm.Show();
                        frm.StoreLoginForm(this);
                    }
                    else
                    {
                        MessageBox.Show("Password is incorrect");
                    }
                }
                else
                {
                    MessageBox.Show("Username does not exist, Please register first");
                }
            }
            else
            {
                MessageBox.Show("Please fill in a usename and password");
            }
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            FileHandler fileHandler = new FileHandler();
            users = fileHandler.GetUsers();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FileHandler fileHandler = new FileHandler();
            User user = new User(txbUsername.Text, txbPassword.Text);
            fileHandler.addUser(user);
            MessageBox.Show($"Registered {user.Username}, Welcome");
        }
    }
}
