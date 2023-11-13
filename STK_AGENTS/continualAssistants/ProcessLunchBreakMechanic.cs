using OSPABA;
using simulation;
using agents;
using STK_AGENTS.simulation;
using STK_AGENTS.entities;

namespace continualAssistants
{
	//meta! id="44"
	public class ProcessLunchBreakMechanic : Process
	{
        double delay;
        public ProcessLunchBreakMechanic(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
			MyAgent.AddOwnMessage(Mc.LunchDone);
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            delay = 0.000001;
        }

		//meta! sender="AgentWorkshop", id="45", type="Start"
		public void ProcessStart(MessageForm message)
		{
			MyMessage msg = (MyMessage)message;
			msg.Mechanic.Job = "On lunch break";
			msg.Mechanic.CustomerId = 0;
            msg.Mechanic.HadLunch = true;
			msg.Code = Mc.LunchDone;
			Hold(30, msg);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.LunchDone:
                    MyMessage msg = (MyMessage)message;
					if(MySim.CurrentTime < 241d)
					{
                        if (msg.AssignedResource == Config.AssignedMechanicGr1)
                        {
                            MyAgent.MechanicsGr1LunchDone++;
                        }
                        else if (msg.AssignedResource == Config.AssignedMechanicGr2)
                        {
                            MyAgent.MechanicsGr2LunchDone++;
                        }
                        else
                        {
                            throw new Exception("this should not happen");
                        }
                    }
					AssistantFinished(msg);
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
		public new AgentWorkshop MyAgent
		{
			get
			{
				return (AgentWorkshop)base.MyAgent;
			}
		}
	}
}