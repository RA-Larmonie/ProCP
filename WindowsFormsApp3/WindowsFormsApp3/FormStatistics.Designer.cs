
namespace WindowsFormsApp3
{
    partial class FormStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChartLuggae = new System.Windows.Forms.Integration.ElementHost();
            this.CartesianChartLuggage = new LiveCharts.Wpf.CartesianChart();
            this.label1 = new System.Windows.Forms.Label();
            this.ChartFlights = new System.Windows.Forms.Integration.ElementHost();
            this.CartesianChartFlights = new LiveCharts.Wpf.CartesianChart();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChartLuggae
            // 
            this.ChartLuggae.Location = new System.Drawing.Point(12, 34);
            this.ChartLuggae.Name = "ChartLuggae";
            this.ChartLuggae.Size = new System.Drawing.Size(304, 163);
            this.ChartLuggae.TabIndex = 0;
            this.ChartLuggae.Text = "CartesianChartLuggage";
            this.ChartLuggae.Child = this.CartesianChartLuggage;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total number of luggages per flight";
            // 
            // ChartFlights
            // 
            this.ChartFlights.Location = new System.Drawing.Point(368, 34);
            this.ChartFlights.Name = "ChartFlights";
            this.ChartFlights.Size = new System.Drawing.Size(309, 163);
            this.ChartFlights.TabIndex = 2;
            this.ChartFlights.Text = "elementHost1";
            this.ChartFlights.Child = this.CartesianChartFlights;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(458, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total number of flights";
            // 
            // FormStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 247);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ChartFlights);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChartLuggae);
            this.Name = "FormStatistics";
            this.Text = "FormStatistics";
            this.Load += new System.EventHandler(this.FormStatistics_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ChartLuggae;
        private LiveCharts.Wpf.CartesianChart CartesianChartLuggage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Integration.ElementHost ChartFlights;
        private LiveCharts.Wpf.CartesianChart CartesianChartFlights;
        private System.Windows.Forms.Label label2;
    }
}