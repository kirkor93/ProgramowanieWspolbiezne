using System;
using System.Globalization;

namespace Zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix loadedMatrix = FileLoader.LoadMatrixFromFile();
            //loadedMatrix.Print();
            //string test = loadedMatrix.Determinant.ToString(CultureInfo.InvariantCulture);
            //Console.WriteLine("Calculated det: {0}, rows: {1}, columns: {2}", test, loadedMatrix.Rows, loadedMatrix.Columns);
            //loadedMatrix.RemoveColumn(3).Print();

            CrammerMethod.Start(loadedMatrix);

            Console.Write("Press enter to continue: ");
            Console.ReadLine();
        }
    }
}