using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgNum3
{
    public static class MatrixOps
    {
        public static double[,] IdentityMatrix(int size)
        {
            double[,] matrix = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }
                }
            }
            return matrix;
        }

        public static double[,] Transpose(this double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        public static void Fill(this double[,] matrix,Random rnd)

        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rnd.NextDouble();
                }
            }
        }
        public static double[,] Add(this double[,] matrix1, double[,] matrix2)
        {

            var m = matrix1.GetLength(0);
            var n = matrix1.GetLength(1);
            var y = matrix2.GetLength(0);
            var z = matrix2.GetLength(1);

            double[,] matrix = new double[m, n];
            if (!(m == y && n == z))
            {
                throw new Exception("Matrices cannot be added");
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return matrix;

        }
       
        public static double[,] Multiply(this double[,] matrix1, double[,] matrix2)
        {
            int n = matrix1.GetLength(0);
            int m = matrix1.GetLength(1);
            int p = matrix2.GetLength(1);
            if (m != matrix2.GetLength(0))
            {
                throw new Exception("Matrices cannot be multiplied");
            }
            double[,] result = new double[n, p];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }
        public static double[] Multiply(this double[] vector,double number)
        {
            for(int i=0;i<vector.GetLength(0);i++)
            {
                vector[i] *= number;
            }
            return vector;
        }
        public static double[,] Multiply(this double[,] matrix, double number)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            double[,] result = new double[n, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = matrix[i,j] * number;
                }
            }
            return result;

        }
        public static void Print(this double[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format($"{matrix[i, j]} "));
                }
                Console.Write("\n");
            }
        }
        public static void Print(this double[] vector)
        {
            foreach(var v in vector)
            {
                Console.Write(v + " ");
            }
            Console.Write("\n");
        }
        public static double[] GetColumn(this double[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }
        public static double[] GetRow(double[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        public static void ReplaceRow(this double[,] matrix,double[] row, int rowNumber)
        {
            for (int i = 0; i < row.Length; i++)
            {
                matrix[rowNumber, i] = row[i];
            }

        }
        public static void ReplaceColumn(this double[,] matrix, double[] column, int columnNumber)
        {
            for (int i = 0; i < column.Length; i++)
            {
                matrix[i, columnNumber] = column[i];
            }
        }

        public static double[] Solve(this double[,] A, double[] b)
        {
            int n = b.Length;


            for (int p = 0; p < n; p++)
            {
                

                    int max = p;
                    for (int i = p + 1; i < n; i++)
                    {
                            if(Math.Abs(A[i, p])>Math.Abs(A[max,p]))
                        {
                            max = i;
                        }
                    }
                    double[] temp = GetRow(A, p);
                    double[] maxx = GetRow(A, max); ;
                    ReplaceRow(A, maxx, p);
                    ReplaceRow(A, temp, max);

                    double t = b[p];
                    b[p] = b[max];
                    b[max] = t;
                
                

                if (A[p, p]==0)
                {
                    throw new ArithmeticException("");
                }


                for (int i = p + 1; i < n; i++)
                {
              
                    double alpha = A[i, p] / A[p, p];
                    b[i] -= alpha * b[p];                 
                    for (int j = p; j < n; j++)
                    {

                        A[i,j] -= alpha * A[p,j];
                    }
                }


            }

            double[] x = new double[n];

            for (int i = n - 1; i >= 0; i--)
            {

                double sum = 0.0;




                for (int j = i + 1; j < n; j++)
                {
                    sum += A[i,j] * x[j];
                }
                x[i] = (b[i] - sum) / A[i,i];

            }

            return x;
        }

    }

    

}
