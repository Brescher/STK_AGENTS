using OSPABA;
using simulation;
using managers;
using continualAssistants;
using STK_AGENTS.entities;
using OSPStat;
using OSPDataStruct;

namespace agents
{
    //meta! id="2"
    public class AgentSurroundings : Agent
	{
        public Stat TimeInSystem { get; set; }
		public int CustomersArrived { get; set; }
		public int CustomersDeparted { get; set; }

        public SimQueue<Customer> CustomersInSTK;

        
        public AgentSurroundings(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            TimeInSystem = new Stat();
			CustomersArrived = 0;
			CustomersDeparted = 0;
            CustomersInSTK = new SimQueue<Customer>(new WStat(MySim));

            StartSimulation();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerSurroundings(SimId.ManagerSurroundings, MySim, this);
			new SchedulerCustomerArrival(SimId.SchedulerCustomerArrival, MySim, this);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.CustomerDeparture);
			AddOwnMessage(Mc.NewCustomer);
			AddOwnMessage(Mc.Finish);
		}
        //meta! tag="end"

        public void StartSimulation()
        {
			MyMessage message = new MyMessage(MySim);
            message.Addressee = FindAssistant(SimId.SchedulerCustomerArrival);
            MyManager.StartContinualAssistant(message);
        }
    }
}