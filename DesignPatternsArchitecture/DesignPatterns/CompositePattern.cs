using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
   public interface IMenu
    {
        IMenu SubMenu { get; set;}
        string Name { get; set; } 
    }

    public class Menu : IMenu
    {
        public string Name { get; set; }
        public IMenu SubMenu { get; set; }
    }

}
