using OSPABA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.entities
{
    public class Clerk : Entity
    {
        string job;
        int customerId;
        bool hadLunch;
        public Clerk(Simulation mySim) : base(mySim)
        {
            job = "idle";
            customerId = 0;
            HadLunch = false;
        }

        public string Job { get => job; set => job = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public bool HadLunch { get => hadLunch; set => hadLunch = value; }
    }
}
