using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zad2
{
    class Ferry
    {
    #region Variables&Properties
        public const int MaxWaitTime = 2;
        public const int FerryCapacity = 5;
        public const int CarsCount = 11;
        
        public Stack<Car> Cars;
        public Thread FerryThread;
        private Stopwatch _timer;
        private int _courses = 0;
        
        public CoastEnum Coast
        {
            get;
            set;
        }
    #endregion

    #region Methods
        public Ferry()
        {
            FerryThread = new Thread(new ThreadStart(MoveFerry));
            FerryThread.Priority = ThreadPriority.AboveNormal;
            Coast = CoastEnum.WestCoast;
            Cars = new Stack<Car>();
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void MoveFerry()
        {
            Monitor.Enter(this);
            try
            {
                switch(Coast)
                {
                    case CoastEnum.WestCoast:
                        if(Cars.Count == FerryCapacity)
                        {
                            MoveEastCoast();
                        }
                        else
                        {   
                            if (Cars.Count >= 1 && _timer.Elapsed.Seconds > MaxWaitTime)
                            {
                                MoveEastCoast();
                            }
                            else
                            {
                                FerryWait();
                            }
                        }
                        break;
                    case CoastEnum.EastCoast:
                        if(Cars.Count == 0)
                        {
                            MoveWestCoast();
                        }
                        else
                        {
                            FerryWait();
                        }
                        break;
                }
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        private void MoveWestCoast()
        {
            
            Console.WriteLine("Ferry Move to West Coast");
            Thread.Sleep(1000);
            _timer.Restart();
            Coast = CoastEnum.WestCoast;
            ++_courses;
            
            Monitor.PulseAll(this);
            //if (_courses == (CarsCount % FerryCapacity + 1))
            //{
            //    FerryThread.Abort();
            //}
            Monitor.Wait(this);
            MoveFerry();
        }

        private void MoveEastCoast()
        {
            Console.WriteLine("Ferry Move to East Coast");
            Thread.Sleep(1000);

            Coast = CoastEnum.EastCoast;
            
            Monitor.PulseAll(this);
            Monitor.Wait(this);
            MoveFerry();
        }

        private void FerryWait()
        {
            Console.WriteLine("FerryWait");
            if (Cars.Count < 1)
            {
                Console.WriteLine("Reset wait time");
                _timer.Restart();
            }
            Monitor.PulseAll(this);
            Monitor.Wait(this);

            MoveFerry();
        }
    #endregion
    }

    public enum CoastEnum
    {
        WestCoast = 0,
        EastCoast
    }
}
