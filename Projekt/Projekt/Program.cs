using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {
        public int peopleCount = 3;

        static void Main(string[] args)
        {
            HairSaloon mainSallon = new HairSaloon();

            List<Human> people = new List<Human>();

            for(int i = 0; i < 5; ++i)
            {
                people.Add(new Human(mainSallon, i + 1));
                people[i].HumanThread.Start();
            }

            for (int i = 0; i < people.Count; ++i)
            {
                people[i].HumanThread.Join();
            }

            Console.WriteLine("Ended");

            Console.ReadLine();
        }
    }
}
