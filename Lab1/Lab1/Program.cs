using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void PrintCheck(float n, bool conv)
        {
            if (conv)
            {
                Console.WriteLine("Вы ввели число " + n.ToString("F5"));
            }
            else
            {
                Console.WriteLine("Вы ввели не число");
            }
        }
        static void Main(string[] args)
        {
            float a, b, c;
            bool conv;
            string str;
            do
            {
                Console.Write("Введите коэффициент A:");
                str = Console.ReadLine();
                conv = float.TryParse(str, out a);
                PrintCheck(a, conv);
            }while(!conv);
            do
            {
                Console.Write("Введите коэффициент B:");
                str = Console.ReadLine();
                conv = float.TryParse(str, out b);
                PrintCheck(b, conv);
            }while(!conv);
            do
            {
                Console.Write("Введите коэффициент C:");
                str = Console.ReadLine();
                conv = float.TryParse(str, out c);
                PrintCheck(c, conv);
            } while (!conv);
            double D;
            D = b * b - 4 * a * c;
            double x1, x2;
            Console.WriteLine("Дискриминант равен: {0}", D);
            if (D > 0)
            {
                x1 = (-b + Math.Sqrt(D))/a*2;
                x2 = (-b - Math.Sqrt(D))/a*2;
                Console.WriteLine("1-ый корень равен: {0}", x1);
                Console.WriteLine("2-ой корень равен: {0}", x2);
            }
            else if (D == 0)
            {
                x1 = -b / (a * 2);
                x2 = x1;
                Console.WriteLine("Корень равен: {0}", x1);
            }
            else
            {
                Console.Write("Корней нет");
            }
        }
    }
}
