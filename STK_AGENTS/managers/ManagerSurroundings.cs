using OSPABA;
using simulation;
using agents;
using continualAssistants;
using STK_AGENTS;
using STK_AGENTS.simulation;
using OSPRNG;

namespace managers
{
	//meta! id="2"
	public class ManagerSurroundings : Manager
	{
        public ManagerSurroundings(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AgentModel", id="13", type="Notice"
		public void ProcessInit(MessageForm message)
		{
            MyAgent.StartSimulation();
        }

		//meta! sender="AgentModel", id="11", type="Notice"
		public void ProcessCustomerDeparture(MessageForm message)
		{
			MyMessage msg = (MyMessage) message;
			MyAgent.CustomersDeparted++;
			MyAgent.TimeInSystem.AddSample(msg.Customer.GetTimeInSystem());
			MyAgent.CustomersInSTK.Remove(msg.Customer);
        }

		//meta! sender="SchedulerCustomerArrival", id="23", type="Finish"
		public void ProcessFinish(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="SchedulerCustomerArrival", id="57", type="Notice"
		public void ProcessNewCustomer(MessageForm message)
		{
			MyMessage msg = (MyMessage) message;

            if (Config.Cooling < MySim.CurrentTime)
            {
                message.Addressee = MyAgent.FindAssistant(SimId.SchedulerCustomerArrival);
                BreakContinualAssistant(message);
            }
            else
            {
                message.Code = Mc.CustomerArrival;
                message.Addressee = MySim.FindAgent(SimId.AgentModel);
                Notice(message);

                MyAgent.CustomersArrived++;
                MyAgent.CustomersInSTK.Enqueue(msg.Customer);
            }
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Init:
				ProcessInit(message);
			break;

			case Mc.NewCustomer:
				ProcessNewCustomer(message);
			break;

			case Mc.Finish:
				ProcessFinish(message);
			break;

			case Mc.CustomerDeparture:
				ProcessCustomerDeparture(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentSurroundings MyAgent
		{
			get
			{
				return (AgentSurroundings)base.MyAgent;
			}
		}
    }
}