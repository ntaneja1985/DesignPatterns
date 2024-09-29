using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    #region Base Classes
    public interface ICommand
    {
        Guid Id { get; set; }
    }
    public interface ICommandHandler<T> where T : ICommand
    {
        bool Execute(T command); 
    }

    public interface IQuery<T> where T : class
    {

    }

    public interface IEvent
    {
        Guid Id { get; set; }

    #endregion

    #region ConcreteClasses
    public abstract class BaseCommand<T> : ICommand where T : class
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

 
    public class  CreateCustomerCommand: BaseCommand<Customer>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Amount { get; set; }

    }

    //Mediator
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand> 
    {
        //All repositories must be binded to the same transaction
        IRepository<Customer> rep = new RepositoryCustomer();
        IRepository<AuditCustomer> AuditRep = null;
        EventSource eventSource = new EventSource();
       
        public bool Execute(CreateCustomerCommand command)
        {
            //Object mapper to convert command object to model object
            var aggregate = new AggregateCustomer(command.Name, command.Amount);
            rep.Save(aggregate.GetCustomer());
            AuditRep.Save(aggregate.GetAuditCustomer());
            eventSource.Events.Add(new CustomerCreated() { Name = command.Name });
            //repo.save
            //event sourcing
            //send events to other microservices using service bus
            return true;
        }
    }

    public class DeleteCustomerCommand : BaseCommand<Customer>
    {
        public int CustId { get; set; }
    }

    //Denormalized Data
    public class ReadCustomer : CustomerRating, IQuery<Customer>
    {
        public ReadCustomer()
        {

        }

    }
    #endregion
}
