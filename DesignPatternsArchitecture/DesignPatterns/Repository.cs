using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface IRepository<T> where T : class
    {
        //Add to memory
        bool Add(T entity);    
        //Save to database
        bool Save(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(int id);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T: class
    {
        public List<T> list { get; set; } = new List<T>();

        public bool Add(T entity)
        {
            list.Add(entity);
            return true;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract bool Save(T obj);

        public abstract IEnumerable<T> Search(int id);
        
    }
    public class RepositoryCustomer : RepositoryBase<Customer>
    {
        public override IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public override bool Save(Customer obj)
        {
            //code for ef core to save to DB
            return true;
        }

        public override IEnumerable<Customer> Search(int id)
        {
            throw new NotImplementedException();
        }
    }


    public static class FactoryRepository<T> where T: class
    {
        public static IRepository<T> Create()
        {
            if(typeof(T).ToString()=="Customer")
            {
                return (IRepository<T>)new RepositoryCustomer();
            }
            return null;
        }
    }
}
