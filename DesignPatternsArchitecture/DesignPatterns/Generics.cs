using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class Generics
    {
        public bool Compare(int num1, int num2) 
        {
            return num1 == num2;
        
        }

        public bool Compare(string str1, string str2)
        {
            return str1 == str2;
        }

        public bool Compare<T>(T obj1, T obj2)
        {
            return obj1.Equals(obj2);
        }

    }
}
