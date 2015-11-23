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
        public const int FerryCapacity = 2;
        public const int CarsCount = 5;
        
        public Stack<Car> Cars;
        public Thread FerryThread;
        private Actions _ferryAction;
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
            Coast = CoastEnum.WestCoast;
            _ferryAction = Actions.FerryWait;
            Cars = new Stack<Car>();
            _timer = new Stopwatch();
            _timer.Start();
        }

        public bool CanCarTakeAction(Car car)
        {
            lock(this)
            {
                if (_ferryAction == Actions.FerryWait)
                    if (Coast == CoastEnum.EastCoast)
                    {
                        if (Cars.Count > 0 && car == Cars.Peek())
                        {
                            GetCarOut(car);
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        if (Cars.Count < FerryCapacity) 
                        {
                            GetCarIn(car);
                            return true;
                        }
                        else
                            return false;
                    }
                else
                    return false;
            }
        }

        private void GetCarIn(Car car)
        {
            lock(this)
            {
                _ferryAction = Actions.CarMove;
                Thread.Sleep(500);
                Cars.Push(car);
                _ferryAction = Actions.FerryWait;
            }
        }

        private void GetCarOut(Car car)
        {
            lock (this)
            {
                _ferryAction = Actions.CarMove;
                Thread.Sleep(500);
                Cars.Pop();
                _ferryAction = Actions.FerryWait;
            }
        }

        private void MoveFerry()
        {
            {
                switch (Coast)
                {
                    case CoastEnum.WestCoast:
                        if (Cars.Count >= FerryCapacity)
                        {
                            Console.WriteLine("Cap is reached");
                            MoveEastCoast();
                        }
                        else
                        {
                            if (Cars.Count >= 1 && _timer.Elapsed.Seconds > MaxWaitTime)
                            {
                                Console.WriteLine("Time is finished and there is more than 1 car");
                                MoveEastCoast();
                            }
                            else
                            {
                                FerryWait();
                            }
                        }
                        break;
                    case CoastEnum.EastCoast:
                        if (Cars.Count == 0)
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
        }

        private void MoveWestCoast()
        {
            lock(this)
            {
                _ferryAction = Actions.FerryMove;
            }
                Console.WriteLine(" Ferry Move to West Coast");
                Thread.Sleep(1000);
            lock(this)
            {
                _courses = 0;
                _timer.Restart();
                Coast = CoastEnum.WestCoast;

                _ferryAction = Actions.FerryWait;
            }
                MoveFerry();
        }

        private void MoveEastCoast()
        {
            lock(this)
            {
                _ferryAction = Actions.FerryMove;
            }
                Console.WriteLine(" Ferry Move to East Coast");
                Thread.Sleep(1000);
            lock(this)
            {
                Coast = CoastEnum.EastCoast;
                _ferryAction = Actions.FerryWait;
            }
                MoveFerry();
        }

        private void FerryWait()
        {
            lock (this)
            {
                _ferryAction = Actions.FerryWait;
            }
            Console.WriteLine(" FerryWait");
            if (Cars.Count < 1 && _timer.Elapsed.Seconds >= MaxWaitTime && Coast == CoastEnum.WestCoast)
            {
                if (_courses > 2) FerryThread.Abort();
                ++_courses;
                Console.WriteLine(" Reset wait time");
                _timer.Restart();
            }
            Thread.Sleep(2000);
            MoveFerry();
        }
    #endregion
    }

    public enum CoastEnum
    {
        WestCoast = 0,
        EastCoast
    }

    public enum Actions
    {
        FerryWait,
        FerryMove,
        CarMove
    }
}
