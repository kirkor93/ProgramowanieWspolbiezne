using System;
using System.Globalization;

namespace Zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix loadedMatrix = FileLoader.LoadMatrixFromFile();
            Console.WriteLine("Loaded matrix: ");
            loadedMatrix.Print();
            Console.WriteLine();
            Console.WriteLine(CrammerMethod.Start(loadedMatrix));

            Console.Write("Press enter to continue: ");
            Console.ReadLine();
        }
    }
}