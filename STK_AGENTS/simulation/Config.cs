using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STK_AGENTS.simulation
{
    public class Config
    {
        public const double MaxTime = 8d * 60d;
        public const double Cooling = MaxTime - 75d;
        public const double PCustomerArrival = 60.0d / 23.0d;
        public const double PCustomerArrivalBoosted = 60.0d / (23.0d * 1.24d);

        public const int Car = 1;
        public const int Van = 2;
        public const int Truck = 3;

        public const int AssignedQueue = 0;
        public const int AssignedMechanicGr1 = 1;
        public const int AssignedMechanicGr2 = 2;
        public const int AssignedParkingSpace = 3;


        public static readonly double[] PCarType = { 0.65d, 0.21d, 0.14d };
        public static readonly double[] CheckInTime = { 180.0d / 60.0d, 431.0d / 60.0d, 695.0d / 60.0d };
        public static readonly double[] PaymentTime = { 65d/60d, 177d/60d };
        


        public static readonly double[] Personal = { 31d, 45d };

        public static readonly double[] Van1 = { 35d, 37d, 0.2d };
        public static readonly double[] Van2 = { 38d, 40d, 0.35d };
        public static readonly double[] Van3 = { 41d, 47d, 0.3d };
        public static readonly double[] Van4 = { 48d, 52d, 0.15d };

        public static readonly double[] Truck1 = { 37d, 42d, 0.05d };
        public static readonly double[] Truck2 = { 43d, 45d, 0.1d };
        public static readonly double[] Truck3 = { 46d, 47d, 0.15d };
        public static readonly double[] Truck4 = { 48d, 51d, 0.4d };
        public static readonly double[] Truck5 = { 52d, 55d, 0.25d };
        public static readonly double[] Truck6 = { 56d, 65d, 0.05d };

        private Config()
        { }

    }
}
