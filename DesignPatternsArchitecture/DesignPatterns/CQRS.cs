using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface ICommand<T> where T : class
    {

    }

    public interface IQuery<T> where T: class
    {

    }
    public class  CreateCustomer: Customer, ICommand<Customer> 
    {
        public CreateCustomer()
        {
            
        }

    }

    public class UpdateCustomer : Customer, ICommand<Customer>
    {
        public UpdateCustomer()
        {

        }

    }

    //Denormalized Data
    public class ReadCustomer : CustomerRating, IQuery<Customer>
    {
        public ReadCustomer()
        {

        }

    }




}
