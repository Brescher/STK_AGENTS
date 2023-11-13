using OSPABA;
using ScottPlot.Plottable;
using simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STK_AGENTS.GUI
{
    public partial class GraphA : Form
    {
        SignalPlotXY plot;
        MySimulation sim;
        public GraphA()
        {
            InitializeComponent();
            sim = new MySimulation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            double[] plotX = new double[15];
            double[] plotY = new double[15];

            plot = formsPlot1.Plot.AddSignalXY(plotX, plotY);
            formsPlot1.Plot.XAxis.Label("Number of clerks");
            formsPlot1.Plot.YAxis.Label("Average number of customers in check in queue");
            formsPlot1.Plot.Title("Clerks/Customers");
            formsPlot1.Refresh();
            sim.StandardCustomerFlow = checkBox3.Checked;
            sim.NumberOfMechanicsGr1 = Convert.ToInt32(textBox2.Text);
            sim.NumberOfMechanicsGr2 = Convert.ToInt32(textBox2.Text);



            sim.SetMaxSimSpeed();
            sim.Turbo = true;

            for (int i = 1; i <= 15; i++)
            {
                sim.NumberOfClerks = i;

                plotX[i - 1] = i;

                Task task = Task.Run(() => sim.Simulate(Convert.ToInt32(textBox1.Text), 480d));
                await task;

                plotY[i - 1] = sim.GSAvgCheckInQueueSize.Mean();

                plot.MaxRenderIndex = i - 1;
                formsPlot1.Plot.AxisAuto();
                formsPlot1.Refresh();
            }
        }
    }
}
