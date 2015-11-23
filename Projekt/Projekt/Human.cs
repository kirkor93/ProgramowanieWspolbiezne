using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projekt
{
    class Human
    {
        public Thread HumanThread;
        public HumanState State;
        public HairSaloon Saloon;

        private int _index;
        
        public Human(HairSaloon saloon, int index)
        {
            HumanThread = new Thread(new ThreadStart(Run));
            State = HumanState.Outside;
            Saloon = saloon;
            _index = index;
        }

        public void Run()
        {
            switch(State)
            {
                case HumanState.Outside:
                    GoToSaloon();
                    break;
                case HumanState.InsideBefore:
                    GoOnChair();
                    break;
                case HumanState.OnChair:
                    //GoToWaitRoom();
                    GoOutside();
                    break;
                //case HumanState.InsideAfter:
                //    GoOutside();
                //    break;
            }
        }
        
        private void GoToSaloon()
        {
            if (Monitor.TryEnter(Saloon.EnterLock))
            {
                try
                {
                    Thread.Sleep(500);
                    Console.WriteLine(string.Format("Human {0} Entered Saloon", _index));
                    State = HumanState.InsideBefore;
                    Saloon.EnterWaitRoom(this);
                }
                finally
                {
                    Monitor.Exit(Saloon.EnterLock);
                    Run();
                }
            }
            else
            {
                Wait();
            }
        }

        private void GoOnChair()
        {
            if (Monitor.TryEnter(Saloon.ChairLock))
            {
                try
                {
                    Thread.Sleep(500);
                    Console.WriteLine(string.Format("Human {0} Entered Chair", _index));
                    State = HumanState.OnChair;
                    Saloon.EnterChairRoom(this);
                }
                finally
                {
                    Monitor.Exit(Saloon.ChairLock);
                    Run();
                }
            }
            else
            {
                Wait();
            }
        }

        //private void GoToWaitRoom()
        //{
        //    if (Monitor.TryEnter(Saloon.ChairLock))
        //    {
        //        try
        //        {
        //            Thread.Sleep(500);
        //            Console.WriteLine(string.Format("Human {0} Entered Waiting", _index));
        //            State = HumanState.InsideAfter;
        //            Saloon.ExitChairRoom(this);
        //        }
        //        finally
        //        {
        //            Monitor.Exit(Saloon.ChairLock);
        //            Run();
        //        }
        //    }
        //    else
        //    {
        //        Wait();
        //    }
        //}

        private void GoOutside()
        {
            if (Monitor.TryEnter(Saloon.ExitLock))
            {
                try
                {
                    Thread.Sleep(500);
                    Console.WriteLine(string.Format("Human {0} Exited Saloon", _index));
                    State = HumanState.Outside;
                    //Saloon.ExitWaitRoom(this);
                    Saloon.ExitChairRoom(this);
                }
                finally
                {
                    Monitor.Exit(Saloon.ExitLock);
                }
            }
            else
            {
                Wait();
            }
        }

        private void Wait()
        {
            if (State == HumanState.Outside) Console.WriteLine(string.Format("Human {0} WaitOutside", _index));
            if(State == HumanState.InsideBefore) Console.WriteLine(string.Format("Human {0} WaitInside", _index));
            if (State == HumanState.OnChair) Console.WriteLine(string.Format("Human {0} WaitExit", _index));
            Thread.Sleep(1000);
            Run();
        }
    }

    public enum HumanState
    {
        Outside,
        InsideBefore,
        OnChair,
        //InsideAfter
    }
}
