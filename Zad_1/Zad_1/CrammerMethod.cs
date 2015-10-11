using System;
using System.Threading;

namespace Zad_1
{
    public static class CrammerMethod
    {
        private static Semaphore _semaphore = new Semaphore(1, 1);
        private static Thread[] _threads;

        public static string Start(Matrix matrix)
        {
            if (_threads != null)
            {
                throw new InvalidOperationException("Already running");
            }

            //_threads = new Thread[];

            Matrix left = matrix.RemoveColumn(matrix.Columns - 1);
            Matrix right = matrix.GetColumn(matrix.Columns - 1);

            double mainDet = left.Determinant;
            double[] otherDets = new double[left.Columns];

            for (int i = 0; i < left.Columns; i++)
            {
                Matrix tmp = new Matrix(left);
                for(int j = 0 ; j < tmp.Rows ; j ++)
                {
                    tmp.SetValue(j, i, right.GetValue(j, 0));
                }

                otherDets[i] = tmp.Determinant;
                Console.WriteLine(i + ": " + otherDets[i] / mainDet);
            }

            return null;
        }
    }


}
