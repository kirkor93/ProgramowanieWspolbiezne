using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix loadedMatrix = FileLoader.LoadMatrixFromFile();
            string test = loadedMatrix.Determinant.ToString(CultureInfo.InvariantCulture);
            Console.WriteLine("Calculated det: {0}", test);
//            string result = CrammerMethod.Start(loadedMatrix);
//            Console.WriteLine(result);

            Console.Write("Press enter to continue: ");
            Console.ReadLine();
        }
    }
}