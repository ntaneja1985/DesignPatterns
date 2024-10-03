using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class SupplierClass
    {

        private SupplierClass oldValues = null;

        public void SaveState()
        {
            oldValues = (SupplierClass)this.Clone();   
        }

        public void Revert()
        {
            this.Id = oldValues.Id;
            this.Name = oldValues.Name; 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public SupplierClass()
        {
            this.Address = new Address();
        }

        public SupplierClass Clone()
        {
            var cloned = (SupplierClass)this.MemberwiseClone();
            cloned.Address = (Address)this.Address.Clone();
            return cloned ;
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Address Clone()
        {
            return (Address)this.MemberwiseClone();
        }
    }

    //Memento Pattern
    public class SupplierRepository
    {
        public SupplierClass Load()
        {
            SupplierClass temp = new SupplierClass();
            temp.Id = 100;
            temp.Name = "Test";
            return temp ;

        }
    }
}
