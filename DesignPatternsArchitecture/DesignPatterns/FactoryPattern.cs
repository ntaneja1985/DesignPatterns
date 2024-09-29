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

    public interface CustomerType
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

    public class SpecialCustomer : CustomerType
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

    public class GSTTax : ITax
    {
        public void CalculateTax()
        {
            throw new NotImplementedException();
        }
    }
    public class GoldCustomer : CustomerType
    {
        public void Calculate()
        {
            //Do something
        }
    }

    public class DiscountedCustomer : CustomerType
    {
        public void Calculate()
        {
            //Do something
        }
    }

    //This is not factory pattern
    //Centralizing object creation is not factory pattern
    public static class SimpleFactoryCustomerType 
    {
        public static CustomerType Create(int i)
        {
            if(i == 0)
            {
                return new DiscountedCustomer();
            }
           return new GoldCustomer();
        }

    }

    public class CustomerTypeFactory
    {
        public static CustomerType GetCustomerType(int i)
        {
            switch (i)
            {
                case 0:
                    return new DiscountedCustomer();
                case 1:
                    return new GoldCustomer();
                case 2: return new SpecialCustomer(new GSTTax(), new CourierDelivery());
                default:
                    throw new ArgumentException("Invalid customer type");
            }
        }
    }

}
