using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class SingletonCache
    {
        private static List<Country> _countries = null;
        private static int _counter = 0;

        public static void Increment()
        {
            _counter = _counter + 1;    
        }
        public static IEnumerable<Country> GetCountries()
        {
            //only one thread will run
            lock (_countries)
            {
                if (_countries == null)
                {
                    _countries = new List<Country>();
                    _countries.Add(new Country() { Id = 1, Name = "India" });
                    _countries.Add(new Country() { Id = 2, Name = "USA" });
                }
            }

            //return a Clone of the countries object so that original _countries object is not modified
            return _countries.ToList<Country>();
        }
    }
}
