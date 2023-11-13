using OSPABA;
using simulation;
using agents;
using continualAssistants;
using STK_AGENTS.entities;
using STK_AGENTS.simulation;

namespace managers
{
	//meta! id="5"
	public class ManagerWorkshop : Manager
	{
        
		public ManagerWorkshop(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AgentSTK", id="16", type="Request"
		public void ProcessCustomerServiceInspection(MessageForm message)
		{
            MyMessage msg = (MyMessage)message;
            msg.Customer.ParkingStartWaitTime = MySim.CurrentTime;
            if (!((MySimulation)MySim).Validation)
			{
				if(msg.AssignedResource == Config.AssignedMechanicGr1)
				{
                    msg.Mechanic = MyAgent.FreeMechanicsGr1.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                } 
				else if(msg.AssignedResource == Config.AssignedMechanicGr2)
				{
                    msg.Mechanic = MyAgent.FreeMechanicsGr2.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                } 
				else if(msg.AssignedResource == Config.AssignedParkingSpace && msg.Customer.TypeOfCar == Config.Truck && MyAgent.NumberOfFreeMechanicsGr2 > 0)
				{
					MyAgent.NumberOfFreeMechanicsGr2--;
					MyAgent.NumberOfFreeSpaceParking++;
					msg.AssignedResource = Config.AssignedMechanicGr2;
					msg.Mechanic = MyAgent.FreeMechanicsGr2.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                }
				else if(msg.AssignedResource == Config.AssignedParkingSpace && msg.Customer.TypeOfCar != Config.Truck && MyAgent.NumberOfFreeMechanicsGr1 > 0)
				{
                    MyAgent.NumberOfFreeMechanicsGr1--;
                    MyAgent.NumberOfFreeSpaceParking++;
                    msg.AssignedResource = Config.AssignedMechanicGr1;
                    msg.Mechanic = MyAgent.FreeMechanicsGr1.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                } 
				else if(msg.AssignedResource == Config.AssignedParkingSpace && msg.Customer.TypeOfCar != Config.Truck && MyAgent.NumberOfFreeMechanicsGr2 > 0)
				{
                    MyAgent.NumberOfFreeMechanicsGr2--;
                    MyAgent.NumberOfFreeSpaceParking++;
                    msg.AssignedResource = Config.AssignedMechanicGr2;
                    msg.Mechanic = MyAgent.FreeMechanicsGr2.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                } else
				{
					msg.Customer.Position = "In parking lot";
                    MyAgent.CustomersInParkingLot.Enqueue(msg);
                }
            }
			else
			{
                if (msg.AssignedResource == Config.AssignedParkingSpace && MyAgent.NumberOfFreeMechanicsGr1 == 0)
                {
                    msg.Customer.Position = "In parking lot";
                    MyAgent.CustomersInParkingLot.Enqueue(msg);
                }
                else
                {
                    if (msg.AssignedResource == Config.AssignedParkingSpace && MyAgent.NumberOfFreeMechanicsGr1 > 0)
                    {
                        MyAgent.NumberOfFreeMechanicsGr1--;
                        MyAgent.NumberOfFreeSpaceParking++;
                        msg.AssignedResource = Config.AssignedMechanicGr1;
                    }
                    msg.Mechanic = MyAgent.FreeMechanicsGr1.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                }
            }
            
			
        }

		//meta! sender="ProcessInspection", id="34", type="Finish"
        //koniec inspekcie 
		public void ProcessFinishProcessInspection(MessageForm message)
		{
            MyMessage responseMessage = (MyMessage)message;
            

			MyMessage copy = (MyMessage)message.CreateCopy();

			if (!((MySimulation)MySim).Validation)
			{
                //kontrola ci mechanik bol na obede a ci je cas na obed
                if(!copy.Mechanic.HadLunch && MySim.CurrentTime >= 120 && MySim.CurrentTime <= 210)
                {
                    //raz treba poslat vsetkych volnych mechanikov na obed
                    if (!MyAgent.SendFreeWorkersToLunch)
                    {
                        SendFreeWorkersToLunch();
                        MyAgent.SendFreeWorkersToLunch = true;
                    }
                    copy.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakMechanic);
                    StartContinualAssistant(copy);
                }
                else if (MyAgent.CustomersInParkingLot.Count > 0)
                {
                    //kontrola ci caka na parkovisku auto ktore moze skontrolvoat dany mechanik
                    if (copy.AssignedResource == Config.AssignedMechanicGr1)
                    {
                        MyMessage msg = FindCarForMechanicGr1();
                        if (msg != null)
                        {
                            responseMessage.AssignedResource = Config.AssignedParkingSpace;
                            MyAgent.NumberOfFreeSpaceParking++;
                            msg.AssignedResource = Config.AssignedMechanicGr1;
                            msg.Mechanic = copy.Mechanic;
                            msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                            StartContinualAssistant(msg);
                        } else
                        {
                            responseMessage.AssignedResource = Config.AssignedMechanicGr1;
                            copy.Mechanic.Job = "Idle";
                            copy.Mechanic.CustomerId = 0;
                            MyAgent.NumberOfFreeMechanicsGr1++;
                            MyAgent.FreeMechanicsGr1.Enqueue((MechanicGr1)copy.Mechanic);
                        }
                    }
                    else if (copy.AssignedResource == Config.AssignedMechanicGr2)
                    {
                        responseMessage.AssignedResource = Config.AssignedParkingSpace;
                        MyMessage msg = FindCarForMechanicGr2();
                        MyAgent.NumberOfFreeSpaceParking++;
                        msg.AssignedResource = Config.AssignedMechanicGr2;
                        msg.Mechanic = copy.Mechanic;
                        msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                        StartContinualAssistant(msg);
                    }
                }
                else
                {
                    copy.Mechanic.Job = "Idle";
                    copy.Mechanic.CustomerId = 0;
					if(copy.AssignedResource == Config.AssignedMechanicGr1)
					{
                        responseMessage.AssignedResource = Config.AssignedMechanicGr1;
                        MyAgent.NumberOfFreeMechanicsGr1++;
                        MyAgent.FreeMechanicsGr1.Enqueue((MechanicGr1)copy.Mechanic);
                    }
                    else if(copy.AssignedResource == Config.AssignedMechanicGr2)
                    {
                        responseMessage.AssignedResource = Config.AssignedMechanicGr2;
                        MyAgent.NumberOfFreeMechanicsGr2++;
                        MyAgent.FreeMechanicsGr2.Enqueue((MechanicGr2)copy.Mechanic);
                    }
                    
                }
            } else
			{
                if (MyAgent.CustomersInParkingLot.Count > 0)
                {
                    MyAgent.NumberOfFreeSpaceParking++;
                    MyMessage newMessage = (MyMessage)MyAgent.CustomersInParkingLot.Dequeue();
                    newMessage.AssignedResource = Config.AssignedMechanicGr1;
                    newMessage.Mechanic = copy.Mechanic;
                    newMessage.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(newMessage);
                }
                else
                {
                    copy.Mechanic.Job = "Idle";
                    copy.Mechanic.CustomerId = 0;
                    MyAgent.NumberOfFreeMechanicsGr1++;
                    MyAgent.FreeMechanicsGr1.Enqueue((MechanicGr1)copy.Mechanic);
                }
            }

            responseMessage.Code = Mc.CustomerServiceInspection;
            Response(responseMessage);
        }

