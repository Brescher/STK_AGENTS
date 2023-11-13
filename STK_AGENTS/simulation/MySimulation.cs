using OSPABA;
using agents;
using OSPStat;
using STK_AGENTS.entities;
using continualAssistants;

namespace simulation
{
	public class MySimulation : Simulation
	{
		Random seedGenerator = new Random();
		int numberOfClerks, numberOfMechanicsGr1, numberOfMechanicsGr2;
		public Stat GSAvgCustomersInSystemAtTheEnd, GSAvgFreeClerks, GSAvgFreeMechanicsGr1, GSAvgFreeMechanicsGr2,
					GSAvgPaymentQueueSize, GSAvgCheckInQueueSize, GSAvgTimeInSystem, GSCustomersInSystem, 
					GSAvgTimeInCheckInQueue, GSAvgTimeInPaymentQueue, GSCustomersArrived, GSAvgCustomersInInspection,
					GSAvgParkingOccupancy, GSCustomersDeparted, GSCustomersChecked, GSTimeInInspection, GSTimeInCheckIn,
                    GSMechanicsGr1HadLunch, GSMechanicsGr2HadLunch, GSClerksHadLuch, GSAvgTimeInParkingLot;
        public bool Validation, Turbo, StandardCustomerFlow;

		public MySimulation()
		{
			Init();
		}

		override protected void PrepareSimulation()
		{
			base.PrepareSimulation();
            // Create global statistcis
            GSAvgCustomersInSystemAtTheEnd = new Stat();
            GSAvgFreeClerks = new Stat();
            GSAvgFreeMechanicsGr1 = new Stat();
            GSAvgFreeMechanicsGr2 = new Stat();
            GSAvgPaymentQueueSize = new Stat();
            GSAvgCheckInQueueSize = new Stat();
            GSAvgTimeInSystem = new Stat();
            GSCustomersInSystem = new Stat();
            GSAvgTimeInCheckInQueue = new Stat();
            GSAvgTimeInPaymentQueue = new Stat();
            GSCustomersArrived = new Stat();
            GSAvgCustomersInInspection = new Stat();
            GSAvgParkingOccupancy = new Stat();
            GSCustomersDeparted = new Stat();
            GSCustomersChecked = new Stat();
            GSTimeInInspection = new Stat();
            GSTimeInCheckIn = new Stat();
            GSMechanicsGr1HadLunch = new Stat();
            GSMechanicsGr2HadLunch = new Stat();
            GSClerksHadLuch = new Stat();
            GSAvgTimeInParkingLot = new Stat();

        }

		override protected void PrepareReplication()
		{
			base.PrepareReplication();
            // Reset entities, queues, local statistics, etc...
            //SetSimSpeed(20, 1);
        }

		override protected void ReplicationFinished()
		{
            // Actualize free mechanics after rep
            

            GSAvgCustomersInSystemAtTheEnd.AddSample(AgentSurroundings.CustomersInSTK.Count);


            AgentSurroundings.CustomersInSTK.LengthStatistic.updateAfterReplication();
            AgentReception.FreeClerks.LengthStatistic.updateAfterReplication();
            AgentWorkshop.FreeMechanicsGr1.LengthStatistic.updateAfterReplication();
            AgentWorkshop.FreeMechanicsGr2.LengthStatistic.updateAfterReplication();
            AgentReception.CustomersInCheckInQueue.LengthStatistic.updateAfterReplication();
            AgentReception.CustomersInPaymentQueue.LengthStatistic.updateAfterReplication();



            GSAvgFreeClerks.AddSample(AgentReception.FreeClerks.LengthStatistic.Mean());
            GSAvgFreeMechanicsGr1.AddSample(AgentWorkshop.FreeMechanicsGr1.LengthStatistic.Mean());
            GSAvgFreeMechanicsGr2.AddSample(AgentWorkshop.FreeMechanicsGr2.LengthStatistic.Mean());
            GSAvgCheckInQueueSize.AddSample(AgentReception.CustomersInCheckInQueue.LengthStatistic.Mean());
            GSAvgPaymentQueueSize.AddSample(AgentReception.CustomersInPaymentQueue.LengthStatistic.Mean());
            GSCustomersInSystem.AddSample(AgentSurroundings.CustomersInSTK.LengthStatistic.Mean());
            GSAvgTimeInSystem.AddSample(AgentSurroundings.TimeInSystem.Mean());
            GSAvgTimeInCheckInQueue.AddSample(AgentReception.TimeInCheckInQueue.Mean());
            GSAvgTimeInPaymentQueue.AddSample(AgentReception.TimeInPaymentQueue.Mean());
            GSCustomersArrived.AddSample(AgentSurroundings.CustomersArrived);
            GSCustomersDeparted.AddSample(AgentSurroundings.CustomersDeparted);
            GSAvgCustomersInInspection.AddSample(AgentWorkshop.CustomersInInespection.LengthStatistic.Mean());
            GSAvgParkingOccupancy.AddSample(AgentWorkshop.CustomersInParkingLot.LengthStatistic.Mean());
            GSCustomersChecked.AddSample(AgentWorkshop.customersChecked);
            GSTimeInInspection.AddSample(AgentWorkshop.TimeInInspection.Mean());
            GSTimeInCheckIn.AddSample(AgentReception.TimeInCheckIn.Mean());
            GSMechanicsGr1HadLunch.AddSample(AgentWorkshop.MechanicsGr1LunchDone);
            GSMechanicsGr2HadLunch.AddSample(AgentWorkshop.MechanicsGr2LunchDone);
            GSClerksHadLuch.AddSample(AgentReception.ClerksLunchDone);
            GSAvgTimeInParkingLot.AddSample(AgentWorkshop.TimeInParkingLot.Mean());


            foreach (ISimDelegate item in Delegates)
            {
                item.Refresh(this);
            }

            Turbo = true;
            // Collect local statistics into global, update UI, etc...
            base.ReplicationFinished();
			
        }

