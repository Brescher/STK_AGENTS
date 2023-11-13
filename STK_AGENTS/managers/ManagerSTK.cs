using OSPABA;
using simulation;
using agents;
using continualAssistants;

namespace managers
{
	//meta! id="3"
	public class ManagerSTK : Manager
	{
		public ManagerSTK(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
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

		//meta! sender="AgentModel", id="14", type="Request"
		public void ProcessCustomerService(MessageForm message)
		{
            message.Code = Mc.CustomerServiceReception;
            message.Addressee = MySim.FindAgent(SimId.AgentReception);
            Request(message);
        }

		//meta! sender="AgentWorkshop", id="16", type="Response"
		public void ProcessCustomerServiceInspection(MessageForm message)
		{
            message.Code = Mc.CustomerServiceSendToInspection;
            Response(message);
        }

		//meta! sender="AgentReception", id="51", type="Request"
		public void ProcessCustomerServiceSendToInspection(MessageForm message)
		{
            message.Code = Mc.CustomerServiceInspection;
            message.Addressee = MySim.FindAgent(SimId.AgentWorkshop);
            Request(message);
        }

		//meta! sender="AgentReception", id="15", type="Response"
		public void ProcessCustomerServiceReception(MessageForm message)
		{
            message.Code = Mc.CustomerService;
            Response(message);
        }

		//meta! sender="AgentReception", id="24", type="Request"
		public void ProcessGetNumberOfFreeMechanicsReception(MessageForm message)
		{
            message.Code = Mc.GetNumberOfFreeMechanicsWorkshop;
            message.Addressee = MySim.FindAgent(SimId.AgentWorkshop);
            Request(message);
        }

		//meta! sender="AgentWorkshop", id="25", type="Response"
		public void ProcessGetNumberOfFreeMechanicsWorkshop(MessageForm message)
		{
            message.Code = Mc.GetNumberOfFreeMechanicsReception;
            Response(message);
        }

		public void ProcessFreedResourceWorkshop(MessageForm message)
		{
            message.Code = Mc.FreedResourceSTK;
            message.Addressee = MySim.FindAgent(SimId.AgentReception);
            Notice(message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.CustomerServiceSendToInspection:
				ProcessCustomerServiceSendToInspection(message);
			break;

			case Mc.GetNumberOfFreeMechanicsReception:
				ProcessGetNumberOfFreeMechanicsReception(message);
			break;

			case Mc.CustomerServiceInspection:
				ProcessCustomerServiceInspection(message);
			break;

			case Mc.CustomerService:
				ProcessCustomerService(message);
			break;

			case Mc.CustomerServiceReception:
				ProcessCustomerServiceReception(message);
			break;

			case Mc.GetNumberOfFreeMechanicsWorkshop:
				ProcessGetNumberOfFreeMechanicsWorkshop(message);
			break;

			case Mc.FreedResourceWorkshop:
				ProcessFreedResourceWorkshop(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentSTK MyAgent
		{
			get
			{
				return (AgentSTK)base.MyAgent;
			}
		}
	}
}