using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //internal class Abstraction
    //{
        interface ICustomer
        {
            string Name { get; set; }
            string Age { get; set; }
            decimal Amount { get; set; }

            decimal Rating { get; set; }

            //Wrong, not job of customer to calculate discount. Violates SRP
            decimal CalculateDiscount();
        List<IAddress> AddressList { get; set; }    
        List<IAddress> Addresses();
        }
        interface IAddress2
        {

        }
        interface HomeAddress: IAddress
        {

        }

        interface OfficeAddress: IAddress
        {

        }

    interface IProduct
        {

        }

    //Example of Aggregation, list of IAddresses can be used with Supplier or Customer, there is no exclusive ownership
    interface ISupplier
    {
        List<IAddress> Addresses();

    }

    public interface IAddress
    {
        string Address1 { get; set; }
        string City { get; set; }
        string Region { get; set; }
        string PostalCode { get; set; }
        string Country { get; set; }
        string Phone { get; set; }
    }

        interface IRating
        {
        public decimal getRating(ICustomer customer);
        }
         interface IDiscount
        {
            //string DiscountType { get;set; }
           public decimal Calculate(ICustomer customer);

        }

    interface IEnquiry
    {
        string Name { get; set; }
        string Age { get; set; }
    }

    interface IRepository: IRepositoryRead
    {
        void Add();
        void Update();
        void Delete();
    }

    interface IRepositoryRead
    {
        void Read();
    }

    public interface IRepository<T>
    {
        void Save (T entity);
    }
}
