using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zad2
{
    class Car
    {
#region Variables&Properties
        public Thread CarThread;

        public int Index
        {
            get;
            set;
        }

        public Ferry Ferry
        {
            get;
            set;
        }

        public CarEnum CarState
        {
            get;
            set;
        }

#endregion

#region Methods
        public Car()
        {
            CarThread = new Thread(new ThreadStart(Drive));
            CarState = CarEnum.WaitForFerry;
        }

        private void Drive()
        {
            switch (CarState)
            {
                case CarEnum.OnFerry:
                    if (Ferry.Coast == CoastEnum.EastCoast)
                    {
                        MoveFromFerry();
                    }
                    else
                    {
                        WaitOnFerry();
                    }
                    break;
                case CarEnum.WaitForFerry:
                    if (Ferry.Coast == CoastEnum.WestCoast)
                    {
                        GetOnFerry();
                    }
                    else
                    {
                        WaitForFerry();
                    }
                    break;
            }
        }

        private void WaitForAccess()
        {
            Console.WriteLine(string.Format("Car {0} wait for access", Index));
            Thread.Sleep(1500);
            Drive();
        }

        private void MoveFromFerry()
        {
            if(Ferry.CanCarTakeAction(this))
            {
                Console.WriteLine(string.Format("Car {0} get out of the Ferry", Index));
                //Ferry.GetCarOut(this);
            }
            else
            {
                WaitForAccess();
            }
        }

        private void GetOnFerry()
        {
            if(Ferry.CanCarTakeAction(this))
            {
                Console.WriteLine(string.Format("Car {0} get on the Ferry", Index));
                //Ferry.GetCarIn(this);
                CarState = CarEnum.OnFerry;
            }
            WaitForAccess();
        }

        private void WaitForFerry()
        {
            Console.WriteLine(string.Format("Car {0} wait for the Ferry", Index));            
            Thread.Sleep(500);
            Drive();
        }

        private void WaitOnFerry()
        {
            Console.WriteLine(string.Format("Car {0} wait on the Ferry", Index));
            Thread.Sleep(500);
            Drive();
        }
#endregion
    }

    public enum CarEnum
    {
        OnFerry = 0,
        WaitForFerry
    }
}
