using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public abstract class EventBase: IEvent
    {
        public EventBase()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class CustomerCreated: EventBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CustomerCancelled : EventBase
    { 
        public int Id { get; set; } 
        public string Name { get; set; }
    }
}
