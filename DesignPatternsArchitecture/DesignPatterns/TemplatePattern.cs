using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface IDb
    {
        void Execute();
        //void Open();
        //void Close();
        //void ExecuteQuery();
    }

    internal abstract class AbstractDb : IDb
    {
        internal void Close()
        {
            //Do something to close the connection
        }

        public void Execute()
        {
            //structure of the algorithm
            Open();
            ExecuteQuery();
            Close();
        }

        protected abstract void ExecuteQuery();


        internal void Open()
        {
            //Do something to opne the connection
        }

    }

    internal class CustomerDb: AbstractDb
    {
        protected override void ExecuteQuery()
        {
            throw new NotImplementedException();    
        }
    }
}
