using OSPABA;
using simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.entities
{
    public class Customer : Entity
    {
        double queueEntranceStartWaitTime, queueEntranceWaitTime, queueDepartureStartWaitTime, queueDepartureWaitTime, arrivalTime, timeInSystem,
            parkingStartWaitTime, parkingEndWaitTime;
        bool inQueueForInspection;
        Clerk servingClerk;
        Mechanic servingMechanic;
        String position;
        //1 - personal, 2 - van, 3 - truck
        int typeOfCar, assignedResource;
        public Customer(Simulation mySim) : base(mySim)
        {
            queueEntranceWaitTime = 0;
            queueDepartureWaitTime = 0;
        }

        public double QueueEntranceStartWaitTime { get => queueEntranceStartWaitTime; set => queueEntranceStartWaitTime = value; }
        public double QueueEntranceWaitTime { get => queueEntranceWaitTime; set => queueEntranceWaitTime = value; }
        public double QueueDepartureStartWaitTime { get => queueDepartureStartWaitTime; set => queueDepartureStartWaitTime = value; }
        public double QueueDepartureWaitTime { get => queueDepartureWaitTime; set => queueDepartureWaitTime = value; }
        public double ArrivalTime { get => arrivalTime; set => arrivalTime = value; }
        public double TimeInSystem { get => timeInSystem; set => timeInSystem = value; }
        public bool InQueueForInspection { get => inQueueForInspection; set => inQueueForInspection = value; }
        public string Position { get => position; set => position = value; }
        public int TypeOfCar { get => typeOfCar; set => typeOfCar = value; }
        public int AssignedResource { get => assignedResource; set => assignedResource = value; }
        internal Clerk ServingClerk { get => servingClerk; set => servingClerk = value; }
        internal Mechanic ServingMechanic { get => servingMechanic; set => servingMechanic = value; }
        public double ParkingStartWaitTime { get => parkingStartWaitTime; set => parkingStartWaitTime = value; }
        public double ParkingEndWaitTime { get => parkingEndWaitTime; set => parkingEndWaitTime = value; }

        public double GetTimeInSystem()
        {
            return TimeInSystem = MySim.CurrentTime - ArrivalTime;
        }

        public double GetTimeInQueueCheckIn()
        {
            return QueueEntranceWaitTime = MySim.CurrentTime - QueueEntranceStartWaitTime;
        }

        public double GetTimeInQueueCPayment()
        {
            return QueueDepartureWaitTime = MySim.CurrentTime - QueueDepartureStartWaitTime;
        }

        public double GetTimeInParking()
        {
            return ParkingEndWaitTime = MySim.CurrentTime - ParkingStartWaitTime;
        }
    }
}
