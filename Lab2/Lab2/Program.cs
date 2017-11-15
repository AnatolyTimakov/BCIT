using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    abstract class Figure : IComparable
    {
        public abstract double area();
    }
    interface IPrint
    {
        void print();
    }
    class Rectangle : Figure, IPrint
    {
        private double _width;
        public double width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        public double height { get; set; }
        public Rectangle(double a, double b) { _width = a; height = b; }
        public override double area()
        {
            return _width*height;
        }
        public override string ToString()
        {
            string temp;
            temp = ("Rectangle: width = {0} \n" + "height = {1} \n" + "area = {2} \n", _width, height, area);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
