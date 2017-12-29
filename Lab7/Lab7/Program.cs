using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace office
{
    class Worker
    {
        public int id_worker;
        public string surname;
        public int id_department;
        public Worker() { }
        public Worker(int a, string s, int b)
        {
            this.id_worker = a;
            this.surname = s;
            this.id_department = b;
        }
        public override string ToString()
        {
            StringBuilder a = new StringBuilder();
            StringBuilder b = new StringBuilder();
            StringBuilder c = new StringBuilder();
            return this.id_worker.ToString() + a.Append(Convert.ToChar(32), 9) + "| " + this.surname + b.Append(Convert.ToChar(32), 20 - this.surname.Length) + "| " + this.id_department + c.Append(Convert.ToChar(32), 13) + "|";
        }
    }
    class Department
    {
        public int id_department;
        public string title;
        public Department() { }
        public Department(int a, string s)
        {
            this.id_department = a;
            this.title = s;
        }
        public override string ToString()
        {
            StringBuilder a = new StringBuilder();
            StringBuilder b = new StringBuilder();
            return this.id_department.ToString() + a.Append(Convert.ToChar(32), 13) + "| " + this.title.ToString() + b.Append(Convert.ToChar(32), 15 - this.title.Length) + "|";
        }
    }
    class Relations
    {
        public int id_worker;
        public int id_department;
        public Relations(int a, int b)
        {
            this.id_worker = a;
            this.id_department = b;
        }
        public override string ToString()
        {
            return "workerid=" + this.id_worker.ToString() + "| OfficeID=" + this.id_department + "|";
        }
    }
}

namespace Lab7
{
    class Program
    {
        static List<office.Worker> workers = new List<office.Worker>()
        {
            new office.Worker(1, "Лескина ", 1),
            new office.Worker(2, "Брысина ", 4),
            new office.Worker(3, "Байбарин ", 2),
            new office.Worker(4, "Гаврилюк ", 3),
            new office.Worker(5, "Баскакова ", 3),
            new office.Worker(6, "Кондрашева ", 4),
            new office.Worker(7, "Тимаков ", 1),
            new office.Worker(8, "Авдеев ", 2),
            new office.Worker(9, "Александрова ", 2),
        };
        static List<office.Department> rooms = new List<office.Department>()
        {
            new office.Department(1, "Отдел финансов "),
            new office.Department(2, "Общий отдел "),
            new office.Department(3, "Отдел кадров "),
            new office.Department(4, "IT отдел ")
        };
        static List<office.Relations> rel = new List<office.Relations>()
        {
            new office.Relations(1, 1),
            new office.Relations(2, 4),
            new office.Relations(3, 2),
            new office.Relations(4, 3),
            new office.Relations(5, 3),
            new office.Relations(6, 4),
            new office.Relations(7, 1),
            new office.Relations(8, 2),
            new office.Relations(9, 2),
            new office.Relations(6, 3),
            new office.Relations(7, 2),
            new office.Relations(8, 4),
            new office.Relations(9, 1)
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Перечисление всех сотрудников:");
            var i1 = from x in workers select x;
            Console.WriteLine("{0, -11} {1, -21} {2}", "id_worker", "surname", "id_department");
            foreach (var x in i1)
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nПеречисление всех офисов:");
            Console.WriteLine("{0, -15} {1}", "id_department", "title");
            var i2 = from x in rooms select x;
            foreach (var x in i2) Console.WriteLine(x);

            Console.WriteLine("\nCписок всех сотрудников, отсортированный по отделам:");
            Console.WriteLine("{0, -11} {1, -21} {2}", "id_worker", "surname", "id_department");
            foreach (var r in rooms)
            {
                var i3 = from x in workers where r.id_department == x.id_department select x;
                foreach (var x in i3) Console.WriteLine(x);
            }

            Console.WriteLine("\nCписок всех сотрудников, фамилия которых начинается на А:");
            Console.WriteLine("{0, -11} {1, -21} {2}", "id_worker", "surname", "id_department");
            foreach (var x in workers)
                if (x.surname[0] == 'А') Console.WriteLine(x);

            Console.WriteLine("\nСписок отделов, в которых хотя бы у одного сотрудника фамилия начинается с буквы А");
            Console.WriteLine("{0, -15} {1}", "id_department", "title");
            var i5 = from x in rooms
                     where (workers.Count(y => y.surname[0] == 'А' && y.id_department == x.id_department) > 0)
                     select x;
            foreach (var x in i5) Console.WriteLine(x);

            Console.WriteLine("\nCписок всех отделов и количество сотрудников в каждом отделе");
            Console.WriteLine("{0, -15} {1, -15} {2}", "id_department", "title", "Количество");
            foreach (var x in rooms)
            {
                int num = workers.Count(y => y.id_department == x.id_department);
                Console.WriteLine(x + "" + num);
            }

            Console.WriteLine("\nВывести список всех отделов и количество сотрудников в каждом отделе");
            Console.WriteLine("{0, -15} {1, -16} {2}", "id_department", "title", "Количество сотрудников");
            foreach (var x in rooms)
            {
                //Перебор по связям отдел-работник
                var i8 = from y in rel
                         where (y.id_department == x.id_department)
                         select y;
                Console.WriteLine(x + " " + i8.Count());
            }
        }
    }
}
