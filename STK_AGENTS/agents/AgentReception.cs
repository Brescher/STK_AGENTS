using OSPABA;
using simulation;
using managers;
using continualAssistants;
using STK_AGENTS.entities;
using OSPDataStruct;
using OSPStat;

namespace agents
{
    //meta! id="4"
	public class AgentReception : Agent
	{
        public List<Clerk> Clerks;

		public SimQueue<Clerk> FreeClerks;
		public SimQueue<MyMessage> CustomersInCheckInQueue;
		public SimQueue<MyMessage> CustomersInPaymentQueue;
		public SimQueue<Customer> CustomersInCheckIn;
        public Stat TimeInCheckInQueue { get; set; }
        public Stat TimeInPaymentQueue { get; set; }
        public Stat TimeInCheckIn { get; set; }

		public int ClerksLunchDone, ClerksSendToLunch;

        public bool SendFreeWorkersToLunch;
        public AgentReception(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
			FreeClerks = new SimQueue<Clerk>(new WStat(MySim));
            CustomersInCheckInQueue = new SimQueue<MyMessage>(new WStat(MySim));
            CustomersInPaymentQueue = new SimQueue<MyMessage>(new WStat(MySim));
            CustomersInCheckIn = new SimQueue<Customer>(new WStat(MySim));
			TimeInCheckInQueue = new Stat();
            TimeInCheckIn = new Stat();
            TimeInPaymentQueue = new Stat();
            MySimulation sim = (MySimulation)MySim;
			Clerks = new List<Clerk>(sim.NumberOfClerks);
			MySimulation tempSim = (MySimulation)MySim;
			ClerksLunchDone = 0;
            ClerksSendToLunch = 0;
			SendFreeWorkersToLunch = false;

            for (int i = 0; i < tempSim.NumberOfClerks; i++)
			{
				Clerks.Add(new Clerk(MySim));
				FreeClerks.AddLast(Clerks[i]);
			}

        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerReception(SimId.ManagerReception, MySim, this);
			new ProcessPayment(SimId.ProcessPayment, MySim, this);
			new ProcessLunchBreakClerk(SimId.ProcessLunchBreakClerk, MySim, this);
			new ProcessCheckIn(SimId.ProcessCheckIn, MySim, this);
			AddOwnMessage(Mc.CustomerServiceSendToInspection);
			AddOwnMessage(Mc.CustomerServiceReception);
			AddOwnMessage(Mc.GetNumberOfFreeMechanicsReception);
			AddOwnMessage(Mc.FreedResourceSTK);
		}
		//meta! tag="end"
	}
}