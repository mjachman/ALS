using System;
using System.Collections.Generic;
namespace AlgNum3
{
    class Program
    {

        static double[,] ratings = new double[,] { { 1, 1, 2, 3, 4, 2, 5, 4, 0, 0 }, { 4, 1, 4, 1, 0, 4, 2, 1, 2, 4 }, { 5, 0, 4, 4, 1, 0, 5, 5, 0, 5 }, { 2, 0, 5, 0, 5, 3, 2, 5, 1, 5 }, { 1, 5, 5, 1, 5, 1, 2, 5, 2, 5 } };
       
        static int d = 10;
        static double reg = 0.1;
        static int n = 5;
        static int m = 10;
        //static double[,] P = new double[d, 10];
        //static double[,] U = new double[d, 3];
        static double[,] U = new double[d,5];
        static double[,] P = new double[d, 10]; 
        //{
        //    {0.93119636, 0.01215318, 0.82254304, 0.92704314, 0.72097256,
        //0.1119594 , 0.05907673, 0.27337659, 0.51578453, 0.47299487 },
        //    {0.1671686 , 0.02328032, 0.64793332, 0.46310597, 0.98508579,
        //0.23390272, 0.34862754, 0.29751156, 0.81994987, 0.32293732},
        //    {0.72302848, 0.91165485, 0.70980305, 0.20125138, 0.33071352,
        //0.40941998, 0.6984816 , 0.94986196, 0.52719633, 0.66722182} };

        
       // {
       //     { 0.02930222, 0.90635812, 0.71271017 },
       //{ 0.03319273, 0.2316068, 0.96492267},
       //{ 0.35638381, 0.42064508, 0.83929454} };

        static void Main(string[] args)
        {
            Random rnd = new Random();
            P.Fill(rnd);
            U.Fill(rnd);
            for (int iter = 0; iter < 1000; iter++)
            {
                for (int u = 0; u < n; u++)
                {

                    List<int> indexes = new List<int>();

                    for (int i = 0; i < ratings.GetLength(1); i++)
                    {
                        if (ratings[u, i] != 0.0)
                        {
                            indexes.Add(i);
                        }
                    }
                    double[,] P_I_U = new double[P.GetLength(0), indexes.Count];


                    int j = 0;
                    foreach (var ind in indexes)
                    {
                        var column = P.GetColumn(ind);
                        P_I_U.ReplaceColumn(column, j);
                        j++;
                    }


                    var P_I_U_T = P_I_U.Transpose();
                    
                    var E = MatrixOps.IdentityMatrix(d);
                    

                    var A_U = P_I_U.Multiply(P_I_U_T).Add(E.Multiply(reg));
                    
                    j = 0;

                    var V_U = new double[d];


                    for (j = 0; j < V_U.Length; j++)
                    {
                        foreach (var i in indexes)
                        {
                            V_U[j] += ratings[u, i] * P.GetColumn(i)[j];
                        }
                    }
                   

                    var solution = A_U.Solve(V_U);
                   

                    U.ReplaceColumn(solution, u);

                }

                for (int p = 0; p < m; p++)
                {
                    List<int> indexes = new List<int>();

                    for (int i = 0; i < ratings.GetLength(0); i++)
                    {
                        if (ratings[i, p] != 0.0)
                        {
                            indexes.Add(i);
                        }
                    }

                    
                    double[,] U_I_p = new double[U.GetLength(0), indexes.Count];
                    
                    //foreach (var row in indexes)
                    //{
                    //    for (int i = 0; i < U_I_p.GetLength(0); i++)
                    //    {
                    //        U_I_p[i, j] = U[i, row];
                    //    }
                    //    j++;
                    //}

                    int j = 0;
                    foreach (var col in indexes)
                    {
                        var column = U.GetColumn(col);
                        U_I_p.ReplaceColumn(column, j);
                        j++;
                    }

                    var U_I_p_T = U_I_p.Transpose();

                    var E = MatrixOps.IdentityMatrix(d);

                    var B_U = U_I_p.Multiply(U_I_p_T).Add(E.Multiply(reg));

                    j = 0;

                    var V_p = new double[d];


                    for (j = 0; j < V_p.Length; j++)
                    {
                        foreach (var i in indexes)
                        {
                            V_p[j] += ratings[i, p] * U.GetColumn(i)[j];
                        }
                    }
                   

                    var solution = B_U.Solve(V_p);

                    P.ReplaceColumn(solution, p);
                }
            }

            var result = U.Transpose().Multiply(P);
            result.Print();
            Console.ReadKey();
        }
    }
}
