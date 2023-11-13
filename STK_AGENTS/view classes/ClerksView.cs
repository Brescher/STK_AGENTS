using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.view_classes
{
    internal class ClerksView
    {
        public int Id, CustomerID;
        public string Job;

        public ClerksView(int id_, string job_, int customerID_)
        {
            Id = id_;
            Job = job_;
            CustomerID = customerID_;
            
        }
    }
}
