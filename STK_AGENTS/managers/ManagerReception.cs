using OSPABA;
using simulation;
using agents;
using continualAssistants;
using STK_AGENTS.simulation;
using STK_AGENTS.entities;

namespace managers
{
	//meta! id="4"
	public class ManagerReception : Manager
	{
		public ManagerReception(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AgentSTK", id="51", type="Response"
		//prichod zakaznika z inspekcie na platbu a pridelenie novej roboty lebo sa uvolnil mechanik/parkovisko
		public void ProcessCustomerServiceSendToInspection(MessageForm message)
		{
			//poslanie zakaznika na platbu
			MyMessage msg = (MyMessage)message;
			msg.Customer.QueueDepartureStartWaitTime = MySim.CurrentTime;
			if(MyAgent.FreeClerks.Count > 0 && MyAgent.CustomersInPaymentQueue.Count == 0)
			{
				msg.Clerk = MyAgent.FreeClerks.Dequeue();
				msg.Customer.ServingClerk = msg.Clerk;
				msg.Addressee = MyAgent.FindAssistant(SimId.ProcessPayment);
				StartContinualAssistant(msg);
			} else
			{
				MyAgent.CustomersInPaymentQueue.Enqueue(msg);
			}

			//vyber dalsieho zakaznika z check in frontu ak sa da, lebo sa uvolnil zdroj
            if (MyAgent.CustomersInCheckInQueue.Count > 0 && MyAgent.FreeClerks.Count > 0)
            {
                MyMessage copy = (MyMessage)msg.CreateCopy();
				if (copy.AssignedResource == Config.AssignedMechanicGr1)
				{
					MyMessage nextCustomer = GetCarForMechanicGr1();
					if (nextCustomer != null)
					{
						nextCustomer.TypeOfCar = nextCustomer.Customer.TypeOfCar;
						nextCustomer.Code = Mc.GetNumberOfFreeMechanicsReception;
						nextCustomer.Addressee = MySim.FindAgent(SimId.AgentSTK);
						Request(nextCustomer);
					}
				}
				else
				{
					copy = MyAgent.CustomersInCheckInQueue.Dequeue();
					copy.TypeOfCar = copy.Customer.TypeOfCar;
                    copy.Code = Mc.GetNumberOfFreeMechanicsReception;
                    copy.Addressee = MySim.FindAgent(SimId.AgentSTK);
                    Request(copy);
                }
            }
        }

		//meta! sender="AgentSTK", id="15", type="Request"
		//prichod zakaznika do recepcie, kontrola ci ide do frontu alebo sa pojde preberat
		public void ProcessCustomerServiceReception(MessageForm message)
		{
            MyMessage msg = (MyMessage)message;

			if (!((MySimulation)MySim).Validation)
			{
                if (MyAgent.CustomersInCheckInQueue.Count == 0 && MyAgent.FreeClerks.Count > 0)
                {
                    msg.TypeOfCar = msg.Customer.TypeOfCar;
                    msg.Code = Mc.GetNumberOfFreeMechanicsReception;
                    msg.Addressee = MySim.FindAgent(SimId.AgentSTK);
                    Request(msg);
                }
                else
                {
					//kontrola èi je typ auta vo frotne pred prevzatím, ak napr. neèaká žiadny truck vo fronte, tak môže is na prevzatie
					bool typeOfCarInQueue = false;
					if (MyAgent.FreeClerks.Count > 0)
					{
						foreach (MyMessage item in MyAgent.CustomersInCheckInQueue)
						{
							if (msg.Customer.TypeOfCar == item.Customer.TypeOfCar)
							{
								typeOfCarInQueue = true;
								break;
							}
							else if (msg.Customer.TypeOfCar != Config.Truck && item.Customer.TypeOfCar != Config.Truck)
							{
								typeOfCarInQueue = true;
								break;
							}
						}
					}

					if (typeOfCarInQueue == false && MyAgent.FreeClerks.Count > 0)
					{
						msg.TypeOfCar = msg.Customer.TypeOfCar;
						msg.Code = Mc.GetNumberOfFreeMechanicsReception;
						msg.Addressee = MySim.FindAgent(SimId.AgentSTK);
						Request(msg);
					}
					else
					{
						msg.Customer.Position = "In check in queue";
						MyAgent.CustomersInCheckInQueue.Enqueue(msg);
					}
				}
            } else
			{
                if (MyAgent.CustomersInCheckInQueue.Count == 0 && MyAgent.FreeClerks.Count > 0)
                {
                    msg.TypeOfCar = msg.Customer.TypeOfCar;
                    msg.Code = Mc.GetNumberOfFreeMechanicsReception;
                    msg.Addressee = MySim.FindAgent(SimId.AgentSTK);
                    Request(msg);
                }
                else
                {
                    msg.Customer.Position = "In check in queue";
                    MyAgent.CustomersInCheckInQueue.Enqueue(msg);
                }
            }
        }

		//meta! sender="ProcessCheckIn", id="27", type="Finish"
		//poslanie zakaznika na inspekciu a pridelenie roboty pokladnikovy
		public void ProcessFinishProcessCheckIn(MessageForm message)
		{
			message.Code = Mc.CustomerServiceSendToInspection;
			message.Addressee = MySim.FindAgent(SimId.AgentSTK);
			Request(message);

            MyMessage msg = (MyMessage)message.CreateCopy();
            MyMessage copy = (MyMessage)message.CreateCopy();
            if (!((MySimulation)MySim).Validation)
            {
                if (!copy.Clerk.HadLunch && MySim.CurrentTime >= 120 && MySim.CurrentTime <= 210)
				{
                //raz treba poslat vsetkych volnych pokladnikov na obed
					if (!MyAgent.SendFreeWorkersToLunch)
					{
						SendFreeWorkersToLunch();
						MyAgent.SendFreeWorkersToLunch = true;
					}

					if (MySim.CurrentTime > 195 ||
						MyAgent.Clerks.Count - MyAgent.FreeClerks.Count > 5 ||
						MyAgent.ClerksSendToLunch < MyAgent.Clerks.Count / 2 ||
						MyAgent.ClerksSendToLunch == 0 ||
						MyAgent.ClerksLunchDone >= MyAgent.Clerks.Count / 2)
					{
						MyAgent.ClerksSendToLunch++;
						copy.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakClerk);
						StartContinualAssistant(copy);
					}
					else
					{
						AssignWorkToClerk(msg);
					}
				}else
				{
					AssignWorkToClerk(msg);
				}
			}
            else
            {
                AssignWorkToClerk(msg);
            }
        }

