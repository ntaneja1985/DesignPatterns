using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface ICustomerAbstraction
    {
        string Name { get; set; }
        string Phone { get; set; }
        decimal BillAmount { get; set; }

        //void Validate(); //Violation of Liskov, is it the job of customer to validate himself?
    }

    public interface IValidate
    {
        void Validate(ICustomerAbstraction obj);
    }

    public abstract class CustomerBase : ICustomerAbstraction
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal BillAmount { get; set; }

       // public abstract void Validate();
    }

    public class NormalCustomerNew : CustomerBase
    {
        
    }

    public class ValidateNormal : IValidate
    {
        public void Validate(ICustomerAbstraction obj)
        {
            if (obj.Name.Length == 0 || obj.Phone.Length == 0) 
            {
                throw new Exception("Name and Phone both are required");
            }
        }
    }


    public class PaidCustomerNew : CustomerBase
        {
            
        }

    public class PaidValidate : IValidate
    {
        public void Validate(ICustomerAbstraction obj)
        {
            if (obj.Name.Length == 0 || obj.Phone.Length == 0)
            {
                throw new Exception("Name and Phone both are required");
            }
        }
    }
}
