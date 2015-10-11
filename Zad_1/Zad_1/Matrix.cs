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

        public int Rows
        {
            get { return _values != null ? _values.Length : 0; }
        }

        public int Columns
        {
            get { return (_values != null && _values.Length > 0 && _values[0] != null) ? _values[0].Length : 0; }
        }

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

        public Matrix(Matrix original)
        {
            _values = new double[original._values.Length][];
            for (int i = 0; i < _values.Length; i++)
            {
                _values[i] = new double[original._values[i].Length];
            }

            for (int i = 0; i < _values.Length; i++)
            {
                for (int j = 0; j < _values[i].Length; j++)
                {
                    _values[i][j] = original._values[i][j];
                }
            }
        }

        public Matrix GetRow(int index)
        {
            double [][]tmp = new double[1][];
            tmp[0] = new double[_values[index].Length];
            for (int i = 0; i < _values[index].Length; i++)
            {
                tmp[0][i] = _values[index][i];
            }

            return new Matrix(tmp);
        }

        public Matrix GetColumn(int index)
        {
            double[][] tmp = new double[_values.Length][];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = new double[1];
            }
            for (int i = 0; i < _values.Length; i++)
            {
                tmp[i][0] = _values[i][index];
            }

            return new Matrix(tmp);
        }

        public Matrix RemoveColumn(int index)
        {
            double[][] tmp = new double[_values.Length][];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = new double[_values[i].Length - 1];
            }
            for (int i = 0; i < _values.Length; i++)
            {
                for (int j = 0, y = 0; j < _values[i].Length; j++, y++)
                {
                    if (j == index)
                    {
                        y--;
                        continue;
                    }
                    tmp[i][y] = _values[i][j];
                }
            }

            return new Matrix(tmp);
        }

        public void Print()
        { 
            foreach (double[] d in _values)
            {
                foreach (double d1 in d)
                {
                    Console.Write(d1 + " ");
                }
                Console.WriteLine();
            }
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
