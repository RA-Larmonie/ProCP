using System;
using System.CodeDom.Compiler;
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
    public partial class FormInput : Form
    {
        Airport airport;

        public FormInput()
        {
            InitializeComponent();
            this.airport = new Airport();
        }

        //Button: Next
        private void button1_Click(object sender, EventArgs e)
        {
            if(this.airport.flights.Count > 0)
            {
                FormSim sim1 = new FormSim(airport);
                FormStatistics stat = new FormStatistics(airport);
                sim1.Show();
                stat.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please enter a flight!");
            }
            
            
        }

        //Add flight
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Please choose a color!");
                }
                else if(textBoxFlightNr.Text == "")
                {
                    MessageBox.Show("Please enter a flight nr");
                }
                else if(textBoxNrBagg.Text == "")
                {
                    MessageBox.Show("Please enter the number of baggages");
                }
                else
                {
                    if (airport.addFlight(new Flight(flight: textBoxFlightNr.Text,
                                             nrOffBaggages: Convert.ToInt32(textBoxNrBagg.Text),
                                             color: comboBox1.SelectedItem.ToString())))
                    {
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                    }
                    else
                    {
                        MessageBox.Show("Flight already exists!");
                    }
                }
                
                
            }
            catch (Exception )
            {
                MessageBox.Show("Please enter a number in baggages!");
            }
            
            updateListbox1();
        }

        private void updateListbox1()
        {
            listBox1.Items.Clear();
            foreach(Flight flight in airport.flights)
            {
                listBox1.Items.Add(flight);
            }            
        }


    }
}
