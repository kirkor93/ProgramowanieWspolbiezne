using System;
using System.Linq;
using System.Threading;

namespace Zad_1
{
    public static class CrammerMethod
    {
        private static readonly Semaphore Semaphore = new Semaphore(1, 1);
        private static Thread[] _threads;
        private static Matrix _startMatrix;
        private static double[] _results;

        public static string Start(Matrix matrix)
        {
            if (_threads != null)
            {
                throw new InvalidOperationException("Already running");
            }

            _startMatrix = matrix;
            _threads = new Thread[matrix.Columns - 1];
            _results = new double[matrix.Columns];

            //main det
            CountDeterminant(matrix.Columns - 1);

            for (int i = 0; i < matrix.Columns - 1; i++)
            {
                _threads[i] = new Thread(new ParameterizedThreadStart(CountResult));
                _threads[i].Name = $"Thread {i}";
                _threads[i].Start(i);
            }

            foreach (Thread t in _threads)
            {
                t.Join();
            }

            //Console.WriteLine("Main thread restarted");

            string message;
            if (_results.Last() == 0)
            {
                message = 
                    _results.Distinct().Count() > 1 ? 
                    "The system has no solutions." : "The system has infinitely many solutions.\n";
            }
            else
            {
                message = "The system has one solution :\n";
                for (int i = 0; i < _results.Length - 1; i++)
                {
                    message += $"x{i} = {_results[i]}\n";
                }
            }

            _threads = null;
            _results = null;

            return message;
        }

        private static void CountDeterminant(object column)
        {
            //Console.WriteLine($"Thread {Thread.CurrentThread.Name} started counting column {column}");
            //Thread.Sleep(10000);

            Semaphore.WaitOne();
            Matrix tmp = new Matrix(_startMatrix);
            Semaphore.Release();

            Matrix left = tmp.RemoveColumn(tmp.Columns - 1);
            Matrix right = tmp.GetColumn(tmp.Columns - 1);

            int c = (int) column;

            if(c < left.Columns)
            {
                for (int j = 0; j < tmp.Rows; j++)
                {
                    left.SetValue(j, c, right.GetValue(j, 0));
                }
            }

            Semaphore.WaitOne();
            _results[c] = left.Determinant;
            Semaphore.Release();
        }

        private static void CountResult(object column)
        {
            CountDeterminant(column);

            if (_results.Last() != 0)
            {
                Semaphore.WaitOne();
                _results[(int)column] /= _results[_results.Length - 1];
                Semaphore.Release();
            }

            //Console.WriteLine($"Thread {Thread.CurrentThread.Name} finished. Counted result: {_results[(int)column]}");
        }
    }


}
