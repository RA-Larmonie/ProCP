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
    public partial class FormAdmin : Form
    {
        Airport airport;
        public FormAdmin(Airport airport)
        {
            InitializeComponent();
            this.airport = airport;
            refreshList();
        }

        public void refreshList()
        {
            listBox1.Items.Clear();
            foreach (Employee u in airport.getUsers())
            {
                listBox1.Items.Add(u);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee user = listBox1.SelectedItem as Employee;
            airport.deleteUser(user);
            refreshList();
        }

        //Button: Update Access
        private void button2_Click(object sender, EventArgs e)
        {
            Employee user = listBox1.SelectedItem as Employee;
            Employee.RoleType roleType = (Employee.RoleType)Enum.Parse(typeof(Employee.RoleType), comboBoxRole1.SelectedItem.ToString());
            airport.updateAccess(user, roleType);
            refreshList();
        }

        //Button: Add emplpoyee
        private void button3_Click(object sender, EventArgs e)
        {
            Employee.RoleType roleType = (Employee.RoleType)Enum.Parse(typeof(Employee.RoleType), comboBoxRole2.SelectedItem.ToString());
            airport.addUser(new Employee(10, textBoxName.Text, textBoxUserName.Text, textBoxPassw.Text, roleType));
            refreshList();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin(this.airport);
            login.Show();
            this.Hide();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FormInput input = new FormInput();
            input.Show();
            this.Hide();
        }
    }
}
