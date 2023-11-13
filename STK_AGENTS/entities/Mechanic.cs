using OSPABA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.entities
{

    public abstract class Mechanic : Entity
    {
        string job, typeOfCar;
        int customerId;
        protected int salary;
        bool hadLunch;

        protected Mechanic(Simulation mySim) : base(mySim)
        {
            job = "idle";
            typeOfCar = "-";
            customerId = 0;
            hadLunch = false;
        }

        public string Job { get => job; set => job = value; }
        public string TypeOfCar { get => typeOfCar; set => typeOfCar = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public int Salary { get => salary;}
        public bool HadLunch { get => hadLunch; set => hadLunch = value; }
    }
}
