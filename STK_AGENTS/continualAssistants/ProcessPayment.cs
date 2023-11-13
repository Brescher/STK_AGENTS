using OSPABA;
using simulation;
using agents;
using OSPRNG;
using STK_AGENTS.simulation;

namespace continualAssistants
{
	//meta! id="28"
	public class ProcessPayment : Process
	{
		UniformContinuousRNG paymentGen;
		public ProcessPayment(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
            MySimulation sim = (MySimulation)MySim;
            MyAgent.AddOwnMessage(Mc.PaymentDone);
            paymentGen = new UniformContinuousRNG(Config.PaymentTime[0], Config.PaymentTime[1], sim.SeedGenerator);
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentReception", id="29", type="Start"
		public void ProcessStart(MessageForm message)
		{
            MyMessage msg = (MyMessage)message;
			
            msg.Customer.Position = "Paying";
			MyAgent.TimeInPaymentQueue.AddSample(msg.Customer.GetTimeInQueueCPayment());
            msg.Clerk.Job = "Paying";
            msg.Clerk.CustomerId = msg.Customer.Id;
            message.Code = Mc.PaymentDone;
            Hold(paymentGen.Sample(), message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            case Mc.PaymentDone:
					
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