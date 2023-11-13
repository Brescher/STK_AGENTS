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
    public partial class Statistics : Form
    {
        public MySimulation sim;
        public Statistics()
        {
            InitializeComponent();
        }

        private DataGridViewTextBoxColumn CreateDataGridColumn(string columnName, string propertyName)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = propertyName;
            column.HeaderText = columnName;

            return column;
        }

        private void FillAllColumnsInControl(DataGridView dataGrid)
        {
            for (var i = 0; i < dataGrid.ColumnCount; i++)
            {
                dataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn stat = CreateDataGridColumn("Statistic", "Statistic");

            DataGridViewTextBoxColumn mean = CreateDataGridColumn("Mean", "Mean");

            DataGridViewTextBoxColumn conf90 = CreateDataGridColumn("90% confidence interval", "90confidenceInterval");

            DataGridViewTextBoxColumn conf95 = CreateDataGridColumn("95% confidence interval", "95confidenceInterval");

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.Columns.Add(stat);
            dataGridView1.Columns.Add(mean);
            dataGridView1.Columns.Add(conf90);
            dataGridView1.Columns.Add(conf95);

            FillAllColumnsInControl(dataGridView1);
            dataGridView1.Rows.Clear();

            dataGridView1.Rows.Add("Customers arrived",
                                    sim.GSCustomersArrived.Mean(),
                                    "<" + Math.Round(sim.GSCustomersArrived.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSCustomersArrived.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSCustomersArrived.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSCustomersArrived.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Customers departed",
                                    sim.GSCustomersDeparted.Mean(),
                                    "<" + Math.Round(sim.GSCustomersDeparted.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSCustomersDeparted.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSCustomersDeparted.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSCustomersDeparted.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Customers in system",
                                    sim.GSCustomersInSystem.Mean(),
                                    "<" + Math.Round(sim.GSCustomersInSystem.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSCustomersInSystem.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSCustomersInSystem.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSCustomersInSystem.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Time in system",
                                    sim.GSAvgTimeInSystem.Mean(),
                                    "<" + Math.Round(sim.GSAvgTimeInSystem.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgTimeInSystem.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgTimeInSystem.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgTimeInSystem.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Customers in system at the end",
                                    sim.GSAvgCustomersInSystemAtTheEnd.Mean(),
                                    "<" + Math.Round(sim.GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Customers in check in queue",
                                    sim.GSAvgCheckInQueueSize.Mean(),
                                    "<" + Math.Round(sim.GSAvgCheckInQueueSize.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgCheckInQueueSize.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgCheckInQueueSize.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgCheckInQueueSize.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Time in check in queue",
                                    sim.GSAvgTimeInCheckInQueue.Mean(),
                                    "<" + Math.Round(sim.GSAvgTimeInCheckInQueue.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgTimeInCheckInQueue.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgTimeInCheckInQueue.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgTimeInCheckInQueue.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Customers in payment queue",
                                    sim.GSAvgPaymentQueueSize.Mean(),
                                    "<" + Math.Round(sim.GSAvgPaymentQueueSize.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgPaymentQueueSize.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgPaymentQueueSize.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgPaymentQueueSize.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Time in payment in queue",
                                    sim.GSAvgTimeInPaymentQueue.Mean(),
                                    "<" + Math.Round(sim.GSAvgTimeInPaymentQueue.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgTimeInPaymentQueue.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgTimeInPaymentQueue.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgTimeInPaymentQueue.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Customers in parking lot",
                                    sim.GSAvgParkingOccupancy.Mean(),
                                    "<" + Math.Round(sim.GSAvgParkingOccupancy.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgParkingOccupancy.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgParkingOccupancy.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgParkingOccupancy.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Time in parking lot",
                                    sim.GSAvgTimeInParkingLot.Mean(),
                                    "<" + Math.Round(sim.GSAvgTimeInParkingLot.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgTimeInParkingLot.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgTimeInParkingLot.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgTimeInParkingLot.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Free clerks",
                                    sim.GSAvgFreeClerks.Mean(),
                                    "<" + Math.Round(sim.GSAvgFreeClerks.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgFreeClerks.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgFreeClerks.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgFreeClerks.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Free mechanics group 1",
                                    sim.GSAvgFreeMechanicsGr1.Mean(),
                                    "<" + Math.Round(sim.GSAvgFreeMechanicsGr1.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgFreeMechanicsGr1.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgFreeMechanicsGr1.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgFreeMechanicsGr1.ConfidenceInterval95[1], 5) + ">"
                                    );

            dataGridView1.Rows.Add("Free mechanics group 2",
                                    sim.GSAvgFreeMechanicsGr2.Mean(),
                                    "<" + Math.Round(sim.GSAvgFreeMechanicsGr2.ConfidenceInterval90[0], 5) + " | " + Math.Round(sim.GSAvgFreeMechanicsGr2.ConfidenceInterval90[1], 5) + ">",
                                    "<" + Math.Round(sim.GSAvgFreeMechanicsGr2.ConfidenceInterval95[0], 5) + " | " + Math.Round(sim.GSAvgFreeMechanicsGr2.ConfidenceInterval95[1], 5) + ">"
                                    );
        }
    }
}
