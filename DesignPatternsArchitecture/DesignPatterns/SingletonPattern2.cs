using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> lazyInstance = new Lazy<Singleton>(() => new Singleton());

        //Private constructor to prevent instantiation from outside
        private Singleton() { }

        //Public Property to provide Access from Outside
        public static Singleton Instance { get { return lazyInstance.Value; } }

        public void DoSomething()
        {
            Console.WriteLine("Singleton instance called");
        }
    }
}
