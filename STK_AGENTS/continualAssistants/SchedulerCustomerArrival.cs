using OSPABA;
using simulation;
using agents;
using OSPRNG;
using STK_AGENTS.entities;
using STK_AGENTS.simulation;

namespace continualAssistants
{
	//meta! id="22"
	public class SchedulerCustomerArrival : Scheduler
	{
		ExponentialRNG customerArrivalGen;
		ExponentialRNG customerArrivalGenBoosted;
		UniformContinuousRNG typeOfCarGen;
		bool genActive;
		public SchedulerCustomerArrival(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
            MySimulation sim = (MySimulation)MySim;
            customerArrivalGen = new ExponentialRNG(Config.PCustomerArrival, sim.SeedGenerator);
            customerArrivalGenBoosted = new ExponentialRNG(Config.PCustomerArrivalBoosted, sim.SeedGenerator);

            
            typeOfCarGen = new UniformContinuousRNG(0d, 1d, sim.SeedGenerator);

        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
			genActive = true;
		}

		//meta! sender="AgentSurroundings", id="23", type="Start"
		public void ProcessStart(MessageForm message)
		{
            MyMessage copy = (MyMessage)message.CreateCopy();

            copy.Code = Mc.NewCustomer;

            Hold(0, copy);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.NewCustomer:
					if(genActive)
					{
						if (((MySimulation)MySim).StandardCustomerFlow)
						{
                            Hold(customerArrivalGen.Sample(), message.CreateCopy());
                        } else
						{
                            Hold(customerArrivalGenBoosted.Sample(), message.CreateCopy());
                        }
                        

                        MyMessage msg = (MyMessage)message;
                        msg.Customer = new Customer(MySim);
						msg.Customer.QueueEntranceStartWaitTime = MySim.CurrentTime;
						msg.Customer.ArrivalTime = MySim.CurrentTime;
						double pGen = typeOfCarGen.Sample();
						double p = 0;
						int i;
						for(i = 1; i <= Config.PCarType.Length; i++)
						{
							p += Config.PCarType[i - 1];

                            if (pGen < p)
							{
								break;
							}
						}
						msg.Customer.TypeOfCar = i;
                        msg.Addressee = MyAgent;

                        Notice(msg);
                    }
				break;
			}
		}

        public void ProcessBreak(MessageForm message)
        {
            genActive = false;
            AssistantFinished(message);
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.Start:
					ProcessStart(message);
				break;

				case Mc.BreakCA:
					ProcessBreak(message);
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