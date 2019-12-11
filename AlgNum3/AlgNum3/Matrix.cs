using System;
using System.Collections.Generic;
using System.Text;

namespace AlgNum3
{
    class Matrix
    {
        public double[,] matrix;

        public Matrix(int n,int m)
        {
            matrix = new double[n, m];
        }
        public static Matrix operator +(Matrix a) => a;
    }
}