		//meta! sender="ProcessLunchBreakMechanic", id="45", type="Finish"
        //poobede mechanik bud preberie auto z parkoviska alebo sa uvolni a oboznami sa pokladna ze sa uvolnil zdroj
		public void ProcessFinishProcessLunchBreakMechanic(MessageForm message)
		{
            MyMessage originalMsg = (MyMessage)message;
            MyMessage copy = (MyMessage)originalMsg.CreateCopy();
            if (MyAgent.CustomersInParkingLot.Count > 0)
            {
                if (originalMsg.AssignedResource == Config.AssignedMechanicGr1)
                {
                    MyMessage msg = FindCarForMechanicGr1();
                    if (msg != null)
                    {
                        copy.AssignedResource = Config.AssignedParkingSpace;
                        MyAgent.NumberOfFreeSpaceParking++;
                        msg.AssignedResource = Config.AssignedMechanicGr1;
                        msg.Mechanic = originalMsg.Mechanic;
                        msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                        StartContinualAssistant(msg);
                    }
                    else
                    {
                        copy.AssignedResource = Config.AssignedMechanicGr1;
                        copy.Mechanic.Job = "Idle";
                        copy.Mechanic.CustomerId = 0;
                        MyAgent.NumberOfFreeMechanicsGr1++;
                        MyAgent.FreeMechanicsGr1.Enqueue((MechanicGr1)copy.Mechanic);
                    }
                }
                else if (originalMsg.AssignedResource == Config.AssignedMechanicGr2)
                {
                    MyMessage msg = FindCarForMechanicGr2();
                    copy.AssignedResource = Config.AssignedParkingSpace;
                    MyAgent.NumberOfFreeSpaceParking++;
                    msg.AssignedResource = Config.AssignedMechanicGr2;
                    msg.Mechanic = originalMsg.Mechanic;
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessInspection);
                    StartContinualAssistant(msg);
                }
            }
            else
            {
                originalMsg.Mechanic.Job = "Idle";
                originalMsg.Mechanic.CustomerId = 0;
                if (originalMsg.AssignedResource == Config.AssignedMechanicGr1)
                {
                    copy.AssignedResource = Config.AssignedMechanicGr1;
                    MyAgent.NumberOfFreeMechanicsGr1++;
                    MyAgent.FreeMechanicsGr1.Enqueue((MechanicGr1)originalMsg.Mechanic);
                }
                else if (originalMsg.AssignedResource == Config.AssignedMechanicGr2)

                {
                    copy.AssignedResource = Config.AssignedMechanicGr2;
                    MyAgent.NumberOfFreeMechanicsGr2++;
                    MyAgent.FreeMechanicsGr2.Enqueue((MechanicGr2)originalMsg.Mechanic);
                }

            }

