using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //Many developers think centralizing the object creation is Factory Pattern: Wrong!!!
    //public class CustomerType
    //{
    //    public virtual void Calculate()
    //    {

    //    }

    //}

    public interface ICustomerType
    {
        void Calculate();
    }

    public interface ITax
    {
        void CalculateTax();
    }
    public interface IDelivery
    {
        void Deliver();
    }

    public class SpecialCustomer : ICustomerType
    {
        private readonly ITax _tax;
        private readonly IDelivery _delivery;
        public SpecialCustomer(ITax tax, IDelivery delivery)
        {
            _tax = tax; 
            _delivery = delivery;   
        }
        public void Calculate()
        {
            //Use tax and delivery
            throw new NotImplementedException();
        }
    }

    public class CourierDelivery : IDelivery
    {
        public void Deliver()
        {
            throw new NotImplementedException();
        }
    }

    public class PickupDelivery : IDelivery
    {
        public void Deliver()
        {
            throw new NotImplementedException();
        }
    }

    public class GSTTax : ITax
    {
        public void CalculateTax()
        {
            throw new NotImplementedException();
        }
    }
    public class VATTax : ITax
    {
        public void CalculateTax()
        {
            throw new NotImplementedException();
        }
    }
    public class GoldCustomer : BaseCustomerType
    {
        public GoldCustomer(ITax tax, IDelivery delivery) : base(tax, delivery)
        {

        }
        public override void Calculate()
        {
            //Do something
        }
    }
    public class BasicCustomer : BaseCustomerType
    {
        public BasicCustomer(ITax tax, IDelivery delivery) : base(tax, delivery)
        {

        }
        public override void Calculate()
        {
            //Do something
        }
    }
    public class DiscountedCustomer : BaseCustomerType
    {
        public DiscountedCustomer(ITax tax,IDelivery delivery):base(tax,delivery)
        {
            
        }
        public override void Calculate()
        {
            throw new NotImplementedException();
        }
    }

    //This is not factory pattern
    //Centralizing object creation is not factory pattern
    public static class SimpleFactoryCustomerType 
    {
        public static ICustomerType Create(int i)
        {
            if(i == 0)
            {
                return new DiscountedCustomer(new GSTTax(),new CourierDelivery());
            }
           return new GoldCustomer(new GSTTax(), new CourierDelivery());
        }

    }

    public abstract class BaseCustomerType: ICustomerType
    {
        ITax _tax;
        IDelivery _delivery;
        protected BaseCustomerType(ITax tax, IDelivery delivery)
        {
            
        }
        public abstract void Calculate();
    }

    public class CustomerTypeFactory
    {
        public static ICustomerType GetCustomerType(int i)
        {
            switch (i)
            {
                case 0:
                    return new DiscountedCustomer(new GSTTax(), new CourierDelivery());
                case 1:
                    return new GoldCustomer(new GSTTax(), new CourierDelivery());
                case 2: return new SpecialCustomer(new GSTTax(), new CourierDelivery());
                default:
                    throw new ArgumentException("Invalid customer type");
            }
        }
    }

    //Define an interface for creating an object
    public interface IFactoryCustomer
    {
        ICustomerType Create();
        ITax CreateTax();
        IDelivery CreateDelivery();
    }


    //Defer instantiation to the subclasses
    public class FactoryCustomer : IFactoryCustomer
    {
        public virtual ICustomerType Create()
        {
            //there can be several permutations and combinations
            return new BasicCustomer(CreateTax(),CreateDelivery());
        }

        public virtual IDelivery CreateDelivery()
        {
            //There will be several permutation and combinations, not so simplistic
            return new CourierDelivery();
        }

        public virtual ITax CreateTax()
        {
            //There will be several permutation and combinations, not so simplistic
            return new GSTTax();
        }
    }

    //Defer instantiation to the subclasses
    public class FactoryCustomerPickup: FactoryCustomer
    {
        public override IDelivery CreateDelivery()
        {
            return new PickupDelivery();  
        }
    }

    //This will have VAT Tax and Pickup Delivery
    //Defer instantiation to the subclasses
    public class FactoryCustomerVATPickup:FactoryCustomerPickup
    {
        public override ITax CreateTax()
        {
            return new VATTax();   
        }
    }

    //This will have a Gold Customer, VAT Tax and Pickup Delivery
    //Defer instantiation to the subclasses
    //Take 2-3 combinations from the top and change 1 step
    public class FactoryGoldCustomerVATPickup : FactoryCustomerPickup
    {
        public override ICustomerType Create()
        {
            return new GoldCustomer(CreateTax(),CreateDelivery());
        }
    }

    //Abstract Factory Pattern
    
    public class FactorySupplier
    {
        public ITax Create()
        {
            return new GSTTax();
        }
    }


}
