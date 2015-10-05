using System;
using System.Collections;
using System.Collections.Generic;

namespace Zad_1
{
    public class WrongMatrixSizeException : Exception
    {
        public WrongMatrixSizeException() : base("Sent matrix is not square") { }
        public WrongMatrixSizeException(string message) : base(message) { }
    }

    public static class CrammerMethod
    {
        public static string Start(double[][] matrix)
        {
            return "DUPA";
        }

        public static double CountDeterminant(double[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i].Length != matrix.Length)
                {
                    throw new WrongMatrixSizeException();
                }
            }

            Stack<double[][]> matrices = new Stack<double[][]>();
            matrices.Push(matrix);

            while (matrices.Count > 0)
            {
                
            }

            return 0.0f;

        }
    }


}
