using System;
using System.Collections.Generic;

namespace Zad_1
{
    public class WrongMatrixSizeException : Exception
    {
        public WrongMatrixSizeException() : base("Matrix is not square") { }
        public WrongMatrixSizeException(string message) : base(message) { }
    }

    public struct Matrix
    {
        private double[][] _values;

        public double Determinant
        {
            get
            {
                for (int i = 0; i < _values.Length; i++)
                {
                    if (_values[i].Length != _values.Length)
                    {
                        throw new WrongMatrixSizeException();
                    }
                }

                double result = 0.0f;
                switch (_values.Length)
                {
                    case 0:
                        throw new WrongMatrixSizeException("Matrix is empty");

                    case 1:
                        result = _values[0][0];
                        break;

                    case 2:
                        result = _values[0][0] * _values[1][1] - _values[0][1] * _values[1][0];
                        break;

                    default:
                        double[][] tmp = new double[_values.Length - 1][];
                        for (int j = 0; j < _values.Length - 1; j++)
                        {
                            tmp[j] = new double[_values.Length - 1];
                        }

                        const int ignoredColumn = 0;
                        for (int ignoredRow = 0; ignoredRow < _values.Length; ignoredRow++)
                        {
                            for (int i = 0, x = 0; i < _values.Length; i++, x++)
                            {
                                if (i == ignoredRow)
                                {
                                    x--;
                                    continue;
                                }

                                for (int j = 0, y = 0; j < _values.Length; j++, y++)
                                {
                                    if (j == ignoredColumn)
                                    {
                                        y--;
                                        continue;
                                    }
                                    tmp[x][y] = _values[i][j];
                                }
                            }

                            result += Math.Pow(-1.0f, ignoredRow + ignoredColumn + 2)
                                        *_values[ignoredRow][ignoredColumn]
                                        *new Matrix(tmp).Determinant;

//                            foreach (double[] doubles in tmp)
//                            {
//                                foreach (double d in doubles)
//                                {
//                                    Console.Write(d + " ");
//                                }
//                                Console.WriteLine();
//                            }
//                            Console.WriteLine();
                        }
                        break;
                }

                return result;
            }
        }

        public Matrix(double[][] values)
        {
            _values = new double[values.Length][];
            for (int i = 0; i < _values.Length; i++)
            {
                _values[i] = new double[values[i].Length];
            }

            for (int i = 0; i < _values.Length; i++)
            {
                for (int j = 0; j < _values[i].Length; j++)
                {
                    _values[i][j] = values[i][j];
                }
            }
        }

        public Matrix RemoveRow(int index)
        {
            Matrix tmp = new Matrix(_values);


            return tmp;
        }

        public double GetValue(int i, int j)
        {
            if (i >= _values.Length
                || j >= _values[i].Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _values[i][j];
        }

        public void SetValue(int i, int j, double value)
        {
            if (i >= _values.Length
                || j >= _values[i].Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            _values[i][j] = value;
        }
    }
}
