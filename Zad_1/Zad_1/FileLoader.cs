using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Zad_1
{
    public class FileLoader
    {
        private const string DefaultPath = @"Matrix.txt";

        public static double[][] LoadMatrixFromFile(string path = DefaultPath)
        {
            List<double[]> matrixTmp = new List<double[]>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Replace('.', ',');
                    List<double> row = new List<double>();
                    bool readLine = false;

                    for (int i = 0; i <= 9; i++)
                    {
                        if(line.Contains(i.ToString("G", CultureInfo.InvariantCulture)))
                        {
                            readLine = true;
                            break;
                        }
                    }

                    while (readLine)
                    {
                        int index = line.IndexOf(' ');
                        double number;
                        if (index > 0)
                        {
                            number = Convert.ToDouble(line.Substring(0, index + 1));
                            line = line.Substring(index + 1);
                        }
                        else
                        {
                            number = Convert.ToDouble(line);
                            readLine = false;
                        }
                        row.Add(number);
                    }
                    matrixTmp.Add(row.ToArray());
                }
            }

            double[][] matrix = new double[matrixTmp.Count][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = matrixTmp[i].ToArray();
            }

            int expectedRowLength = matrixTmp.Count + 1;
            foreach (double[] doubles in matrix)
            {
                if (doubles.Length != expectedRowLength)
                {
                    throw new WrongMatrixSizeException("Matrix for this exercise must have (i, i+1) size");
                }
            }

//            foreach (double[] doubles in matrix)
//            {
//                foreach (double d in doubles)
//                {
//                    Console.Write(d + " ");
//                }
//                Console.WriteLine();
//            }

            return matrix;
        }
    }
}
