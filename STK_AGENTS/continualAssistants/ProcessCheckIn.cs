using OSPABA;
using simulation;
using agents;
using OSPRNG;
using STK_AGENTS.simulation;

namespace continualAssistants
{
	//meta! id="26"
	public class ProcessCheckIn : Process
	{
		TriangularRNG customerCheckIn;
		public ProcessCheckIn(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
            MySimulation sim = (MySimulation)MySim;
            MyAgent.AddOwnMessage(Mc.CheckInDone);
            customerCheckIn = new TriangularRNG(Config.CheckInTime[0], Config.CheckInTime[1], Config.CheckInTime[2], sim.SeedGenerator);
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentReception", id="27", type="Start"
		public void ProcessStart(MessageForm message)
		{
			MyMessage msg = (MyMessage)message;
            MyAgent.CustomersInCheckIn.Enqueue(msg.Customer);
            msg.Customer.Position = "Checking in";
            MyAgent.TimeInCheckInQueue.AddSample(msg.Customer.GetTimeInQueueCheckIn());
            msg.Clerk.Job = "Checking in";
			msg.Clerk.CustomerId = msg.Customer.Id;
            message.Code = Mc.CheckInDone;

			double timeInCheckIn = customerCheckIn.Sample();
			MyAgent.TimeInCheckIn.AddSample(timeInCheckIn);
            Hold(timeInCheckIn, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.CheckInDone:
                    MyMessage msg = (MyMessage)message;
                    MyAgent.CustomersInCheckIn.Remove(msg.Customer);
                    AssistantFinished(message);
			break;
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Start:
				ProcessStart(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentReception MyAgent
		{
			get
			{
				return (AgentReception)base.MyAgent;
			}
		}
    }
}