using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
     class NormalCustomer : ICustomer
    {

        //Example of Association, there is only a thin relationship between classes
         public NormalCustomer(IRating rating)
        {
            Rating = rating.getRating(this);
        }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<IAddress> Addresses()
        {
            throw new NotImplementedException();
        }

        public decimal CalculateDiscount()
        {
            if (IsAmountValid())
            {
                IDiscount discount = new Discount();
                return discount.Calculate(this);
            }
            return 0;
        }

        //Make an abstraction...ensure that only interface methods are accessible, this is an internal method
        private bool IsAmountValid()
        {
            return Amount > 0;
        }
    }

    public class CustomerAgeType : ICustomer
    {
        public decimal Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ICustomer.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ICustomer.Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        decimal ICustomer.Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        decimal ICustomer.Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public decimal CalculateDiscount()
        {
            throw new NotImplementedException();
        }

        List<IAddress> ICustomer.Addresses()
        {
            throw new NotImplementedException();
        }

        decimal ICustomer.CalculateDiscount()
        {
            throw new NotImplementedException();
        }
    }

    class Rating : IRating
    {
        public decimal getRating(ICustomer customer)
        {
            throw new NotImplementedException();
        }
    }

    class Discount : IDiscount
    {
        public decimal Calculate(ICustomer customer)
        {
            throw new NotImplementedException();
        }
    }
}
