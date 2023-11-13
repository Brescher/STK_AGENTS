using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.view_classes
{
    internal class MechanicGr2View
    {
        public int Id, CustomerID;
        public string Job;

        public MechanicGr2View(int id_, string job_, int customerID_)
        {
            Id = id_;
            Job = job_;
            CustomerID = customerID_;

        }
    }
}
