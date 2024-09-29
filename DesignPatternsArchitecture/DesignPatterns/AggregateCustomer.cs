using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class AggregateCustomer
    {
        private AuditCustomer Audit { get; set; }
        private Customer customer { get; set; }

        public Customer GetCustomer()
        {
            return customer;
        }

        public AuditCustomer GetAuditCustomer()
        {
            return Audit;
        }
        public AggregateCustomer(string name, decimal amount)
        {
            Customer customer = new Customer();
            customer.Name = name;
            customer.Amount = amount;
            Audit = new AuditCustomer();
        }
    }
}
