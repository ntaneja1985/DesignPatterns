using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{

    //Move all common methods across classes into BaseClass
    public abstract class BaseCustomer : ICustomer
    {
        //public BaseCustomer(IAddress i)
        //{
        //    AddressList = new List<IAddress> { i };
        //}
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public virtual decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public abstract List<IAddress> Addresses { get; set; }
        public List<IAddress> AddressList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ICustomer.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ICustomer.Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        decimal ICustomer.Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        decimal ICustomer.Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public decimal CalculateDiscount()
        {
            throw new NotImplementedException();
        }

        //public abstract decimal CalculateDiscount();

        List<IAddress> ICustomer.Addresses()
        {
            throw new NotImplementedException();
        }

        decimal ICustomer.CalculateDiscount()
        {
            throw new NotImplementedException();
        }
    }

    //Not necessary to write ICustomer here as Base Customer already implements it
    //However it is good from readability perspective
    public class Customer : ICustomer
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public decimal CalculateDiscount()
        {
            throw new NotImplementedException();
        }

        List<IAddress> ICustomer.Addresses()
        {
            throw new NotImplementedException();
        }

        public List<IAddress> Addresses { get; set; }
        public List<IAddress> AddressList { get; set; }

   
    }

    public class HomeAddressImpl : HomeAddress
    {
        public string Address1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string City { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Region { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PostalCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class OfficeAddressImpl : OfficeAddress
    {
        public string Address1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string City { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Region { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PostalCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class CustomerVIP : BaseCustomer, ICustomer
    {
        public override List<IAddress> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class EnquiryCustomer: BaseCustomer, ICustomer
    {
        //Wrong implementation
        public override decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override List<IAddress> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class EnquiryNew : IEnquiry
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class NormalCustomer2 : BaseCustomer, ICustomer
    {
        public override List<IAddress> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
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
        public List<IAddress> AddressList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
        public List<IAddress> AddressList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
        public string DiscountType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public decimal Calculate(ICustomer customer)
        {
            if (DiscountType == "N")
            {
                return 0;
            }
            else if (DiscountType == "Age") {

                return 10;
            }

            throw new NotImplementedException();
        }
    }

    class NormalDiscount : IDiscount
    {
        public string DiscountType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public decimal Calculate(ICustomer customer)
        {
            return 0;
        }
    }

    class AgeDiscount : IDiscount
    {

        public decimal Calculate(ICustomer customer)
        {
            if(customer != null && int.Parse(customer.Age) > 60)
            {
                return 10;
            }
            return 0;   
        }
    }

    class DiscountRegion : IDiscount
    {
        public decimal Calculate(ICustomer customer)
        {
            throw new NotImplementedException();
        }
    }

    class DiscountAmount : IDiscount
    {
        public decimal Calculate(ICustomer customer)
        {
            throw new NotImplementedException();
        }
    }


    //Wrong -Not job of customer to calculate discount
    public class WeekendCustomer : BaseCustomer, ICustomer
    {
        //public override decimal CalculateDiscount()
        //{
        //    //If customer comes on weekend, discount is more
        //    throw new NotImplementedException();
        //}
        public override List<IAddress> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class TestCustomer : BaseCustomer, ICustomer
    {
        public override List<IAddress> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    //public class Repository : IRepository
    //{
    //    public void Add()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Read()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class Reporting : IRepositoryRead
    {
        public void Read()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomerRating
    {
        public int Rating { get; set; }
    }

    public class Supplier : ISupplier
    {
        public List<IAddress> Addresses()
        {
            throw new NotImplementedException();
        }
    }


}
