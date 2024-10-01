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
        protected CustomerBase()
        {
            this.Name = string.Empty;
            this.Phone = string.Empty;
            this.BillAmount = 0;
        }

        // public abstract void Validate();
    }

    public class NormalCustomerNew : CustomerBase
    {
        
    }


    //Decorator Pattern
    public class ValidationLinker : IValidate
    {
        IValidate _nextValidateLink = null;
        public ValidationLinker(IValidate validate)
        {
            _nextValidateLink=validate;
        }
        public virtual void Validate(ICustomerAbstraction obj)
        {
            _nextValidateLink.Validate(obj);
        }
    }

    public class BasicValidation : IValidate
    {
        public void Validate(ICustomerAbstraction obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                throw new Exception("Name is required");
            }
        }
    }

    public class PhoneCheckValidation : ValidationLinker
    {
        public PhoneCheckValidation(IValidate obj) : base(obj)
        {

        }

        public override void Validate(ICustomerAbstraction obj)
        {
            //First call the base validation
            base.Validate(obj);

            //Then run our own validation
            if (string.IsNullOrEmpty(obj.Phone))
            {
                throw new Exception("Phone number is required");
            }
        }
    }

    public class BillCheckValidation : ValidationLinker
        {
            public BillCheckValidation(IValidate obj):base (obj) 
            {
                
            }
            public override void Validate(ICustomerAbstraction obj)
            {
                //First call the base validation
                base.Validate(obj);

                //Then run our own validation
                if (obj.BillAmount == 0)
                {
                    throw new Exception("Bill Amount cannot be zero");
                }
            }
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
