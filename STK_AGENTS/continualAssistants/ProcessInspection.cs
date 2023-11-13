using OSPABA;
using simulation;
using agents;
using OSPRNG;
using STK_AGENTS.simulation;

namespace continualAssistants
{
	//meta! id="33"
	public class ProcessInspection : Process
	{
		UniformDiscreteRNG personalCarGen;
		EmpiricRNG<int> vanGen, truckGen;

		public ProcessInspection(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
            MySimulation sim = (MySimulation)MySim;

            MyAgent.AddOwnMessage(Mc.InspectionDone);

            personalCarGen = new UniformDiscreteRNG((int)Config.Personal[0], (int)Config.Personal[1], sim.SeedGenerator);
            vanGen = new EmpiricRNG<int>(sim.SeedGenerator,
                                         new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Van1[0], (int)Config.Van1[1]), Config.Van1[2]),
                                         new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Van2[0], (int)Config.Van2[1]), Config.Van2[2]),
                                         new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Van3[0], (int)Config.Van3[1]), Config.Van3[2]),
                                         new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Van4[0], (int)Config.Van4[1]), Config.Van4[2]));

            truckGen = new EmpiricRNG<int>(sim.SeedGenerator,
                                           new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Truck1[0], (int)Config.Truck1[1]), Config.Truck1[2]),
                                           new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Truck2[0], (int)Config.Truck2[1]), Config.Truck2[2]),
                                           new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Truck3[0], (int)Config.Truck3[1]), Config.Truck3[2]),
                                           new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Truck4[0], (int)Config.Truck4[1]), Config.Truck4[2]),
                                           new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Truck5[0], (int)Config.Truck5[1]), Config.Truck5[2]),
                                           new EmpiricPair<int>(new UniformDiscreteRNG((int)Config.Truck6[0], (int)Config.Truck6[1]), Config.Truck6[2]));
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentWorkshop", id="34", type="Start"
		public void ProcessStart(MessageForm message)
		{
            MyMessage msg = (MyMessage)message;
			MyAgent.CustomersInInespection.Enqueue(msg.Customer);
            msg.Customer.Position = "In inspection";
			MyAgent.TimeInParkingLot.AddSample(msg.Customer.GetTimeInParking());
            msg.Mechanic.Job = "Inspecting";
            msg.Mechanic.CustomerId = msg.Customer.Id;
            message.Code = Mc.InspectionDone;

			int genValue;

            switch (msg.Customer.TypeOfCar)
			{
				case Config.Car:
					genValue = personalCarGen.Sample();
					MyAgent.TimeInInspection.AddSample(genValue);
                    Hold(genValue, message);
                break;
				case Config.Van:
					genValue = vanGen.Sample();
                    MyAgent.TimeInInspection.AddSample(genValue);
                    Hold(genValue, message);
                break;
				case Config.Truck:
					genValue = truckGen.Sample();
                    MyAgent.TimeInInspection.AddSample(genValue);
                    Hold(genValue, message);
                break;
			}
            
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.InspectionDone:
					MyMessage msg = (MyMessage)message;
					MyAgent.CustomersInInespection.Remove(msg.Customer);
					MyAgent.customersChecked++;
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
		public new AgentWorkshop MyAgent
		{
			get
			{
				return (AgentWorkshop)base.MyAgent;
			}
		}
    }
}