using System;
using System.Threading;

namespace Zad_1
{
    public static class CrammerMethod
    {
        private static Semaphore _semaphore = new Semaphore(1, 1);
        private static Thread[] _threads;
        private static Matrix _startMatrix;

        public static string Start(Matrix matrix)
        {
            if (_threads != null)
            {
                throw new InvalidOperationException("Already running");
            }

            _startMatrix = matrix;
            _threads = new Thread[matrix.Columns - 1];

            for (int i = 0; i < matrix.Columns - 1; i++)
            {
                _threads[i] = new Thread(new ParameterizedThreadStart(CountOne));
                _threads[i].Start(i);
            }

            _threads = null;
            return null;
        }

        private static void CountOne(object columnNumber)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread} started counting column {columnNumber}");
            //Thread.Sleep(10000);

            _semaphore.WaitOne();
            Matrix tmp = new Matrix(_startMatrix);
            _semaphore.Release();

            Matrix left = tmp.RemoveColumn(tmp.Columns - 1);
            Matrix right = tmp.GetColumn(tmp.Columns - 1);

            for (int j = 0; j < tmp.Rows; j++)
            {
                left.SetValue(j, (int)columnNumber, right.GetValue(j, 0));
            }

            Console.WriteLine(left.Determinant);
            //Console.WriteLine(i + ": " + otherDets[i] / mainDet);
        }
    }


}