		//meta! sender="ProcessPayment", id="29", type="Finish"
		//odchod zakaznika po platbe a pridelenie novej roboty pokladnikovy
		public void ProcessFinishProcessPayment(MessageForm message)
		{
			message.Code = Mc.CustomerServiceReception;
			Response(message);

            MyMessage msg = (MyMessage)message.CreateCopy();
            MyMessage copy = (MyMessage)message.CreateCopy();
            if (!((MySimulation)MySim).Validation)
			{
                if (!copy.Clerk.HadLunch && MySim.CurrentTime >= 120 && MySim.CurrentTime <= 210)
                {
                    //raz treba poslat vsetkych volnych pokladnikov na obed
                    if (!MyAgent.SendFreeWorkersToLunch)
                    {
                        SendFreeWorkersToLunch();
                        MyAgent.SendFreeWorkersToLunch = true;
                    }

                    if (MySim.CurrentTime > 195 ||
                        MyAgent.Clerks.Count - MyAgent.FreeClerks.Count > 5 ||
                        MyAgent.ClerksSendToLunch < MyAgent.Clerks.Count / 2 ||
                        MyAgent.ClerksSendToLunch == 0 ||
                        MyAgent.ClerksLunchDone >= MyAgent.Clerks.Count / 2)
                    {
                        MyAgent.ClerksSendToLunch++;
                        copy.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakClerk);
                        StartContinualAssistant(copy);
                    }
                    else
                    {
                        AssignWorkToClerk(msg);
                    }

                }
                //metodka
                else
                {
                    AssignWorkToClerk(msg);
                }
            } else
			{
                AssignWorkToClerk(msg);
            }
                
        }

		//meta! sender="ProcessLunchBreakClerk", id="42", type="Finish"
		//priradenie roboty pokladnikovy poobede
		public void ProcessFinishProcessLunchBreakClerk(MessageForm message)
		{
            AssignWorkToClerk((MyMessage)message);
        }

        //meta! sender="AgentSTK", id="24", type="Response"
        //podla priradeneho zdroja posli zakaznika tam, kam ma ist
        public void ProcessGetNumberOfFreeMechanicsReception(MessageForm message)
		{
            MyMessage msg = (MyMessage)message;
            MyMessage copy = (MyMessage)message.CreateCopy();
			
			if(msg.AssignedResource == Config.AssignedQueue)
			{
				if(msg.Customer != null)
				{
					msg.Customer.Position = "In check in queue";
                    MyAgent.CustomersInCheckInQueue.Enqueue(msg);
                }
			} else 
			{
				if(MyAgent.FreeClerks.Count > 0 )
				{
					if(msg.Customer == null)
					{
						msg = MyAgent.CustomersInCheckInQueue.Dequeue();
						msg.AssignedResource = copy.AssignedResource;
					}
					
                    msg.Clerk = MyAgent.FreeClerks.Dequeue();
					msg.Customer.ServingClerk = msg.Clerk;
					msg.Addressee = MyAgent.FindAssistant(SimId.ProcessCheckIn);
					StartContinualAssistant(msg);
				}else
				{
                    if (msg.Customer != null)
                    {
                        msg.Customer.Position = "In check in queue";
                        MyAgent.CustomersInCheckInQueue.Enqueue(msg);
                    }
                }
			}
        }