		override protected void SimulationFinished()
		{
			// Dysplay simulation results
			base.SimulationFinished();

            foreach (ISimDelegate item in Delegates)
            {
                item.Refresh(this);
            }
            string nameOfFile;
            if (Validation)
            {
                nameOfFile = "Validation-R"+ ReplicationCount + "-C-" + numberOfClerks + "-M1-" + numberOfMechanicsGr1 + "-" + DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss") + ".csv";
            }
            else
            {
                nameOfFile = ReplicationCount + "C-" + numberOfClerks + "-M1-" + numberOfMechanicsGr1 + "-M2-" + numberOfMechanicsGr2 + "-" + DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss") + ".csv";
            }

            if (!StandardCustomerFlow)
            {
                nameOfFile = "not_standard_customer_flow" + nameOfFile;
            }
            string projectDirectory = Directory.GetCurrentDirectory();
            string solutionDirectory = Directory.GetParent(projectDirectory).Parent.Parent.Parent.FullName;
            string folderPath = solutionDirectory + "\\CSVFiles";
            string filePath = folderPath + "\\" + nameOfFile;
            //SaveToCSV(filePath);
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			AgentModel = new AgentModel(SimId.AgentModel, this, null);
			AgentSurroundings = new AgentSurroundings(SimId.AgentSurroundings, this, AgentModel);
			AgentSTK = new AgentSTK(SimId.AgentSTK, this, AgentModel);
			AgentReception = new AgentReception(SimId.AgentReception, this, AgentSTK);
			AgentWorkshop = new AgentWorkshop(SimId.AgentWorkshop, this, AgentSTK);
		}
		public AgentModel AgentModel
		{ get; set; }
		public AgentSurroundings AgentSurroundings
		{ get; set; }
		public AgentSTK AgentSTK
		{ get; set; }
		public AgentReception AgentReception
		{ get; set; }
		public AgentWorkshop AgentWorkshop
		{ get; set; }
        public Random SeedGenerator { get => seedGenerator; set => seedGenerator = value; }
        public int NumberOfClerks { get => numberOfClerks; set => numberOfClerks = value; }
        public int NumberOfMechanicsGr1 { get => numberOfMechanicsGr1; set => numberOfMechanicsGr1 = value; }
        public int NumberOfMechanicsGr2 { get => numberOfMechanicsGr2; set => numberOfMechanicsGr2 = value; }
        //meta! tag="end"


        public static string FormatTime(double time)
        {
            int h = (int)Math.Floor(time / 60);
            int m = (int)Math.Floor (time % 60);
            double s = ((int)(time*60) % 60) + (time * 60) - (int)(time * 60);

            string ret = "";
            if (0 < h) ret += (h < 10 ? "0" : "") + h + ":";
            ret += (m < 10 ? "0" : "") + m + ":";
            ret += (s < 10 ? "0" : "") + string.Format("{0:0.00}", s);
            return ret;
        }

