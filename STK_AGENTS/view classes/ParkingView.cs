using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.view_classes
{
    internal class ParkingView
    {
        public int Id;
        public double startWait;

        public ParkingView(int id_, double startWait_)
        {
            Id = id_;
            startWait = startWait_;
        }
    }
}
