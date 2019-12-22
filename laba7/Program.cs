using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba7
{
    class Program
    {
        static double FE;
        static double _Z;
        static double[] X;
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите метод:\n1) Метод Хука-Дживса\n2) Комплексный метод\n3)  Метод Фиакко и Маккормика");
            int met = int.Parse(Console.ReadLine());
            switch(met)
            {
                case 1:
                    Hook_Jeeves_Method();
                    break;
                case 2:
                    ComplexM complex = new ComplexM();
                    ComplexM.Complex_Method();
                    break;
                case 3:
                    FiakMark fiakMark = new FiakMark();
                    FiakMark.Fiacco_McCormick_Method();
                    break;
                default:
                    return;
            }
            Console.ReadKey();
            
            

        }

        public static void Hook_Jeeves_Method()
        {
            Console.WriteLine("Метод Хука-Дживса при наличии ограничений");
            Console.WriteLine("Введите число переменных");
            int N = int.Parse(Console.ReadLine());
            X = new double[N];
            double[] B = new double[N];
            double[] Y = new double[N];
            double[] P = new double[N];
            Console.WriteLine("Введите начальную точку X1,X2,...XN ");
            for (int I = 0; I < N; I++)
                X[I] = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите длину шага");
            double H = double.Parse(Console.ReadLine());
            double K = H, FI;
            for (int I = 0; I < N; I++)
            {
                Y[I] = X[I];
                P[I] = X[I];
                B[I] = X[I];
            }
            Z();
            FI = _Z;
            Console.WriteLine($"Начальные значения функции {_Z}");
            for (int I = 0; I < N; I++)
                Console.Write($"{X[I]}   ");
            Console.WriteLine();
            int PS = 0, BS = 1;

            int J = 0;
            double FB = FI;
            do
            {
                X[J] = Y[J] + K;
                Z();
                if (_Z >= FI)
                {
                    X[J] = Y[J] - K;
                    Z();
                    if (_Z >= FI)
                        X[J] = Y[J];
                    else Y[J] = X[J];
                }
                else Y[J] = X[J];
                Z();
                FI = _Z;
                Console.WriteLine($"Пробный шаг {_Z}");
                for (int I = 0; I < N; I++)
                    Console.Write($"{X[I]}   ");
                Console.WriteLine();
                if (J == N - 1)
                {
                    if (FI < FB - 1E-08)
                    {
                        for (int I = 0; I < N; I++)
                        {
                            P[I] = 2 * Y[I] - B[I];
                            B[I] = Y[I]; X[I] = P[I]; Y[I] = X[I];
                        }
                        FB = FI; PS = 1;
                        BS = 0; Z(); FI = _Z;
                        Console.WriteLine($"Поиск по образцу  {_Z}");
                        for (int I = 0; I < N; I++)
                            Console.Write($"{X[I]}  ");
                        Console.WriteLine();
                        J = 0;
                    }
                    else
                    {
                        if (PS == 1 && BS == 0)
                        {
                            for (int I = 0; I < N; I++)
                            {
                                P[I] = B[I];
                                Y[I] = B[I];
                                X[I] = B[I];
                            }
                            BS = 1;
                            PS = 0;
                            Z();
                            FI = _Z; FB = _Z;
                            Console.WriteLine($"Замена базисной точки {_Z}");
                            for (int I = 0; I < N; I++)
                                Console.Write($"{X[I]}  ");
                            Console.WriteLine();
                            J = 0;
                        }
                        else
                        {
                            K = K / 10;
                            Console.WriteLine("Уменьшить длину шага");
                            if (K <= 1E-08)
                                break;
                            J = 0;
                        }
                    }
                }
                else J = J + 1;
            } while (true);
            Console.WriteLine("\tМинимум найден");
            for (int I = 0; I < N; I++)
                Console.Write($"X{I + 1} = {P[I]}  ");
            Console.WriteLine();
            Console.WriteLine($"Минимум функции равен {FB}");
            Console.WriteLine($"Количество вычислений равно {FE}");
        }

        public static void Z()
        {
            FE = FE + 1;
            if (X[0] >= 0 && X[1] >= 0 && X[0] + X[1] >= 4)
                _Z = (float)(3 * Math.Pow(X[0], 2) + 4 * X[0] * X[1] + 5 * Math.Pow(X[1], 2));
            else _Z = 1e+30F;
        }




    }


}