            copy.Addressee = MySim.FindAgent(SimId.AgentSTK);
            copy.Code = Mc.FreedResourceWorkshop;
            Notice(copy);
        }

		//meta! sender="AgentSTK", id="25", type="Request"
        //pridelenie zdroju zakaznikovy, co sa ma vykonat na pokladni so zakaznikom
		public void ProcessGetNumberOfFreeMechanicsWorkshop(MessageForm message)
		{
			MyMessage msg = (MyMessage)message;
			if (!((MySimulation)MySim).Validation)
			{
                if(msg.TypeOfCar == Config.Truck && MyAgent.NumberOfFreeMechanicsGr2 > 0)
				{
					msg.AssignedResource = Config.AssignedMechanicGr2;
					MyAgent.NumberOfFreeMechanicsGr2--;
				} 
				else if(msg.TypeOfCar != Config.Truck && MyAgent.NumberOfFreeMechanicsGr1 > 0)
				{
					msg.AssignedResource = Config.AssignedMechanicGr1;
					MyAgent.NumberOfFreeMechanicsGr1--;
				}
                else if (msg.TypeOfCar != Config.Truck && MyAgent.NumberOfFreeMechanicsGr2 > 0)
                {
                    msg.AssignedResource = Config.AssignedMechanicGr2;
                    MyAgent.NumberOfFreeMechanicsGr2--;
                }
                else if (MyAgent.NumberOfFreeSpaceParking > 0)
                {
                    msg.AssignedResource = Config.AssignedParkingSpace;
                    MyAgent.NumberOfFreeSpaceParking--;
                }
                else
                {
                    msg.AssignedResource = Config.AssignedQueue;
                }
            } else
			{
                if (MyAgent.NumberOfFreeMechanicsGr1 > 0)
                {
                    msg.AssignedResource = Config.AssignedMechanicGr1;
                    MyAgent.NumberOfFreeMechanicsGr1--;
                }
                else if (MyAgent.NumberOfFreeSpaceParking > 0)
                {
                    msg.AssignedResource = Config.AssignedParkingSpace;
                    MyAgent.NumberOfFreeSpaceParking--;
                }
                else
                {
                    msg.AssignedResource = Config.AssignedQueue;
                }
            }

			msg.Code = Mc.GetNumberOfFreeMechanicsWorkshop;
			Response(msg);
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
			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.ProcessInspection:
					ProcessFinishProcessInspection(message);
				break;

				case SimId.ProcessLunchBreakMechanic:
					ProcessFinishProcessLunchBreakMechanic(message);
				break;
				}
			break;

			case Mc.CustomerServiceInspection:
				ProcessCustomerServiceInspection(message);
			break;

			case Mc.GetNumberOfFreeMechanicsWorkshop:
				ProcessGetNumberOfFreeMechanicsWorkshop(message);
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
        //vyber auta z parkoviska pre mechanika typu 1
        private MyMessage FindCarForMechanicGr1()
        {
            foreach(MyMessage msg in MyAgent.CustomersInParkingLot)
            {
                if(msg.Customer.TypeOfCar == Config.Car || msg.Customer.TypeOfCar == Config.Van)
                {
                    MyAgent.CustomersInParkingLot.Remove(msg);
                    return msg;
                }
            }
            return null;
        }
        //vyber auta z parkoviska pre mechanika typu 2
        private MyMessage FindCarForMechanicGr2()
        {
            foreach (MyMessage msg in MyAgent.CustomersInParkingLot)
            {
                if (msg.Customer.TypeOfCar == Config.Truck)
                {
                    MyAgent.CustomersInParkingLot.Remove(msg);
                    return msg;
                }
            }
            return MyAgent.CustomersInParkingLot.Dequeue();
        }
        //odoslanie volnych mechanikov na obed
        private void SendFreeWorkersToLunch()
        {
            int number = MyAgent.NumberOfFreeMechanicsGr1;
            for (int i = 0;i < number; i++)
            {
                MyMessage msg = new MyMessage(MySim);
                MyAgent.NumberOfFreeMechanicsGr1--;
                msg.Mechanic = MyAgent.FreeMechanicsGr1.Dequeue();
                msg.AssignedResource = Config.AssignedMechanicGr1;
                msg.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakMechanic);
                StartContinualAssistant(msg);
            }

            number = MyAgent.NumberOfFreeMechanicsGr2;
            for (int i = 0; i < number; i++)
            {
                MyMessage msg = new MyMessage(MySim);
                MyAgent.NumberOfFreeMechanicsGr2--;
                msg.Mechanic = MyAgent.FreeMechanicsGr2.Dequeue();
                msg.AssignedResource = Config.AssignedMechanicGr2;
                msg.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakMechanic);
                StartContinualAssistant(msg);
            }
        }
    }
}