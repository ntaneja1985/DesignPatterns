using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal static class SimpleFactory
    {
        public static IDiscount CreateDiscount(int id)
        {
            if (id == 0)
            {
                return new DiscountAmount();
            }
            else if (id == 1)
            {
                return new DiscountRegion();
            }
            else if (id == 2) 
            {
                return new AgeDiscount();
            }

            return new NormalDiscount();
        }

        public static ICustomer CreateCustomer(int type)
        {
            if (type == 0)
            {
                var homeAddress = new HomeAddressImpl();
                homeAddress.City = "Chandigarh";
                Customer customer = new Customer
                {
                    AddressList = new List<IAddress>() { homeAddress }
                };

            }
            else if (type == 1) 
            {
                var officeAddress = new OfficeAddressImpl();
                officeAddress.City = "Delhi";
                Customer customer = new Customer
                {
                    AddressList = new List<IAddress>() { officeAddress }
                };

            }
            else if (type == 2)
            {
                var officeAddress = new OfficeAddressImpl();
                officeAddress.City = "Delhi";
                CustomerVIP customer = new CustomerVIP
                {
                    AddressList = new List<IAddress>() { officeAddress }
                };

            }

            return new NormalCustomer2();
        }
    }
}
