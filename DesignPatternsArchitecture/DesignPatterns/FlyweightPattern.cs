using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class VistingCard
    {
        public string empName { get; set; }
        public static SharedData Common { get; set; }
    }
    public class SharedData
    {
        public string CompanyName { get; set; }
    }
}
