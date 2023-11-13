using OSPABA;
using ScottPlot.Drawing.Colormaps;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace STK_AGENTS.GUI
{
    public partial class GraphB : Form
    {
        SignalPlotXY plot;
        MySimulation sim;

        public GraphB()
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
            double[] plotX = new double[16];
            double[] plotY = new double[16];

            plot = formsPlot1.Plot.AddSignalXY(plotX, plotY);
            formsPlot1.Plot.XAxis.Label("Number of mechanics");
            formsPlot1.Plot.YAxis.Label("Average time in system");
            formsPlot1.Plot.Title("Mechanics/Time");
            formsPlot1.Refresh();

            sim.NumberOfClerks = Convert.ToInt32(textBox2.Text);
            sim.StandardCustomerFlow = checkBox3.Checked;
            sim.SetMaxSimSpeed();
            sim.Turbo = true;

            for (int i = 0; i <= 15; i++)
            {

                sim.NumberOfMechanicsGr1 = i + 10;
                sim.NumberOfMechanicsGr2 = sim.NumberOfMechanicsGr1 / 2;
                
                

                plotX[i] = i + 10;

                Task task = Task.Run(() => sim.Simulate(Convert.ToInt32(textBox1.Text), 480d));
                await task;

                plotY[i] = sim.GSAvgTimeInSystem.Mean();

                plot.MaxRenderIndex = i;
                formsPlot1.Plot.AxisAuto();
                formsPlot1.Refresh();
            }
        }
    }
}
