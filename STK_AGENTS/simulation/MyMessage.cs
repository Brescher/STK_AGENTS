using OSPABA;
using STK_AGENTS.entities;

namespace simulation
{
	public class MyMessage : MessageForm
	{
        //assigned resource: 0 - nothing, go to queue; 1 - mechanic gr 1; 2 - mechanic gr 2; 3 - parking spot
        int assignedResource, typeOfCar;
        Clerk clerk;
        Mechanic mechanic;
        Customer customer;
        public MyMessage(Simulation sim) :
			base(sim)
		{
            Clerk = null;
            mechanic = null;
            customer = null;
            AssignedResource = 0;
        }

        public Mechanic Mechanic { get => mechanic; set => mechanic = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public Clerk Clerk { get => clerk; set => clerk = value; }
        public int AssignedResource { get => assignedResource; set => assignedResource = value; }
        public int TypeOfCar { get => typeOfCar; set => typeOfCar = value; }

        public MyMessage(MyMessage original) :
			base(original)
		{
			// copy() is called in superclass
		}

		override public MessageForm CreateCopy()
		{
			return new MyMessage(this);
		}

		override protected void Copy(MessageForm message)
		{
			base.Copy(message);
			MyMessage original = (MyMessage)message;
            // Copy attributes
            Clerk = original.Clerk;
            Mechanic = original.Mechanic;
            Customer = original.Customer;
            AssignedResource = original.AssignedResource;
        }
	}
}