		//po obede mechanikov sa zavola tato metoda, lebo sa vratili z obedu, cize sa uvolnili zdroje
		public void ProcessFreedResourceSTK(MessageForm message)
		{
			if (MyAgent.CustomersInCheckInQueue.Count > 0 && MyAgent.FreeClerks.Count > 0)
			{
                MyMessage copy = (MyMessage)message.CreateCopy();
                if (copy.AssignedResource == Config.AssignedMechanicGr1)
                {
                    MyMessage nextCustomer = GetCarForMechanicGr1();
                    if (nextCustomer != null)
                    {
                        nextCustomer.TypeOfCar = nextCustomer.Customer.TypeOfCar;
                        nextCustomer.Code = Mc.GetNumberOfFreeMechanicsReception;
                        nextCustomer.Addressee = MySim.FindAgent(SimId.AgentSTK);
                        Request(nextCustomer);
                    }
                }
                else
                {
                    copy = MyAgent.CustomersInCheckInQueue.Dequeue();
                    copy.TypeOfCar = copy.Customer.TypeOfCar;
                    copy.Code = Mc.GetNumberOfFreeMechanicsReception;
                    copy.Addressee = MySim.FindAgent(SimId.AgentSTK);
                    Request(copy);
                }
            }
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

			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.ProcessCheckIn:
					ProcessFinishProcessCheckIn(message);
				break;

				case SimId.ProcessPayment:
					ProcessFinishProcessPayment(message);
				break;

				case SimId.ProcessLunchBreakClerk:
					ProcessFinishProcessLunchBreakClerk(message);
				break;
				}
			break;

			case Mc.GetNumberOfFreeMechanicsReception:
				ProcessGetNumberOfFreeMechanicsReception(message);
			break;

			case Mc.CustomerServiceReception:
				ProcessCustomerServiceReception(message);
			break;

            case Mc.FreedResourceSTK:
				ProcessFreedResourceSTK(message);
            break;

            case Mc.NextClerkToLunch:
				SendFreeWorkersToLunch();
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
		//vyber auta ktore moze skontrolovat mechanik skupiny 1
		private MyMessage GetCarForMechanicGr1()
		{
			foreach(MyMessage msg in MyAgent.CustomersInCheckInQueue)
			{
				if(msg.Customer.TypeOfCar == Config.Car || msg.Customer.TypeOfCar == Config.Van)
				{
					return MyAgent.CustomersInCheckInQueue.Dequeue();

                }
            }
			return null;
		}
		//odoslanie pokladnikov na obed
        private void SendFreeWorkersToLunch()
        {
			//aspon 5 pokladnikov bude na prevadzke
			if(MyAgent.Clerks.Count - MyAgent.FreeClerks.Count > 5){
                while (MyAgent.FreeClerks.Count > 0)
                {
                    MyMessage msg = new MyMessage(MySim);
                    MyAgent.ClerksSendToLunch++;
                    msg.Clerk = MyAgent.FreeClerks.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakClerk);
                    StartContinualAssistant(msg);
                }
            } else if (MyAgent.Clerks.Count <= 5)
			{
                while (MyAgent.FreeClerks.Count / 2 > 0)
                {
                    MyMessage msg = new MyMessage(MySim);
                    MyAgent.ClerksSendToLunch++;
                    msg.Clerk = MyAgent.FreeClerks.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakClerk);
                    StartContinualAssistant(msg);
                }
            }
            else
			{
                while (MyAgent.FreeClerks.Count > 5 - (MyAgent.Clerks.Count - MyAgent.FreeClerks.Count) && MyAgent.FreeClerks.Count > 0)
                {
                    MyMessage msg = new MyMessage(MySim);
                    MyAgent.ClerksSendToLunch++;
                    msg.Clerk = MyAgent.FreeClerks.Dequeue();
                    msg.Addressee = MyAgent.FindAssistant(SimId.ProcessLunchBreakClerk);
                    StartContinualAssistant(msg);
                }
            }
        }
		//priradenie roboty pokladnikovy
		private void AssignWorkToClerk(MyMessage msg)
		{
            if (MyAgent.CustomersInPaymentQueue.Count > 0)
            {
                MyMessage nextCustomer = MyAgent.CustomersInPaymentQueue.Dequeue();
                nextCustomer.Clerk = msg.Clerk;
                nextCustomer.Addressee = MyAgent.FindAssistant(SimId.ProcessPayment);
                StartContinualAssistant(nextCustomer);
            }
            else if (MyAgent.CustomersInCheckInQueue.Count > 0)
            {
                MyAgent.FreeClerks.Enqueue(msg.Clerk);
                msg.Clerk.Job = "Idle";
                msg.Clerk.CustomerId = 0;
                msg.Customer = null;
                msg.TypeOfCar = MyAgent.CustomersInCheckInQueue.Peek().Customer.TypeOfCar;
                msg.Code = Mc.GetNumberOfFreeMechanicsReception;
                msg.Addressee = MySim.FindAgent(SimId.AgentSTK);
                Request(msg);
            }
            else
            {
                msg.Clerk.Job = "Idle";
                msg.Clerk.CustomerId = 0;
                MyAgent.FreeClerks.Enqueue(msg.Clerk);
            }
        }
	}
}