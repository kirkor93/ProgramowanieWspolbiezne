using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {
        public static int peopleCount = 8;

        static void Main(string[] args)
        {
            HairSaloon mainSallon = new HairSaloon();

            List<Human> people = new List<Human>();

            for (int i = 0; i < Program.peopleCount; ++i)
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
