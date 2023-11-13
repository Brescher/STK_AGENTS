using OSPABA;
using simulation;
using managers;
using continualAssistants;

namespace agents
{
	//meta! id="3"
	public class AgentSTK : Agent
	{
		public AgentSTK(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerSTK(SimId.ManagerSTK, MySim, this);
			AddOwnMessage(Mc.CustomerService);
			AddOwnMessage(Mc.CustomerServiceInspection);
			AddOwnMessage(Mc.CustomerServiceSendToInspection);
			AddOwnMessage(Mc.CustomerServiceReception);
			AddOwnMessage(Mc.GetNumberOfFreeMechanicsReception);
			AddOwnMessage(Mc.GetNumberOfFreeMechanicsWorkshop);
			AddOwnMessage(Mc.FreedResourceWorkshop);
		}
		//meta! tag="end"
	}
}