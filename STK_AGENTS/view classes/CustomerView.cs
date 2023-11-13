using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.view_classes
{
    internal class CustomerView
    {
        public int Id, Position;
        public string CarType;
        public double TInCheckInQ, TInPayQ;

        public CustomerView(int id_, int position_, string carType_, double tInCheckInQ_, double tInPayQ_)
        {
            Id = id_;
            Position = position_;
            CarType = carType_;
            TInPayQ = tInPayQ_;
            TInCheckInQ = tInCheckInQ_;
        }

    }
}
