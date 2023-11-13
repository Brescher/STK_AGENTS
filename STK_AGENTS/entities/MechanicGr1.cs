using OSPABA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.entities
{
    public class MechanicGr1 : Mechanic
    {
        public MechanicGr1(Simulation mySim) : base(mySim)
        {
            salary = 1500;
        }
    }
}
