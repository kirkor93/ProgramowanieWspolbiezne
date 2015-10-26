using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            Ferry mainFerry = new Ferry();

            mainFerry.FerryThread.Start();

            Car[] cars = new Car[Ferry.CarsCount];
            
            for (int i = 0; i < Ferry.CarsCount; ++i)
            {
                cars[i] = new Car();
                cars[i].Index = i+1;
                cars[i].Ferry = mainFerry;
                cars[i].CarThread.Start();
            }
            
            mainFerry.FerryThread.Join();
            for (int i = 0; i < Ferry.CarsCount; ++i)
            {
                cars[i].CarThread.Join();
            }

            Console.WriteLine("Ended");

            Console.ReadLine();
        }
    }
}
