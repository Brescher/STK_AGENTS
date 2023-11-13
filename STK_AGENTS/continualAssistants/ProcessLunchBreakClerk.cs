using OSPABA;
using simulation;
using agents;
using STK_AGENTS.simulation;

namespace continualAssistants
{
	//meta! id="41"
	public class ProcessLunchBreakClerk : Process
	{
		double delay;
		Random random;
		public ProcessLunchBreakClerk(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
            MyAgent.AddOwnMessage(Mc.LunchDone);
            random = new Random();
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            delay = 0.0000001;
        }

		//meta! sender="AgentReception", id="42", type="Start"
		public void ProcessStart(MessageForm message)
		{
            MyMessage msg = (MyMessage)message;
            msg.Clerk.Job = "On lunch break";
            msg.Clerk.CustomerId = 0;
            msg.Clerk.HadLunch = true;
            msg.Code = Mc.LunchDone;
            Hold(30 + random.NextDouble()/100, msg);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.LunchDone:
					if (MySim.CurrentTime < 241d)
					{
                        MyAgent.ClerksLunchDone++;
                    }
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