using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class FormLogin : Form
    {
        Airport airport;

        public FormLogin()
        {
            InitializeComponent();
            airport = new Airport();
        }

        public FormLogin(Airport airport)
        {
            InitializeComponent();
            this.airport = airport;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (userHasAccount())
            {
                if (checkBox1.Checked && userIsAdmin())
                {
                    FormAdmin formAdmin = new FormAdmin(this.airport);
                    formAdmin.Show();
                    this.Hide();
                }
                else if(checkBox1.Checked && !userIsAdmin())
                {
                    MessageBox.Show("You are not an admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FormInput input1 = new FormInput();
                    input1.Show();
                    this.Hide();
                    
                }
            }
            else
            {
                MessageBox.Show("User not Authorized!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool userIsAdmin()
        {
            bool v = false;
            foreach (Employee u in airport.getUsers())
            {
                if (u.Name == textBox1.Text && u.Password == textBox2.Text)
                {
                    if(u.Role == Employee.RoleType.Admin)
                    {
                        v = true;
                    }
                }
            }
            return v;
        }

        private bool userHasAccount()
        {
            bool v = false;
            foreach(Employee u in airport.getUsers())
            {
                if(u.Name == textBox1.Text && u.Password == textBox2.Text)
                {
                    v = true;
                }
            }
            return v;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
