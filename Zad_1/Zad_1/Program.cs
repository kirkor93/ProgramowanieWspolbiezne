using System;
using System.Runtime.InteropServices;

namespace Zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] loadedMatrix = FileLoader.LoadMatrixFromFile();
            string result = CrammerMethod.Start(loadedMatrix);
            Console.WriteLine(result);

            Console.Write("Press enter to continue: ");
            Console.ReadLine();
        }
    }
}