        public void SaveToCSV(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write("Name;Mean;Lower 90% CI; Upper 90% CI; Lower 95% CI; Upper 95% CI;");
                writer.WriteLine();
                writer.Write("Customers arrived;");
                writer.Write(GSCustomersArrived.Mean() + ";" + Math.Round(GSCustomersArrived.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSCustomersArrived.ConfidenceInterval90[1], 5) + ";"
                                                             + Math.Round(GSCustomersArrived.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSCustomersArrived.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Customers departed;");
                writer.Write(GSCustomersDeparted.Mean() + ";" + Math.Round(GSCustomersDeparted.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSCustomersDeparted.ConfidenceInterval90[1], 5) + ";"
                                                              + Math.Round(GSCustomersDeparted.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSCustomersDeparted.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Customers in system;");
                writer.Write(GSCustomersInSystem.Mean() + ";" + Math.Round(GSCustomersInSystem.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSCustomersInSystem.ConfidenceInterval90[1], 5) + ";"
                                                              + Math.Round(GSCustomersInSystem.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSCustomersInSystem.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Time in system;");
                writer.Write(GSAvgTimeInSystem.Mean() + ";" + Math.Round(GSAvgTimeInSystem.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgTimeInSystem.ConfidenceInterval90[1], 5) + ";"
                                                            + Math.Round(GSAvgTimeInSystem.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgTimeInSystem.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Customers in system at the end;");
                writer.Write(GSAvgCustomersInSystemAtTheEnd.Mean() + ";" + Math.Round(GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval90[1], 5) + ";"
                                                                         + Math.Round(GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgCustomersInSystemAtTheEnd.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Customers in check in queue;");
                writer.Write(GSAvgCheckInQueueSize.Mean() + ";" + Math.Round(GSAvgCheckInQueueSize.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgCheckInQueueSize.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgCheckInQueueSize.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgCheckInQueueSize.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Time in check in queue;");
                writer.Write(GSAvgTimeInCheckInQueue.Mean() + ";" + Math.Round(GSAvgTimeInCheckInQueue.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgTimeInCheckInQueue.ConfidenceInterval90[1], 5) + ";"
                                                                  + Math.Round(GSAvgTimeInCheckInQueue.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgTimeInCheckInQueue.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Customers in payment queue;");
                writer.Write(GSAvgPaymentQueueSize.Mean() + ";" + Math.Round(GSAvgPaymentQueueSize.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgPaymentQueueSize.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgPaymentQueueSize.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgPaymentQueueSize.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Time in payment in queue;");
                writer.Write(GSAvgTimeInPaymentQueue.Mean() + ";" + Math.Round(GSAvgTimeInPaymentQueue.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgTimeInPaymentQueue.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgTimeInPaymentQueue.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgTimeInPaymentQueue.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Customers in parking lot;");
                writer.Write(GSAvgParkingOccupancy.Mean() + ";" + Math.Round(GSAvgParkingOccupancy.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgParkingOccupancy.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgParkingOccupancy.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgParkingOccupancy.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Time in parking lot;");
                writer.Write(GSAvgTimeInParkingLot.Mean() + ";" + Math.Round(GSAvgTimeInParkingLot.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgTimeInParkingLot.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgTimeInParkingLot.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgTimeInParkingLot.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Free clerks;");
                writer.Write(GSAvgFreeClerks.Mean() + ";" + Math.Round(GSAvgFreeClerks.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgFreeClerks.ConfidenceInterval90[1], 5) + ";"
                                                          + Math.Round(GSAvgFreeClerks.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgFreeClerks.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Free mechanics group 1;");
                writer.Write(GSAvgFreeMechanicsGr1.Mean() + ";" + Math.Round(GSAvgFreeMechanicsGr1.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgFreeMechanicsGr1.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgFreeMechanicsGr1.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgFreeMechanicsGr1.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Free mechanics group 2;");
                writer.Write(GSAvgFreeMechanicsGr2.Mean() + ";" + Math.Round(GSAvgFreeMechanicsGr2.ConfidenceInterval90[0], 5) + ";" + Math.Round(GSAvgFreeMechanicsGr2.ConfidenceInterval90[1], 5) + ";"
                                                                + Math.Round(GSAvgFreeMechanicsGr2.ConfidenceInterval95[0], 5) + ";" + Math.Round(GSAvgFreeMechanicsGr2.ConfidenceInterval95[1], 5) + ";");
                writer.WriteLine();
                writer.Write("Labour costs;");
                writer.Write((numberOfClerks*1100) + (numberOfMechanicsGr1*1500) + (numberOfMechanicsGr2*2000));
                writer.WriteLine();

                writer.Write("Number of replications;");
                writer.Write(ReplicationCount);
                writer.WriteLine();
            }
        }
    }
}