using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class EventSource
    {
        public List<IEvent> Events { get; set; }
        public EventSource()
        {
            this.Events = new List<IEvent>();
        }
    }
}
