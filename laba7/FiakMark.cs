using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    class FiakMark
    {
        static double _R;
        static double _Z;
        static double GO;
        static double Z2;
        static double Z1;
        static double[] CG;
        static double[] G;
        static double[] C;
        static int N;
        static int _M;
        static double[] X;
        public static void Fiacco_McCormick_Method()
        {
            Console.WriteLine("  Метод Фиакко и Маккормика");
            Console.WriteLine("Введите число переменных");
            N = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите число ограничений");
            _M = int.Parse(Console.ReadLine());
            X = new double[N];
            G = new double[N];
            CG = new double[N];
            C = new double[_M];
            double[] P = new double[N];
            double[] Y = new double[N];
            double[] U = new double[N];
            double[] D = new double[N];
            double[] V = new double[N];
            double[] R = new double[N];
            double[] Q = new double[N];
            double[] M = new double[N];
            double[,] H = new double[N, N];
            double[] IC = new double[_M];
            Console.WriteLine("Введите начальную точку X1,X2,...,XN");
            for (int I = 0; I < N; I++)
            {
                X[I] = double.Parse(Console.ReadLine());
            }

            double S = 0;
            for (int II = 0; II < _M; II++)
            {
                Limitations(II);
                if (C[II] < 0)
                {
                    S = S + 1;
                    IC[II] = 1;
                }
            }
            if (S > 0)
            {
                Console.WriteLine("Первая точка не является допустимой");
            }
            else
            {

                _R = 0;
                double T = 0, B = 0, CC = 0;
                Gradient();
                for (int I = 0; I < N; I++)
                {
                    T = T - G[I] * CG[I];
                    B = B + CG[I] * CG[I];
                }
                _R = T / B;
                if (_R < 0)
                {
                    _R = 1;
                }
                while (true)
                {
                    Console.WriteLine($"R = {_R}");
                    for (int I = 0; I < N; I++)
                    {
                        for (int J = 0; J < N; J++)
                        {
                            H[I, J] = 0;
                        }
                        H[I, I] = 1;
                    }
                    Console.WriteLine("\tТекущие значения");
                    while (true)
                    {
                        for (int I = 0; I < N; I++)
                        {
                            P[I] = X[I];
                            Y[I] = X[I];
                            Console.WriteLine($"X{I + 1} = {X[I]}");
                        }
                        Console.WriteLine();
                        for (int II = 0; II < _M; II++)
                        {
                            Limitations(II);
                        }
                        Z();
                        Console.WriteLine($"Итерация  {CC}  Значение  {_Z}");
                        double FP = _Z; Gradient();
                        double FQ, GP, GQ, HH, G2;
                        double G1 = GO, FF = _Z;
                        while (true)
                        {
                            for (int I = 0; I < N; I++)
                            {
                                U[I] = G[I]; D[I] = 0;
                                for (int J = 0; J < N; J++)
                                {
                                    D[I] = D[I] - H[I, J] * G[J];
                                }
                            }

                            GP = 0;
                            for (int I = 0; I < N; I++)
                            {
                                GP = GP + G[I] * D[I];
                            }
                            if (GP > 0)
                            {
                                Console.WriteLine("Функция возрастает (строка 620)");
                            }

                            double L = 2;
                            for (int I = 0; I < N; I++)
                            {
                                X[I] = P[I] + L * D[I];
                            }
                            while (true)
                            {
                                S = 0;
                                for (int II = 0; II < _M; II++)
                                {
                                    IC[II] = 0;
                                    Limitations(II);
                                    if (C[II] >= 0)
                                        continue;
                                    IC[II] = 1; S = S + 1;
                                    L = L / 1.05;
                                    for (int I = 0; I < N; I++)
                                    {
                                        X[I] = P[I] + L * D[I];
                                    }
                                    II--;
                                }
                                if (S == 0)
                                    break;
                            }
                            HH = L;
                            for (int I = 0; I < N; I++)
                            {
                                Q[I] = P[I] + HH * D[I];
                                X[I] = Q[I];
                            }
                            for (int II = 0; II < _M; II++)
                            {
                                Limitations(II);
                            }
                            Z();
                            FQ = _Z;
                            Gradient();
                            G2 = GO;
                            GQ = 0;
                            for (int I = 0; I < N; I++)
                            {
                                GQ = GQ + G[I] * D[I];
                            }
                            if (GQ < 0 && FQ < FP)
                            {

                                for (int I = 0; I < N; I++)
                                {
                                    for (int J = 0; J < N; J++)
                                    {
                                        H[I, J] = H[I, J] - D[I] * D[J] / GP;
                                    }
                                    P[I] = Q[I];
                                    X[I] = P[I];
                                    Y[I] = X[I];
                                }
                                FF = _Z; FP = _Z; G1 = GO;
                            }
                            else
                                break;
                        }
                        while (true)
                        {
                            double ZZ = 3 * (FP - FQ) / HH;
                            ZZ = ZZ + GP + GQ;
                            double WW = ZZ * ZZ - GP * GQ;
                            if (WW < 0)
                            {
                                WW = 0;
                            }
                            double W = Math.Sqrt(WW);
                            double DD = HH * (1 - (GQ + W - ZZ) / (GQ - GP + 2 * W));
                            for (int I = 0; I < N; I++)
                            {
                                X[I] = P[I] + DD * D[I];
                            }
                            for (int II = 0; II < _M; II++)
                            {
                                Limitations(II);
                            }
                            Z();
                            double FR = _Z; Gradient();
                            double GR = 0;
                            for (int I = 0; I < N; I++)
                            {
                                GR = GR + G[I] * D[I];
                            }
                            if (_Z <= FP && _Z <= FQ)
                                break;
                            if (GP > 0)
                            {
                                HH = DD;
                                for (int I = 0; I < N; I++)
                                {
                                    Q[I] = X[I];
                                }
                                FQ = _Z;
                                GQ = GR;
                                G2 = GO;
                            }
                            else
                            {
                                HH = HH - DD;
                                for (int I = 0; I < N; I++)
                                {
                                    P[I] = X[I];
                                }
                                FP = _Z;
                                GP = GR;
                                G1 = GO;
                            }
                        }
                        double KK = 0, WK = 0;
                        for (int I = 0; I < N; I++)
                        {
                            U[I] = G[I] - U[I];
                            V[I] = X[I] - Y[I];
                        }
                        for (int I = 0; I < N; I++)
                        {
                            M[I] = 0;
                            for (int J = 0; J < N; J++)
                            {
                                M[I] = M[I] + H[I, J] * U[J];
                            }
                            KK = KK + M[I] * U[I];
                            WK = WK + V[I] * U[I];
                        }
                        if (KK != 0 || WK != 0)
                        {
                            for (int I = 0; I < N; I++)
                            {
                                for (int J = 0; J < N; J++)
                                {
                                    H[I, J] = H[I, J] - M[I] * M[J] / KK + V[I] * V[J] / WK;
                                }
                            }
                        }
                        CC = CC + 1;
                        if (Math.Abs((FF - _Z) / FF) < 0.00001)
                            break;
                        FF = _Z;
                    }
                    if (_R * Z2 < 0.00001)
                        break;
                    _R = _R / 100;
                }
                Console.WriteLine();
                for (int I = 0; I < N; I++)
                {
                    Console.WriteLine($"X{I + 1} = {X[I]}");
                }
                Console.WriteLine();
                Console.WriteLine($"F(X) = {Z1}");
            }
        }

        static void Z()
        {

            Z1 = 3 * Math.Pow(X[0], 2) + 4 * X[0] * X[1] + 5 * Math.Pow(X[1], 2);
            Z2 = 0;
            for (int JJ = 0; JJ < _M; JJ++)
            {
                Z2 = Z2 + 1 / C[JJ];
            }
            _Z = Z1 + _R * Z2;
        }

        static void Gradient()
        {
 
            G[0] = 6 * X[0] + 4 * X[1];
            CG[0] = -1 / (C[2] * C[2]) - 1 / C[0];
            G[0] = G[0] + _R * CG[0];
            G[1] = 4 * X[0] + 10 * X[1];
            CG[1] = -1 / (C[2] * C[2]) - 1 / C[1];
            G[1] = G[1] + _R * CG[1];
            GO = 0;
            for (int JJ = 0; JJ < N; JJ++)
            {
                GO = GO + G[JJ] * G[JJ];
            }
            GO = Math.Sqrt(GO);
        }
        static void Limitations(int II)
        {

            switch (II)
            {
                case 0:
                    C[0] = X[2]*X[2] - X[0]*X[0]-X[1]*X[1];
                    break;
                case 1:
                    C[1] = X[0]*X[0]+X[1]*X[1]+X[2]*X[2]-4;
                    break;
                case 2:
                    C[2] = 5 - X[2];
                    break;
                case 3:
                    C[3] = X[0];
                    break;
                case 4:
                    C[4] = X[1];
                    break;
                case 5:
                    C[5] = X[2];
                    break;
            }
        }
    }

}

