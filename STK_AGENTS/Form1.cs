using OSPABA;
using simulation;
using STK_AGENTS.entities;
using STK_AGENTS.GUI;
using STK_AGENTS.view_classes;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STK_AGENTS
{
    public partial class Form1 : Form, ISimDelegate
    {
        MySimulation sim;
        Task task;
        bool pause = false;
        bool isRunning = false;
        bool turbo = false;
        public Form1()
        {
            InitializeComponent();
            InitializeDataGrid();
            //InitializeSim();

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void InitializeSim()
        {
            sim = new MySimulation();
            sim.RegisterDelegate(this);
        }

        public void SimStateChanged(Simulation sim, SimState state)
        {
            //throw new NotImplementedException();
        }

        private void StartSim(object sender, EventArgs e)
        {
            if (isRunning)
            {
                sim.StopSimulation();
                isRunning = false;
            }

            InitializeSim();
            sim.NumberOfClerks = Convert.ToInt32(textBox2.Text);
            sim.NumberOfMechanicsGr1 = Convert.ToInt32(textBox3.Text);
            sim.NumberOfMechanicsGr2 = Convert.ToInt32(textBox4.Text);
            sim.StandardCustomerFlow = checkBox3.Checked;
            sim.Validation = checkBox1.Checked;
            label24.Text = ((sim.NumberOfClerks * 1100) + (sim.NumberOfMechanicsGr1 * 1500) + (sim.NumberOfMechanicsGr2 * 2000)).ToString() + " €";
            if (checkBoxTurbo.Checked)
            {
                sim.SetMaxSimSpeed();
                sim.Turbo = true;
                turbo = true;
            }
            else
            {
                sim.SetSimSpeed(trackBarInterval.Value, trackBarDuration.Value / 10);
                sim.Turbo = false;
                turbo = false;
            }
            
            ClearAndRefreshGrids();

            isRunning = true;
            sim.SimulateAsync(Convert.ToInt32(textBox1.Text), 480d);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void Pause(object sender, EventArgs e)
        {
            if (isRunning)
            {
                if (!pause)
                {
                    pause = true;
                    sim.PauseSimulation();
                    PauseButton.Text = "Continue";
                }
                else
                {
                    pause = false;
                    sim.ResumeSimulation();
                    PauseButton.Text = "Pause";
                }
            }
        }

        private void Stop(object sender, EventArgs e)
        {
            if (isRunning)
            {
                sim.StopSimulation();
                isRunning = false;
            }
        }

        private void DoOnGuiThread(Control control, System.Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void ClearAndRefreshGrids()
        {
            dataGridCheckIn.Refresh();
            dataGridClerks.Refresh();
            dataGridCustomers.Refresh();
            dataGridMechanicsGr1.Refresh();
            dataGridMechanicsGr2.Refresh();
            dataGridParking.Refresh();
            dataGridPayment.Refresh();
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

        private void InitializeDataGrid()
        {
            InitializeDataGridCustomers();
            InitializeDataGridClerks();
            InitializeDataGridMechanicsGr1();
            InitializeDataGridMechanicsGr2();
            InitializeDataGridCheckIn();
            InitializeDataGridParking();
            InitializeDataGridPayment();
        }

        private void InitializeDataGridCustomers()
        {
            dataGridCustomers.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Id", "Id");

            DataGridViewTextBoxColumn carColumn = CreateDataGridColumn("Car type", "CarType");

            DataGridViewTextBoxColumn positionColumn = CreateDataGridColumn("Position", "Position");

            DataGridViewTextBoxColumn timeInCheckInQColumn = CreateDataGridColumn("Time in check in queue", "TInCheckInQ");

            DataGridViewTextBoxColumn timeInPaymentQColumn = CreateDataGridColumn("Time in payment queue", "TInPayQ");

            dataGridCustomers.Columns.Add(IdColumn);
            dataGridCustomers.Columns.Add(carColumn);
            dataGridCustomers.Columns.Add(positionColumn);
            dataGridCustomers.Columns.Add(timeInCheckInQColumn);
            dataGridCustomers.Columns.Add(timeInPaymentQColumn);

            FillAllColumnsInControl(dataGridCustomers);
            dataGridCustomers.Rows.Clear();
        }

        private void InitializeDataGridClerks()
        {
            dataGridClerks.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Id", "Id");

            DataGridViewTextBoxColumn jobColumn = CreateDataGridColumn("Job", "Job");

            DataGridViewTextBoxColumn customerColumn = CreateDataGridColumn("Customer ID", "CustomerID");

            dataGridClerks.Columns.Add(IdColumn);
            dataGridClerks.Columns.Add(jobColumn);
            dataGridClerks.Columns.Add(customerColumn);

            FillAllColumnsInControl(dataGridClerks);
            dataGridClerks.Rows.Clear();
        }

        private void InitializeDataGridMechanicsGr1()
        {
            dataGridMechanicsGr1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Id", "Id");

            DataGridViewTextBoxColumn jobColumn = CreateDataGridColumn("Job", "Job");

            DataGridViewTextBoxColumn customerColumn = CreateDataGridColumn("Customer ID", "CustomerID");

            dataGridMechanicsGr1.Columns.Add(IdColumn);
            dataGridMechanicsGr1.Columns.Add(jobColumn);
            dataGridMechanicsGr1.Columns.Add(customerColumn);

            FillAllColumnsInControl(dataGridMechanicsGr1);
            dataGridMechanicsGr1.Rows.Clear();
        }

        private void InitializeDataGridMechanicsGr2()
        {
            dataGridMechanicsGr2.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Id", "Id");

            DataGridViewTextBoxColumn jobColumn = CreateDataGridColumn("Job", "Job");

            DataGridViewTextBoxColumn customerColumn = CreateDataGridColumn("Customer ID", "CustomerID");

            dataGridMechanicsGr2.Columns.Add(IdColumn);
            dataGridMechanicsGr2.Columns.Add(jobColumn);
            dataGridMechanicsGr2.Columns.Add(customerColumn);

            FillAllColumnsInControl(dataGridMechanicsGr2);
            dataGridMechanicsGr2.Rows.Clear();
        }

        private void InitializeDataGridCheckIn()
        {
            dataGridCheckIn.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Customer Id", "Id");

            DataGridViewTextBoxColumn startWait = CreateDataGridColumn("Waiting time start", "startWait");

            dataGridCheckIn.Columns.Add(IdColumn);
            dataGridCheckIn.Columns.Add(startWait);

            FillAllColumnsInControl(dataGridCheckIn);
            dataGridCheckIn.Rows.Clear();
        }

        private void InitializeDataGridParking()
        {
            dataGridParking.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Customer Id", "CustomerId");

            DataGridViewTextBoxColumn startWait = CreateDataGridColumn("Waiting time start", "startWait");

            dataGridParking.Columns.Add(IdColumn);

            dataGridParking.Columns.Add(startWait);

            FillAllColumnsInControl(dataGridParking);
            dataGridParking.Rows.Clear();
        }

        private void InitializeDataGridPayment()
        {
            dataGridPayment.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn IdColumn = CreateDataGridColumn("Customer Id", "Id");

            DataGridViewTextBoxColumn startWait = CreateDataGridColumn("Waiting time start", "startWait");

            dataGridPayment.Columns.Add(IdColumn);
            dataGridPayment.Columns.Add(startWait);

            FillAllColumnsInControl(dataGridPayment);
            dataGridPayment.Rows.Clear();
        }

        private void sleepTime_Scroll(object sender, EventArgs e)
        {
            if (isRunning && !sim.Turbo)
            {
                sim.SetSimSpeed(trackBarInterval.Value, trackBarDuration.Value / 10);
            }
        }

        private void interval_Scroll(object sender, EventArgs e)
        {
            if (isRunning && !sim.Turbo)
            {
                sim.SetSimSpeed(trackBarInterval.Value, trackBarDuration.Value / 10);
            }
        }

        public void Refresh(Simulation sim)
        {
            MySimulation mySim = (MySimulation)sim;
            this.Invoke(new System.Action(() =>
            {
                DoOnGuiThread(label18, new System.Action(() =>
                {
                    label18.Text = MySimulation.FormatTime(mySim.CurrentTime + (9 * 60));
                }));
                DoOnGuiThread(label19, new System.Action(() =>
                {
                    label19.Text = mySim.CurrentReplication.ToString();
                }));

                if (!mySim.Turbo)
                {
                    DoOnGuiThread(textBox5, new System.Action(() =>
                    {
                        textBox5.Text = "Customers arrived: " + mySim.AgentSurroundings.CustomersArrived.ToString() +
                                    "\r\nCustomers departed: " + mySim.AgentSurroundings.CustomersDeparted.ToString() +
                                    "\r\nNumber of customers in system: " + mySim.AgentSurroundings.CustomersInSTK.Count.ToString() +
                                    "\r\nCheck in queue size: " + mySim.AgentReception.CustomersInCheckInQueue.Count.ToString() +
                                    "\r\nParking in front of the workshop: " + mySim.AgentWorkshop.CustomersInParkingLot.Count.ToString() + " / 5" +
                                    "\r\nPayment queue size: " + mySim.AgentReception.CustomersInPaymentQueue.Count.ToString() +
                                    "\r\nClerks free / total: " + mySim.AgentReception.FreeClerks.Count.ToString() + " / " + mySim.NumberOfClerks.ToString() +
                                    "\r\nMechanics group 1 free / total: " + mySim.AgentWorkshop.FreeMechanicsGr1.Count.ToString() + "/" + mySim.NumberOfMechanicsGr1.ToString() +
                                    "\r\nMechanics group 2 free / total: " + mySim.AgentWorkshop.FreeMechanicsGr2.Count.ToString() + "/" + mySim.NumberOfMechanicsGr2.ToString();



                    }));

                    DoOnGuiThread(dataGridCustomers, new System.Action(() =>
                    {
                        dataGridCustomers.Rows.Clear();
                        foreach (Customer customer in mySim.AgentSurroundings.CustomersInSTK)
                        {
                            dataGridCustomers.Rows.Add(customer.Id, customer.TypeOfCar, customer.Position, MySimulation.FormatTime(customer.QueueEntranceWaitTime), MySimulation.FormatTime(customer.QueueDepartureWaitTime));
                        }
                    }));

                    DoOnGuiThread(dataGridClerks, new System.Action(() =>
                    {
                        dataGridClerks.Rows.Clear();
                        foreach (Clerk clerk in mySim.AgentReception.Clerks)
                        {
                            dataGridClerks.Rows.Add(clerk.Id, clerk.Job, clerk.CustomerId);
                        }
                    }));

                    DoOnGuiThread(dataGridMechanicsGr1, new System.Action(() =>
                    {
                        dataGridMechanicsGr1.Rows.Clear();
                        foreach (MechanicGr1 mech in mySim.AgentWorkshop.MechanicsGr1)
                        {
                            dataGridMechanicsGr1.Rows.Add(mech.Id, mech.Job, mech.CustomerId);
                        }
                    }));

                    DoOnGuiThread(dataGridMechanicsGr2, new System.Action(() =>
                    {
                        dataGridMechanicsGr2.Rows.Clear();
                        foreach (MechanicGr2 mech in mySim.AgentWorkshop.MechanicsGr2)
                        {
                            dataGridMechanicsGr2.Rows.Add(mech.Id, mech.Job, mech.CustomerId);
                        }
                    }));

                    DoOnGuiThread(dataGridCheckIn, new System.Action(() =>
                    {
                        dataGridCheckIn.Rows.Clear();
                        foreach (MyMessage msg in mySim.AgentReception.CustomersInCheckInQueue)
                        {
                            dataGridCheckIn.Rows.Add(msg.Customer.Id, MySimulation.FormatTime(msg.Customer.QueueEntranceStartWaitTime));
                        }
                    }));

                    DoOnGuiThread(dataGridParking, new System.Action(() =>
                    {
                        dataGridParking.Rows.Clear();
                        foreach (MyMessage msg in mySim.AgentWorkshop.CustomersInParkingLot)
                        {
                            dataGridParking.Rows.Add(msg.Customer.Id, MySimulation.FormatTime(msg.Customer.ParkingStartWaitTime));
                        }
                    }));

                    DoOnGuiThread(dataGridPayment, new System.Action(() =>
                    {
                        dataGridPayment.Rows.Clear();
                        foreach (MyMessage msg in mySim.AgentReception.CustomersInPaymentQueue)
                        {
                            dataGridPayment.Rows.Add(msg.Customer.Id, MySimulation.FormatTime(msg.Customer.QueueDepartureStartWaitTime));
                        }
                    }));
                }
                if (mySim.ReplicationCount >= 100)
                {
                    if ((mySim.CurrentReplication % (mySim.ReplicationCount / 100)) == 0)
                    {
                        textBox6.Text = "Customers arrived: " + Math.Round(mySim.GSCustomersArrived.Mean(), 5).ToString() +
                                        "\r\nCustomers departed: " + Math.Round(mySim.GSCustomersDeparted.Mean(), 5).ToString() +
                                        "\r\nNumber of customers in system: " + Math.Round(mySim.GSCustomersInSystem.Mean(), 5).ToString() +
                                        "\r\nNumber of customers in system at the end: " + Math.Round(mySim.GSAvgCustomersInSystemAtTheEnd.Mean(), 5).ToString() +
                                        "\r\nTime of customer in system: " + Math.Round(mySim.GSAvgTimeInSystem.Mean(), 5).ToString() +
                                        "\r\nCheck in queue size: " + Math.Round(mySim.GSAvgCheckInQueueSize.Mean(), 5).ToString() +
                                        "\r\nTime in check in queue: " + Math.Round(mySim.GSAvgTimeInCheckInQueue.Mean(), 5).ToString() +
                                        "\r\nParking in front of the workshop: " + Math.Round(mySim.GSAvgParkingOccupancy.Mean(), 5).ToString() +
                                        "\r\nTime in parking in front of the workshop: " + Math.Round(mySim.GSAvgTimeInParkingLot.Mean(), 5).ToString() +
                                        "\r\nPayment queue size: " + Math.Round(mySim.GSAvgPaymentQueueSize.Mean(), 5).ToString() +
                                        "\r\nTime in payment queue: " + Math.Round(mySim.GSAvgTimeInPaymentQueue.Mean(), 5).ToString() +
                                        "\r\nClerks free: " + Math.Round(mySim.GSAvgFreeClerks.Mean(), 5).ToString() +
                                        "\r\nMechanics group 1 free: " + Math.Round(mySim.GSAvgFreeMechanicsGr1.Mean(), 5).ToString() +
                                        "\r\nMechanics group 2 free: " + Math.Round(mySim.GSAvgFreeMechanicsGr2.Mean(), 5).ToString() +
                                        "\r\nClerks had lunch: " + Math.Round(mySim.GSClerksHadLuch.Mean(), 5).ToString() +
                                        "\r\nMechanics group 1 had lunch: " + Math.Round(mySim.GSMechanicsGr1HadLunch.Mean(), 5).ToString() +
                                        "\r\nMechanics group 2 had lunch: " + Math.Round(mySim.GSMechanicsGr2HadLunch.Mean(), 5).ToString();
                    }

                }
                else
                {
                    textBox6.Text = "Customers arrived: " + Math.Round(mySim.GSCustomersArrived.Mean(), 5).ToString() +
                                    "\r\nCustomers departed: " + Math.Round(mySim.GSCustomersDeparted.Mean(), 5).ToString() +
                                    "\r\nNumber of customers in system: " + Math.Round(mySim.GSCustomersInSystem.Mean(), 5).ToString() +
                                    "\r\nNumber of customers in system at the end: " + Math.Round(mySim.GSAvgCustomersInSystemAtTheEnd.Mean(), 5).ToString() +
                                    "\r\nTime of customer in system: " + Math.Round(mySim.GSAvgTimeInSystem.Mean(), 5).ToString() +
                                    "\r\nCheck in queue size: " + Math.Round(mySim.GSAvgCheckInQueueSize.Mean(), 5).ToString() +
                                    "\r\nTime in check in queue: " + Math.Round(mySim.GSAvgTimeInCheckInQueue.Mean(), 5).ToString() +
                                    "\r\nParking in front of the workshop: " + Math.Round(mySim.GSAvgParkingOccupancy.Mean(), 5).ToString() +
                                    "\r\nTime in parking in front of the workshop: " + Math.Round(mySim.GSAvgTimeInParkingLot.Mean(), 5).ToString() +
                                    "\r\nPayment queue size: " + Math.Round(mySim.GSAvgPaymentQueueSize.Mean(), 5).ToString() +
                                    "\r\nTime in payment queue: " + Math.Round(mySim.GSAvgTimeInPaymentQueue.Mean(), 5).ToString() +
                                    "\r\nClerks free: " + Math.Round(mySim.GSAvgFreeClerks.Mean(), 5).ToString() +
                                    "\r\nMechanics group 1 free: " + Math.Round(mySim.GSAvgFreeMechanicsGr1.Mean(), 5).ToString() +
                                    "\r\nMechanics group 2 free: " + Math.Round(mySim.GSAvgFreeMechanicsGr2.Mean(), 5).ToString() +
                                    "\r\nClerks had lunch: " + Math.Round(mySim.GSClerksHadLuch.Mean(), 5).ToString() +
                                    "\r\nMechanics group 1 had lunch: " + Math.Round(mySim.GSMechanicsGr1HadLunch.Mean(), 5).ToString() +
                                    "\r\nMechanics group 2 had lunch: " + Math.Round(mySim.GSMechanicsGr2HadLunch.Mean(), 5).ToString();
                }
            }));

        }

        private void GraphA(object sender, EventArgs e)
        {
            this.Hide();
            GraphA form2 = new GraphA();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void GraphB(object sender, EventArgs e)
        {
            this.Hide();
            GraphB form2 = new GraphB();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void showStat(object sender, EventArgs e)
        {
            if(sim != null)
            {
                Statistics myForm = new Statistics();
                myForm.sim = sim;
                myForm.Show();
            }
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(isRunning)
            {
                sim.StopSimulation();
            }
            base.OnClosing(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (isRunning)
            {
                sim.StopSimulation();
            }
            base.OnClosing(e);
        }

        private void Form1_Closed(object sender, System.EventArgs e)
        {
            if (isRunning)
            {
                sim.StopSimulation();
            }
        }
    }
}