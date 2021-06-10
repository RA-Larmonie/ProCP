using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace WindowsFormsApp3
{
    public partial class FormStatistics : Form
    {
        Airport airport;
        public FormStatistics(Airport airport)
        {
            this.airport = airport;
            
            InitializeComponent();
            SetupGeneralStats();
        }

        private void FormStatistics_Load(object sender, EventArgs e)
        {

        }
        private void SetupGeneralStats() 
        {
            foreach (Flight item in airport.GetFlights())
            {
                CartesianChartLuggage.Series.Add(new ColumnSeries() {Title = "Total number of luggages of " +item.FlightNr, Values = new ChartValues<int> { item.NrOffBaggages }, DataLabels = true });
            }
            
            CartesianChartFlights.Series.Add(new ColumnSeries() { Title = "Total number of flights", Values = new ChartValues<int> { airport.GetNrOfFlights() }, DataLabels = true });
        }
    }
}
