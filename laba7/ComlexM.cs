using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    public class ComplexM
    {
        static float Last;
        static Random rnd = new Random();
        static float Rnd(int num)
        {
            if (num > 0)
            {
                Last = (float)rnd.NextDouble();
            }
            else if (num < 0)
            {
                rnd = new Random(num);
                Last = (float)rnd.NextDouble();
            }
            return Last;
        }

        static float _Z;
        static int _IC;
        static int _EC;
        static int FE;
        static float[] X;
        static float[] Y;
        static float[] L;
        static float[] U;
        static float[] XC;
        static float[] XO;
        static float[] XR;
        static float[] XH;
        static float[,] C;
        static float[] F;
        static float[] G;
        static float[] IC;
        static float[] EC;
        static int M, N;
        static int IM;
        public static void Complex_Method()
        {
            
            Console.WriteLine("Комплексный метод");
            Console.WriteLine("Выберите функцию:\n1) -X1*X2*X3 + 3300\n2) X^2 + Y^2");
            int func = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество ограничений");
            M = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество переменных");
            N = int.Parse(Console.ReadLine());
            X = new float[N];
            Y = new float[N];
            L = new float[] { 0, 0, 0 };
            U = new float[] { 20, 11, 42 };
            XC = new float[N];
            XO = new float[N];
            XR = new float[N];
            XH = new float[N];
            int K = 2 * N, PP = 0;
            C = new float[K, N];
            F = new float[K];
            G = new float[M];
            IC = new float[M];
            EC = new float[2 * N];
            Console.WriteLine("Введите начальные значения");
            for (int J = 0; J < N; J++)
            {
                X[J] = float.Parse(Console.ReadLine());
                C[0, J] = X[J]; XC[J] = X[J];
            }

            Console.WriteLine("Введите X");
            int _X = int.Parse(Console.ReadLine());
            int I = 0;
            if (func == 1) Z1_1();
            else Z1_2();
            F[0] = _Z;
            M600:
            I = I + 1;
            for (int J = 0; J < N; J++)
            {
                float rand = Rnd(_X);
                Console.WriteLine($"rnd={rand}");
                C[I, J] = L[J] + 0.0001f * (U[J] - L[J]);
                X[J] = C[I, J];
            }
            int IM = 1;
            M640:
            if (func == 1) LimitCheck_1();
            else LimitCheck_2();
            if (_IC == 1)
                goto M720;

            for (int J = 0; J < N; J++)
            {
                XC[J] = ((I - 1) * XC[J] + C[I, J]) / I;
            }
            goto M760;
            M720:
            for (int J = 0; J < N; J++)
            {
                C[I, J] = (C[I, J] + XC[J]) / 2;
                X[J] = C[I, J];
            }
            goto M640;
            M760:
            if (func == 1) Z1_1();
            else Z1_2();
            F[I] = _Z;
            if (I < K - 1)
                goto M600;

            for (int J = 0; J < K - 1; J++)
            {
                for (int i = J + 1; i < K; i++)
                {
                    if (F[J] <= F[i])
                        continue;
                    float f = F[J];
                    F[J] = F[i];
                    F[i] = f;
                    for (int kkk = 0; kkk < N; kkk++)
                    {
                        Y[kkk] = C[J, kkk]; C[J, kkk] = C[i, kkk];
                        C[i, kkk] = Y[kkk];
                    }
                }
            }

            float FM = F[0];
            Console.WriteLine("Первая точка");
            Console.WriteLine($"Минимальное значение = {F[0]}");
            Console.WriteLine("Минимальная точка");
            for (int kkk = 0; kkk < N; kkk++)
            {
                Console.Write($"X{kkk + 1} = {C[0, kkk]}\t");
            }
            Console.WriteLine();
            float FR;

            float A = 1.3f;
            M1190:

            for (int kkk = 0; kkk < N; kkk++)
            {
                XH[kkk] = C[K - 1, kkk]; XO[kkk] = (K * XC[kkk] - XH[kkk]) / (K - 1);
            }

            for (int kkk = 0; kkk < N; kkk++)
            {
                XR[kkk] = (1 + A) * XO[kkk] - A * XH[kkk];
                X[kkk] = XR[kkk];
            }
            M1490:

            IM = 0;
            if (func == 1) LimitCheck_1();
            else LimitCheck_2();

            if (_EC == 0 && _IC == 0)
                goto M2000;
            if (_EC == 0)
                goto M1800;
            for (int J = 0; J < N; J++)
            {
                if (EC[J] == 1)
                {
                    XR[J] = L[J] + 0.0001f;
                    X[J] = XR[J];
                }
                if (EC[J + N] == 1)
                {
                    XR[J] = U[J] - 0.0001f;
                    X[J] = XR[J];
                }
            }
            M1800:
            if (_IC == 0) goto M2000;

            for (int kkk = 0; kkk < N; kkk++)
            {
                XR[kkk] = (XR[kkk] + XC[kkk]) / 2;
                X[kkk] = XR[kkk];
            }
            goto M1490;
            M2000:
            if (func == 1) Z1_1();
            else Z1_2();
            FR = _Z;

            if (FR < F[K - 1])
                goto M2400;
            for (int kkk = 0; kkk < N; kkk++)
            {
                XR[kkk] = (XR[kkk] + XO[kkk]) / 2;
                X[kkk] = XR[kkk];
            }
            goto M1490;
            M2400:

            F[K - 1] = FR;
            for (int kkk = 0; kkk < N; kkk++)
            {
                XC[kkk] = K * XC[kkk] - C[K - 1, kkk] + XR[kkk];
                XC[kkk] = XC[kkk] / K; C[K - 1, kkk] = XR[kkk];
            }
            /*упорядочить значения функции и точки,
             * в которых она вычислена*/
            for (int J = 0; J < K - 1; J++)
            {
                for (int i = J + 1; i < K; i++)
                {
                    if (F[J] <= F[i])
                        continue;
                    float f = F[J];
                    F[J] = F[i];
                    F[i] = f;
                    for (int kkk = 0; kkk < N; kkk++)
                    {
                        Y[kkk] = C[J, kkk]; C[J, kkk] = C[i, kkk];
                        C[i, kkk] = Y[kkk];
                    }
                }
            }

            if (F[0] < FM)
                PP = 1;

            if (PP == 0)
                goto M1190;

            float S1 = 0, S2 = 0;
            for (int i = 0; i < K; i++)
            {
                S1 = S1 + F[i];
                S2 = S2 + F[i] * F[i]; ;
            }
            float SD = S2 - S1 * S1 / K; SD = SD / K;
            float DM = 0, D = 0;
            for (int i = 0; i < K - 1; i++)
            {
                for (int J = i + 1; J < K; J++)
                {
                    D = 0;
                    for (int kkk = 0; kkk < N; kkk++)
                    {
                        D = D + (float)Math.Pow(C[i, kkk] - C[J, kkk], 2);
                    }
                    D = (float)Math.Sqrt(D);
                    if (D > DM)
                        DM = 0;
                }
            }
            if (PP == 0)
                goto M3790;
            Console.WriteLine("Новая точка в строке 3500");
            Console.WriteLine($"Минимальное значение = {F[0]}");
            Console.WriteLine("Точка минимума");
            for (int kkk = 0; kkk < N; kkk++)
            {
                Console.Write($"X{kkk + 1} = {C[0, kkk]}\t");
            }
            Console.WriteLine();
            FM = F[0]; PP = 0;
            M3790:
            if (SD > 0.00001 && DM > 0.0001)
                goto M1190;
            Console.WriteLine("Минимум найден");
            Console.WriteLine("Точка минимума");
            for (int kkk = 0; kkk < N; kkk++)
            {
                Console.Write($"X{kkk + 1} = {C[0, kkk]}\t");
            }
            Console.WriteLine();
            Console.WriteLine($"Минимум функции = {F[0]}");
            Console.WriteLine($"Количество вычислений функции = {FE}");
        }
        public static void Z1_1()
        {
            _Z = -X[0] * X[1] * X[2] + 3300;
            FE = FE + 1;
        }

        public static void Z1_2()
        {
            _Z = (float)(Math.Pow(X[0], 2) + Math.Pow(X[1], 2));
            FE = FE + 1;
        }
        public static void LimitCheck_1()
        {
            for (int II = 0; II < 2 * N; II++)
                EC[II] = 0;
            _EC = 0;
            for (int II = 0; II < M; II++)
                IC[II] = 0;
            _IC = 0;
            if (IM != 1)
            {
                for (int II = 0; II < N; II++)
                {
                    if (X[II] < L[II])
                    {
                        EC[II] = 1; _EC = 1;
                    }
                    if (X[II] > U[II])
                    {
                        EC[N + II] = 1; _EC = 1;
                    }
                }
            }
            G[0] = X[0] + 2 * X[1] + 2 * X[2];
            if (G[0] > 72)
            {
                IC[0] = 1; _IC = 1;
            }
        }

        public static void LimitCheck_2()
        {
            for (int II = 0; II < 2 * N; II++)
                EC[II] = 0;
            _EC = 0;
            for (int II = 0; II < M; II++)
                IC[II] = 0;
            _IC = 0;
            if (IM != 1)
            {
                for (int II = 0; II < N; II++)
                {
                    if (X[II] < L[II])
                    {
                        EC[II] = 1; _EC = 1;
                    }
                    if (X[II] > U[II])
                    {
                        EC[N + II] = 1; _EC = 1;
                    }
                }
            }
            G[0] = X[0] + X[1];
            if (G[0] < 5)
            {
                IC[0] = 1; _IC = 1;
            }
        }

 
        

        

    }
}
