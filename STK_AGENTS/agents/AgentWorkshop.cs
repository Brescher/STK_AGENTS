using OSPABA;
using simulation;
using managers;
using continualAssistants;
using STK_AGENTS.entities;
using OSPDataStruct;
using OSPStat;

namespace agents
{
    //meta! id="5"
	public class AgentWorkshop : Agent
	{
        public List<MechanicGr1> MechanicsGr1;
        public List<MechanicGr2> MechanicsGr2;

        public SimQueue<MechanicGr1> FreeMechanicsGr1;
        public SimQueue<MechanicGr2> FreeMechanicsGr2;
        public SimQueue<Customer> CustomersInInespection;
        public SimQueue<MyMessage> CustomersInParkingLot;

        public int NumberOfFreeMechanicsGr1, NumberOfFreeMechanicsGr2, NumberOfFreeSpaceParking, customersChecked, MechanicsGr1LunchDone, MechanicsGr2LunchDone;
        public Stat TimeInInspection, TimeInParkingLot;

        public bool SendFreeWorkersToLunch;

        public AgentWorkshop(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            FreeMechanicsGr1 = new SimQueue<MechanicGr1>(new WStat(MySim));
            FreeMechanicsGr2 = new SimQueue<MechanicGr2>(new WStat(MySim));
            CustomersInParkingLot = new SimQueue<MyMessage>(new WStat(MySim));
            CustomersInInespection = new SimQueue<Customer>(new WStat(MySim));
            TimeInInspection = new Stat();
            TimeInParkingLot = new Stat();

            MySimulation sim = (MySimulation)MySim;
            MechanicsGr1 = new List<MechanicGr1>(sim.NumberOfMechanicsGr1);
            MechanicsGr2 = new List<MechanicGr2>(sim.NumberOfMechanicsGr2);

            MySimulation tempSim = (MySimulation)MySim;

            for (int i = 0; i < tempSim.NumberOfMechanicsGr1; i++)
            {
                MechanicsGr1.Add(new MechanicGr1(MySim));
                FreeMechanicsGr1.AddLast(MechanicsGr1[i]);
            }

            for (int i = 0; i < tempSim.NumberOfMechanicsGr2; i++)
            {
                MechanicsGr2.Add(new MechanicGr2(MySim));
                FreeMechanicsGr2.AddLast(MechanicsGr2[i]);
            }

            NumberOfFreeMechanicsGr1 = tempSim.NumberOfMechanicsGr1;
            NumberOfFreeMechanicsGr2 = tempSim.NumberOfMechanicsGr2;
            NumberOfFreeSpaceParking = 5;
            customersChecked = 0;
            MechanicsGr1LunchDone = 0;
            MechanicsGr2LunchDone = 0;

            SendFreeWorkersToLunch = false;

        }

            //meta! userInfo="Generated code: do not modify", tag="begin"
            private void Init()
		{
			new ManagerWorkshop(SimId.ManagerWorkshop, MySim, this);
			new ProcessInspection(SimId.ProcessInspection, MySim, this);
			new ProcessLunchBreakMechanic(SimId.ProcessLunchBreakMechanic, MySim, this);
			AddOwnMessage(Mc.CustomerServiceInspection);
			AddOwnMessage(Mc.GetNumberOfFreeMechanicsWorkshop);
		}
		//meta! tag="end"
	}
}