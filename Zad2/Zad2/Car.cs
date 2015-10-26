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
            CarThread.Priority = ThreadPriority.BelowNormal;
            CarState = CarEnum.WaitForFerry;
        }

        public void Drive()
        {
            Monitor.Enter(Ferry);
            try
            {
                switch (CarState)
                {
                    case CarEnum.OnFerry:
                        if (Ferry.Coast == CoastEnum.EastCoast && Ferry.Cars.Count > 0 && Index == Ferry.Cars.Peek().Index)
                        {
                            MoveFromFerry();
                        }
                        else
                        {
                            WaitOnFerry();
                        }
                        break;
                    case CarEnum.WaitForFerry:
                        if (Ferry.Coast == CoastEnum.WestCoast && Ferry.Cars.Count < Ferry.FerryCapacity)
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
            finally
            {
                Monitor.Exit(Ferry);
            }
        }

        private void MoveFromFerry()
        {
            Console.WriteLine(string.Format("Car {0} get out of the Ferry", Index));
            Thread.Sleep(500);
            
            CarState = CarEnum.WaitForFerry;
            Ferry.Cars.Pop();
            
            Monitor.PulseAll(Ferry);
        }

        private void GetOnFerry()
        {
            Console.WriteLine(string.Format("Car {0} get on the Ferry", Index));
            Thread.Sleep(500);
            
            CarState = CarEnum.OnFerry;
            Ferry.Cars.Push(this);
            
            Monitor.PulseAll(Ferry);
            Monitor.Wait(Ferry);

            Drive();
        }

        private void WaitForFerry()
        {
            Console.WriteLine(string.Format("Car {0} wait for the Ferry", Index));            
            Thread.Sleep(500);
            
            Monitor.PulseAll(Ferry);
            Monitor.Wait(Ferry);

            Drive();
        }

        private void WaitOnFerry()
        {
            Console.WriteLine(string.Format("Car {0} wait on the Ferry", Index));
            Thread.Sleep(500);
            Monitor.PulseAll(Ferry);
            Monitor.Wait(Ferry);
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
