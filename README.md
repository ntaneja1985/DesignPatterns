# DesignPatterns
Design Patterns

**Human Beings understand things using classification**
Eric Evans wrote the popular book on Domain Driven Design

- Understand Object Oriented Programming(Helps us to identify entities)
- Once entities are identified, we can start classifying them
- Identity IS A and HAS A relationship beteen entity abstraction
- Abstraction is the process where we identity the entity and what are the important things required for that entity

**Abstraction in code is done through interfaces**

- Nouns become the entity

### Ensure that abstraction is respected. 

- Respect Abstraction by using Encapsulation. In the implementation part, ensure that only the interface methods being implemented
 public. All other methods are internal to the class and hence, must be private.

 **Abstraction is the planning phase**
 It is usually done by writing interfaces

 **Implementation is done by following abstraction using Encapsulation**
 It utilizes private, public, protected

 **Polymorphism is one name many forms**

 ```c#
 ICustomer cust = new NormalCustomer();
cust = new CustomerAgeType(); 
```

### Rather than dealing with concrete classes, we deal with interfaces

We must be careful in creating abstractions. It may cause **Liskov problem**. It looks like a duck, quacks like a duck, but it needs batteries

```c#
ICustomer cust = new EnquiryCustomer();

```

## Aggregation, Composition and Association

### Example of Association:
```c#
//Through constructor usually
 //Example of Association, there is only a thin relationship between classes
         public NormalCustomer(IRating rating)
        {
            Rating = rating.getRating(this);
        }
```

### Example of Aggregation:

```c#
//Example of Aggregation, list of IAddresses can be used with Supplier or Customer, there is no exclusive ownership. Address can be shared across multiple classes
    interface ISupplier
    {
        List<IAddress> Addresses();

    }

```

### Example of Composition

```c#
//Example of composition, there is tight relationship between objects. Here Discount depends on Customer
interface IDiscount
        {
            decimal Calculate(ICustomer customer);

        }
//Here this keyword is Normal Customer which implements ICustomer
if (IsAmountValid())
            {
                IDiscount discount = new Discount();
                return discount.Calculate(this);
            }



```

- Compile Time Polymorphism and Run Time Polymorphism
- Polymorphism is the ability of an object to take different shapes and forms
- Generic Class is not polymorphism
- Goal of Polymorphism is decoupling
- object o = ""
- object o = 123
- ICustomer cust = new Customer()
- ICustomer cust = new EnquiryCustomer()
- In Association we dont have instance references(usually through constructor)
- In Composition both the classes either they live together or die together(exclusive)
- In Aggregation there is no exclusive relationship between classes


***Move all the common methods across classes into an abstract class and then implement the abstract class***
Use inheritance and abstract classes with same family to share common logic
```c#
//Move all common methods across classes into BaseClass
    public abstract class BaseCustomer : ICustomer
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public abstract decimal CalculateDiscount();

        List<IAddress> ICustomer.Addresses()
        {
            throw new NotImplementedException();
        }
    }

    //Inherit from Base Customer and implement ICustomer
    class NormalCustomer2 : BaseCustomer, ICustomer
    {
        public override decimal CalculateDiscount()
        {
            throw new NotImplementedException();
        }
    }
```
- Consume in Client using Object Poloymorphism(ICustomer can be implemented by NormalCustomer or EnquiryCustomer)
- Any wrong abstraction identified during implementation, we should go and rework on our interfaces


# SOLID PRINCIPLES

- Idea behind solid principles is that if we make change at one place, we should not be making changes at multiple places
- Main goal: less impact analysis, change at one place doesnot impact others e.g Microservices

## S-->SRP (Single Responsibility Principle)
- Each class has only a single responsibility

Wrong implementation and wrong abstraction. It is not job of Customer class to calculate discounts
Customer class is getting overloaded
```c#
public class AgeCustomer : BaseCustomer, ICustomer
    {
        public override decimal CalculateDiscount()
        {
            //If comes is greater than 60 years of age, give more discount
            throw new NotImplementedException();
        }
    }
```

Correct Implementation-Calculate Discount separately
```c#
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
```


## O -> Open Closed Principle

- If there is code running out there, dont change it
- We extend it.
- Open for Extension and closed for Modification
- Create different types

Dont do this
```c#
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
```

Instead of modifying the original interface, create a new implementation
Do this instead:
```c#
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
```

## Liskov Subsitution Principle
- Child Class should be able to replace the Parent Class seamlessly.
- It is used to solve the problem of wrong abstraction
- You cannot remove some property from inheritance
- Cannot do hacks like this also as it can cause issues
```c#
\\Modify the base class
        public virtual decimal Amount {  }

\\Implement in child class like this(wrong implementation)
public class EnquiryCustomer: BaseCustomer, ICustomer
    {
        //Wrong implementation
        public override decimal Amount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

\\Solution is to create a new interface
interface IEnquiry
    {
        string Name { get; set; }
        string Age { get; set; }
    }

\\Create a class that implements IEnquiry
  public class EnquiryNew : IEnquiry
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

```


### In Liskov, the child class should be able to substitute the parent class without any sideeffects.

Another example would be the Shape-Circle-Rectangle problem

```c#

public abstract class Shape
{
    public abstract double Area();
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double Area()
    {
        return Width * Height;
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}

\\Use it like this:
static void Main()
{
    Shape rectangle = new Rectangle { Width = 5, Height = 3 };
    Shape circle = new Circle { Radius = 2 };

    Console.WriteLine($"Rectangle area: {rectangle.Area()}");
    Console.WriteLine($"Circle area: {circle.Area()}");
}

```

In this example:

- We create instances of Rectangle and Circle.
- We treat them both as Shape objects.
- The LSP ensures that substituting a Rectangle or a Circle for a Shape doesn't break the program. The behavior (calculating the area) remains consistent.
- Remember, adhering to the LSP helps maintain a robust and predictable class hierarchy, making it easier to reason about your code and preventing unexpected surprises when swapping objects.

## I --> Interface Segregation Principle
- Follows from OCP Principle. 
- Donot force the consumer to use an interface or methods which it doesnot need
- Break down your interface methods across different interfaces

```c#
//We have the following interface
interface IRepository
    {
        void Add();
        void Update();
        void Delete();
        void Read();
    }

 //Implementation is like this
public class Repository : IRepository
    {
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }

```
Suppose we have a Reporting Class that doesnot need all methods like Add or Update. It only needs Read
In the above, we are forcing it to do it.

### Solution
- Split your interfaces

```c#
interface IRepositoryRead
    {
        void Read();
    }
  interface IRepository: IRepositoryRead
    {
        void Add();
        void Update();
        void Delete();
    }

//Please note implementation remain the same as
public class Repository : IRepository
    {
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }

// Now inide the main method we can do this
class Program 
{
    static void Main(string[] args)
    {
        IRepositoryRead read = new Repository();
        read.Read();
    }
}

```

## D --> Dependency Inversion Principle

- High level modules should not depend on low level modules. They should rely on Abstractions such as interface or abstract classes
- Encourages loose coupling and makes code easy to understand

```c#
namespace EmployeeManagement
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
    }
}

namespace EmployeeManagement
{
    public class EmployeeDataAccessLogic
    {
        public Employee GetEmployeeById(int id)
        {
            // Logic to fetch employee data from the database
            // ...
        }
    }
}


```

Problem above is that EmployeeDataAccessLogic knows about the database details directly. 


If we change the database provider or modify the data access logic, it will impact the Employee class as well.

```c#
namespace EmployeeManagement
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
    }

    public class DatabaseEmployeeRepository : IEmployeeRepository
    {
        public Employee GetEmployeeById(int id)
        {
            // Logic to fetch employee data from the database
            // ...
        }
    }
}


```

Now, our EmployeeDataAccessLogic class depends on the abstraction (IEmployeeRepository), not the concrete implementation. 

We can easily switch to a different data source (e.g., an API or a file) without affecting other parts of the system.

```c#
namespace EmployeeManagement
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Other methods related to employee management
        // ...
    }
}

```

By following DIP, we've achieved better separation of concerns and improved testability. Plus, we're ready to adapt to changes in data access without breaking our entire system.

Remember, DIP isn't just about interfaces; it's about designing your system to depend on abstractions rather than concrete detail

### Now in latest version of C#, we can write concrete methods in Interfaces, but it is a hack. Earlier people used to create Abstract classes for this purpose. Any common logic between classes can be implemented in Abstract class

- In Factory pattern, consumer doesnot create concrete implementation. It depends on IFactory

- Interfaces is very uesful if we have complex classification of families.
- SOA is an architectural style, Event Sourcing is a microservices design pattern
- If there is no common logic between classes, then abstract class is not really required. However utilizing interfaces is a good practice.
- Dont create abstract classes first. Usually they are made while refactoring.Also called Late architecting

***SOLID is all about decoupling. How one class can be decoupled from the other class***

# Simple Factory Pattern

- Part of GoF patterns
- Creational design pattern that provides an interface for creating objects in a way that allows subclasses to decide which class to instantiate.

```c#
//Tight coupling
IDiscount discount = new AgeDiscount();

//Better Option
//Caller doesnot create instance, we delegate it to someone
IDiscount discount = Someone.Create(1);
```

- Think of it this way, imagine a factory that takes the same input of wood and creates table,chair,sofa,bed etc.
- It's like having a customizable assembly line for creating objects
- Centralizes Object Creation

```c#
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
    }

\\Inside Main Method use it like this
IDiscount discount2 = SimpleFactory.CreateDiscount(2);
discount2.Calculate(cust);

```

***Centralize our object creation(Simple Factory Pattern) and refer to abstraction in our client***

## IOC: Inversion of Control
- Earlier when we were doing new Discount(), the main method was incharge of creating the discount object. Now we have inverted this control of object creation to the SimpleFactory.
- Inversion of Control is a thought process or principle, which is any kind of unnecessary work give it someone else and you focus on your work
- Not the job of main method() to create object instances. Invert the control i.e give the control to Simple Factory
- SRP is also an example of Inversion of Control
- IOC can be implemented by Factory Pattern, SRP, events or delegates
- IDiscount doesnt have much aggregation. Its simple
- Consider ICustomer interface which has relationship with IAddress
- Then we have to create a customer inside a factory class with different types of IAddresses
- What if ICustomer has a dependency on IPhoneType whether it is office or home phone
- We will have to create different permutations and combinations of the customer inside a factory

```c#
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

```

***To simplify it we use Dependency Injection***

- Lot of developers use Constructor Injection either Property Based or Method Based Injection
- Inject dependent objects from outside

```c#
//Pass the context into the constructor
public BaseCustomer(IAddress i)
        {
            AddressList = new List<IAddress> { i };
        }
```

***Dependency Injection(not a pattern) is a style where dependent objects are injected through constructor or property***
- Main Class points to an abstraction

***Dependency Injection implements Dependency Inversion***

- Service Locator is similar to simple factory pattern
- It is concerned with Networking where a call to WCF service is made(API call across the internet)
- Used like this: DBClientFactory<WorkHeadRepo>.Instance.getWorkOrderYear(appSettings.Value.DefaultConnection)
- Isnt it similar to SimpleFactory.Create(1)

### Domain Driven Development

- Emphasizes Object Oriented Programming(Think like real world objects)
- Follow the Domain (model the real world domain and keep same names or ubiquitous language)
- Conceptualized by Eric Evans
- DDD says there are 3 kinds of classes: Entity Class(Customer, Employee, Supplier), Service(or Technical) Class(Data Access Layer, Repositories,WebApi, Caching, Validation), Value Object Class(Date,Money)
- Service classes are common for all entities and are cross-cutting
- Value Object classes are neither Services or Entities like Date Class, Money class
- Related to Onion Architecture where everything depends on the entity

```c#
Date d = new Date("1/1/2010");
Date d1 = new Date("January 1 2010");
d = d1;

Money m = new Money(D,1);
Money m1 = new Money(Rs,75);
m == m1
```
- Value objects are rarely created, compare based on value. They are not modified once they are created
- Everyone should use DDD
- Value objects we need to code something extra
- Equals(), ===,!== should be overidden
- GetHashCode() should also be overidden
- When objects are stored in a Dictionary, they are looked up using a hash
- Every object is assigned a different hashcode(obj.GetHashCode())
- We need to override the GetHashCode() method and generate hashcode based on the amount only

```c#
Customer c = new Customer();
c.Name = "Nishant";
Customer c2 = new Customer();
c2.Name = "Nishant";

Console.WriteLine(c == c1);//Returns false
//Nishant can be from Delhi or he can be from Chandigarh
//They are different Entity Objects


//Example of Value Object
public class CustMoney 
{
    public string PaymentType {get;set;}
    public decimal Amount {get;set;}

    //Override the Equals Method
    public override bool Equals(object obj)
    {
        return this.Amount == (CustMoney)obj.Amount;
    }

    //Alternatively Use operator overloading
    public static bool operator ==(CustMoney objA, CustMoney objA)
    {
        return objA.Amount == objB.Amount;
    }

    public static bool operator !=(CustMoney objA, CustMoney objA)
    {
        return objA.Amount != objB.Amount;
    }

    //Dont use whole object to generate hashcode, just use the Amount
    public override int GetHashCode
    {
        return this.Amount.HashCode;
    }
}

CustMoney m1 = new CustMoney();
m1.Amount = 100;
m1.PaymentType = "Cash"

CustMoney m2 = new CustMoney();
m2.Amount = 100;
m2.PaymentType = "Credit Card"

Console.WriteLine(m1.Equals(m2));//Returns true since we are comparing by Amount

Dictionary<CustMoney,CustMoney> custMoneyList = new Dictionary<CustMoney,CustMoney>();
custMoneyList.Add(m1,m1);

//since m1 and m2 are equal we can lookup m1 using m2
//In reality we get an exception
//If we make them equal by hashcode based on amount, it will work fine
//Leads to a question, if the developer passes any number it will work, so it is not so smart after all.
//Can cause hash collisions
var t = custMoneyList[m2];
```


# Aggregate Root (Design Pattern): Helps us to maintain integrity of the object by going through the root(or parent object)

```c#

interface ICustomer 
{
    string Name {get;set;}
    List<IAddress> Addresses {get;set;}
}

public class Customer : ICustomer
{
    public List<IAddress> Addresses {get;set;}

    public string Name {get;set;}
}

static void Main(string[] args)
{
    Customer c = new Customer();
    c.Addresses.Add(new HomeAddress());
    c.Addresses.Add(new OfficeAddress());
}

```

- In the below example, dont allow adding Addresses directly by exposing List<IAddresses>()
- Add the Address through the Aggregate Root
- Aggregate root = gatekeeper of the aggregate, ensuring order, consistency, and integrity.
- Helps to main integrity in the Entity Object
- Main Drawback is Customer Class gets overloaded.
- Modify the above code as follows:

```c#
interface ICustomer 
{
    string Name {get;set;}
    void Add(IAddress address);
}

public class Customer : ICustomer
{
    private List<IAddress> Addresses {get;set;}

    public string Name {get;set;}

    public void Add(IAddress address)
    {
        foreach(var item in Addresses)
        {
            if(item.GetType()==typeof(address))
            {
                throw new Exception("Type of Address already exists");
            }
            else 
            {
                this.Addresses.Add(address);
            }
        }
    }
}

```

***One way to decide whether it is a design pattern or architecture style, there should be some pseudo code***

### Structs in C# compare based on value. Can we use Structs in place of Value Object classes.
- Some limitations like EF Support, Inheritance force us to create a class
- if it is a simple comparison, we can use structs but for complex objects, struct will not work.

## Bounded Context
- A bounded context is like a fenced-off area within your software where specific terms and rules apply consistently. It's where a shared understanding the Ubiquitous Language exists. Inside this boundary, everyone agrees on what things mean.
- Outside the boundary, in another bounded context, the same terms might take on different meanings. Think of it as having different dictionaries for different parts of your application.
- Bounded contexts allow us to:
    1. Keep things organized: Each context focuses on a specific domain or subdomain (e.g., orders, payments, customers).
    2. Avoid misunderstandings: Inside a context, terms mean the same thing to everyone.
    3. Encourage loose coupling: By keeping contexts separate, we prevent unnecessary dependencies.


## Context Map

1. The context map is where the magic happens. 
2. It's a visual representation of how these bounded contexts interact with each other. Think of it as a subway map connecting different lines (contexts). Some lines intersect, some run parallel, and some are express trains hurtling through the night. The context map ensures they all play nice and have the right contact points.
3. Shared Kernel: Sometimes, two contexts need to hold hands and share a little code. It's like when Batman teams up with Superman—except with fewer capes. The shared kernel allows them to collaborate without stepping on each other's toes.
4. Customer-Supplier Relationship: One context might provide services to another. It's like the local bakery supplying fresh baguettes to the café next door. In DDD, we call this a customer-supplier relationship. No invoices involved, though.
5. Conformist: Some contexts are rule followers—they conform to the rules set by another context. It's like a teenager reluctantly following curfew because their parents said so. These conformist contexts adapt to maintain harmony.
6. Anticorruption Layer: Imagine a translator at the United Nations. The anticorruption layer ensures that when Context A speaks Klingon, Context B understands it in plain English. It shields your precious domain model from foreign invaders.
7. Open Host Service: This is like throwing a neighborhood block party. One context opens up its services to others. It's all about sharing the love (and endpoints).
8. Published Language: When contexts need to gossip, they use a common dictionary—the published language. It's like having a secret codebook for spies. 

# Iterator Pattern

- Whenever we' re dealing with collections and need to iterate through their elements sequentially, consider the Iterator Design Pattern.
- It's especially handy when you want to keep your client code decoupled from the specifics of the collection implementation

```c#

var employees = new List<Employee>
{
    new Employee("Alice"),
    new Employee("Bob"),
    new Employee("Charlie")
    // ... more employees
};

foreach (var employee in employees)
{
    Console.WriteLine($"Employee: {employee.Name}");
}


```

- Behind the scenes, the foreach loop uses an iterator to access each employee sequentially. But as a developer, you don't need to worry about how it's happening; you just focus on the high-level logic
- The Iterator Design Pattern allows sequential access to the elements of an aggregate object (i.e., a collection) without exposing how those elements are stored internally
- It provides a uniform interface for traversing different data structures. So whether you're dealing with a List, an ArrayList, or an Array, you can use the same approach to iterate through their elements
- The main idea is to separate the iteration logic from the collection object itself.
- IEnumerable<T> helps us to iterate through a collection. Internally it uses IEnumerator.
- IEnumerable<T> doesnot have an add method, it only helps us to iterate over an object, helps us to maintain the sanctity of the object


## If we have an aggregate root, we will always have iterator pattern attached to it. Clone for maintaining integrity.
- Say if Customer Class is an aggregate root, it will implement iterator pattern to loop through the list of addresses.

```c#

public class Customer 
{
    public IEnumerable<Address> Addresses 
    {
        get 
        {
            return _addresses;
        }
    }
}

public class Breaker 
{
    public void BreakIt()
    {
        var cust = new Customer();
        //This is valid code but it should not be happening
        var addresses = (List<Address>) customer.Addresses;
        addresses.Add(new HomeAddress());
        addresses.Add(new HomeAddress());
    }

}

//To fix the Breaker add a method Add() to Customer

public void Add(IAddress addr)
{
    foreach(var item in this.Addresses)
    {
        if(addr.GetType()==typeof(HomeAddress))
        {
            throw new Exception("Not Allowed");
        }
    }
    this.Addresses.Add(addr);
}


```

## Please note Iterator pattern is an overkill for basic collections like Arrays and Lists
- It only supports forward traversal of elements rather than backend traversal or random access
- Iterator also impact performance

## Biggest advantage of iterator pattern is adaptability and flexibility
- Clients (that's you, the developer) can traverse collections without caring about the nitty-gritty details. 
- You don't need to know whether it's an array, a linked list, or a mystical data structure from the future. Just grab that iterator and start exploring!

### Aggregate root makes our code complex. It overloads the root object.
- For iterating through aggregate root, we need iterator pattern. Clone it for maintaining integrity.

## Anti-corruption layer is used with legacy applications
- Used for adding new features into legacy applications
- How to share data between services: shared class, web api, queue

# Bridge Pattern

- The Bridge Design Pattern is all about decoupling an abstraction from its implementation. 
- Abstraction means interfaces and implementation is a class that implements the interface
- Uses encapsulation and aggregation and can use inheritance to separate responsibilities into different classes
- Imagine you have this fancy abstraction (let's call it "Abstractionville") that wants to do some cool stuff, but it doesn't want to be tied down to a specific implementation. It's like Abstractionville wants to date around without committing to anyone—very modern, I must say!

- Bridge splits things into two parts:

1. Abstraction: This is the high-level stuff—the part that Abstractionville interacts with. It's like the front-end of your app, blissfully unaware of the nitty-gritty details.
2. Implementation: This is where the real action happens—the back-end. It's like the engine room of a spaceship. The Implementation part handles the actual work, but it doesn't care about Abstractionville's drama. It's stoic like that.

```c#

// Abstraction
public abstract class MessageSender
{
    public abstract void SendMessage(string message);
}

// Implementers
public class EmailSender : MessageSender
{
    public override void SendMessage(string message)
    {
        // Send email logic here
        Console.WriteLine($"Sending email: {message}");
    }
}

public class SMSSender : MessageSender
{
    public override void SendMessage(string message)
    {
        // Send SMS logic here
        Console.WriteLine($"Sending SMS: {message}");
    }
}

// Client code
var emailSender = new EmailSender();
var smsSender = new SMSSender();

emailSender.SendMessage("Hello, world!");
smsSender.SendMessage("Hey there, Copilot!");

// Abstraction and Implementation are happily decoupled!


```

### When to use Bridge Pattern
-When to Use It: Whenever you find yourself juggling different implementations for the same abstraction—like choosing between pizza toppings without committing to just one. 

### Abstraction is also like our interface: ICustomer
- For example, we need to validate our customer differently in different scenarios
- Solution is to create a new interface IValidateCustomer and with a Validate() method
- Now create classes that implement IValidateCustomer and implement the Validate method
- Extension of Single Responsibility Pattern(SRP)

```c#
Customer c = new Customer();
IValidateCustomer valid = new ValidationWithName();
valid.Validate(c);

```

Basic example of Aggregation,Composition and Association
- Nishant has a shirt (they can exist independently)-->Aggregation
- Nishant has a heart (they need each other for existence) -->Composition
- Nishant knows his neighbours(there is a thin relationship) -->Association(usually through constructor)

# Repository Pattern

- In DDD, we have Entity Classes, Service Classes and Value Objects
- Primary purpose is to abstract and encapsulate the data access layer
- Provides clean separation between business logic and the underlying data storage

Before we explore the pattern, let's understand the problem it aims to solve. Imagine you're building a modern data driven application that needs to access data from a database (like SQL Server). The straightforward approach would be to write all the data access-related code directly within your application's controllers or services. For instance, if you're using Entity Framework, your controller might directly interact with the data context class and execute queries against the database.

However, this approach has drawbacks:

1. Code Duplication: If you have multiple controllers or services that manipulate the same data (e.g., an Employee entity), you’ll end up duplicating the data access code. Any changes to this logic would require updates in multiple places.
2. Tight Coupling: Embedding data access logic directly in controllers tightly couples them to the database implementation. This makes your code less maintainable and harder to test.

## Implementation of Repository Pattern
```c#

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

    //We can use the same factory to create a customer or supplier
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


    //Implementation in Program.cs

    Customer c = new Customer();
    IRepository<Customer> rep = FactoryRepository<Customer>.Create();
    rep.Add(c);
    rep.Save(c);

    //Can do similar for supplier
    Supplier supplier = new Supplier();
    IRepository<Supplier> sup = FactoryRepository<Supplier>.Create();
    sup.Add(supplier);
    sup.Save(supplier);

```
- Injection of DI is static in nature
- We can do DI injection using constructor injection
- Factory pattern can provide instance based on service locator, dropdown selection or any other condition
- Generics help to decouple datatype from logic

```c#
public bool Compare(int num1, int num2) 
        {
            return num1 == num2;
        
        }

        public bool Compare(string str1, string str2)
        {
            return str1 == str2;
        }

        public bool Compare<T>(T obj1, T obj2)
        {
            return obj1.Equals(obj2);
        }

```

# Adapter Pattern
- Adapter Design Pattern is a structural pattern that allows objects with incompatible interfaces to work together. 
- Imagine you have two systems or classes—let's call them System A and System B. They need to collaborate, but their interfaces don't match. The adapter acts as a bridge between them, making communication possible.
- It acts like a wrapper
- Adapter pattern is a wrapper on top of objects or classes that cannot be modified: they can be third party or internal objects and classes
- 2 types of adapters: Object Adapter and Class Adapter
- Most used one is the Object Adapter
- In ADO.NET, we use Repository Pattern which internally used Adapter Pattern
- Lets say we have a Save() method in Repository, we now need to make it work with EFCore and ADO.NET
- ADO.NET may use a different method to Save() like cmd.ExecuteNonQuery()
- We then make a adapter inside our IRepository for Save() method and internally call cmd.ExecuteNonQuery() in its implementation
- Adapter handles the necessary transformations and ensures compatibility

```c#
public interface IExport
    {
        void Export();
    }
    public class WordExport : IExport
    {
        public void Export()
        {
            throw new NotImplementedException();
        }
    }
    public class ExcelExport : IExport
    {
        public void Export()
        {
            throw new NotImplementedException();
        }
    }


    //Third Party DLL
    //Incompatible with IExport interface
    public class PdfExport
    {
        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    //We need to make PdfExport work with IExport interface
    //Object Adapter
    //Make a call to instance of Pdf Export class
    //Wrapper
    public class PdfObjectAdapter : IExport
    {
        //Internally it calls save
        public void Export()
        {
            PdfExport c = new PdfExport();
            c.Save();
        }
    }

    //Class Adapter using inheritance
    public class PdfClassAdapter : PdfExport, IExport
    {
        public void Export()
        {
            this.Save();
        }
    }


    //Usage

    IExport e = new ExcelExport();
    e.Export();
    e = new WordExport();
    e.Export();
    //Use the Pdf Adapter using object adapter
    IExport e2 = new PdfObjectAdapter();
    e2.Export();
    //Use the Pdf Adapter using class adapter
    IExport e3 = new PdfClassAdapter();
    e3.Export();

```

# CQRS: Command Query Responsibility Segregation
- Divide your classes into 2: Create/Update/Delete Classes and Query Classes
- Helps to divide the normalized data from denormalized data
- Establish bounded context, CreateCustomer() is different from GetCustomer()
- When we design interfaces we run into situations that we need certain fields during the READ part and not necessarily during the INSERT/UPDATE part.
- For example, we may want to lookup number of visits of customer(computed value) during the read part, but in write part we may not need it
- This can make our interface and inheriting classes bulky and difficult to maintain
- One Solution is to make DTOs i.e we make a new class which only contains the fields we wish to read
- Sticking to reusability too much can lead to complicated classes which violate SRP principle
- Solution is CQRS, separate out the classes meanT for reporting(QUERY) and those required for INSERT/UPDATE/DELETE
- CQRS is a design pattern
- CQRS is a good pattern for doing transactions between systems-->makes it valuable for designing microservices
- Please note Model and Command class is not the same
- Command is an action like insert/update/delete
- On the other hand Model represents a real world entity like Customer or Supplier
- Along with a command we also have a handler
- Handler is like a mediator
- Base of CQRS comes from few GoF patterns
- One is Command Pattern

```c#
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
    //Mediator
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand> 
    { 
        public bool Execute(CreateCustomerCommand command)
        {
            //repo.save
            //event sourcing
            //send events to other microservices using service bus
            return true;
        }
    }

```

## Command Design Pattern
- Actions can also be classes

```c#
// Command interface
public interface ICommand
{
    void Execute();
}

// Concrete command: PrepareDishCommand
public class PrepareDishCommand : ICommand
{
    private readonly Chef _chef;

    public PrepareDishCommand(Chef chef)
    {
        _chef = chef;
    }

    public void Execute()
    {
        _chef.PrepareDish();
    }
}

// Concrete command: ServeCustomerCommand
public class ServeCustomerCommand : ICommand
{
    private readonly Waiter _waiter;

    public ServeCustomerCommand(Waiter waiter)
    {
        _waiter = waiter;
    }

    public void Execute()
    {
        _waiter.ServeCustomer();
    }
}

// Receiver: Chef
public class Chef
{
    public void PrepareDish()
    {
        Console.WriteLine("Chef is preparing a delicious dish.");
    }
}

// Receiver: Waiter
public class Waiter
{
    public void ServeCustomer()
    {
        Console.WriteLine("Waiter is serving the customer.");
    }
}

// Invoker: Waiter
public class WaiterInvoker
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void ExecuteCommand()
    {
        _command.Execute();
    }
}

// Usage
var chef = new Chef();
var waiter = new Waiter();

var prepareDishCommand = new PrepareDishCommand(chef);
var serveCustomerCommand = new ServeCustomerCommand(waiter);

var waiterInvoker = new WaiterInvoker();
waiterInvoker.SetCommand(prepareDishCommand);
waiterInvoker.ExecuteCommand(); // Chef prepares dish

waiterInvoker.SetCommand(serveCustomerCommand);
waiterInvoker.ExecuteCommand(); // Waiter serves customer


```
***MediatR is a nuget package which creates mappings: for example for create customer command invoke the create customer handler***
- It is an orchestrator and handles mapping between command and its handler

## Mediator Pattern
- Behavioural Design Patterns
- Its primary goal is to reduce the complexity of communication between multiple objects by introducing a mediator: a central hub that handles interactions among these objects.
- Think of a Facebook group. Members don't directly message each other: they post in the group, and the group handles distribution. Similarly, in software, the mediator coordinates interactions.
- Mediator Class acts like a traffic police
- Lets say we have an accounting application, an inventory application and billing application. If we call class billing it needs to make an entry into accounting or inventory needs to talk to accounting and billing
- This interaction becomes complex very quickly
- So we introduce a mediator in-between them
- Register all the classes inside an mediator. For each of the classes, define a handler
- Mediator will invoke handlers of those classes which are called
- CQRS is a combination of command pattern and mediator pattern

## Facade Pattern
- Structural design pattern
- What if we have a complex system with several subsystems
- This pattern provides a simplified interface or facade for interacting with that complex system
- This is especially useful when we have a complex system with many interdependent classes
- Take the example of Home Theatre System
- A home theatre system has a DVD Player, a Projector, Sound System and Lights
- Rather than having the user interact with each of these system directly, we can provide a HomeTheatreFacade which will just expose 2 simple methods: Watch Movie and End Movie
- The WatchMovie() method will take case of the complex interactions such as starting the projector, dimming the lights, firing up the sound system etc.
- Same goes for EndMovie() method which will switch off the projector, switch the lights back on and turn off the sound system

## Facade Pattern is different from Adapter Pattern
- Adapter pattern is used when we have incompatible interfaces (remember pdfexport, word export example)
- Facade pattern is simply used to simplify the interaction with a complex subsystem
- Simplifies a subsystem
```c#
\\Components of the Home Theatre System
public class DVDPlayer
{
    public void On() => Console.WriteLine("DVD Player is On");
    public void Play(string movie) => Console.WriteLine($"Playing {movie}");
    public void Off() => Console.WriteLine("DVD Player is Off");
}

public class Projector
{
    public void On() => Console.WriteLine("Projector is On");
    public void Off() => Console.WriteLine("Projector is Off");
}

public class SoundSystem
{
    public void On() => Console.WriteLine("Sound System is On");
    public void SetVolume(int level) => Console.WriteLine($"Setting volume to {level}");
    public void Off() => Console.WriteLine("Sound System is Off");
}

public class Lights
{
    public void Dim(int level) => Console.WriteLine($"Dimming lights to {level}%");
}

\\Facade Class
public class HomeTheaterFacade
{
    private readonly DVDPlayer _dvdPlayer;
    private readonly Projector _projector;
    private readonly SoundSystem _soundSystem;
    private readonly Lights _lights;

    public HomeTheaterFacade(DVDPlayer dvdPlayer, Projector projector, SoundSystem soundSystem, Lights lights)
    {
        _dvdPlayer = dvdPlayer;
        _projector = projector;
        _soundSystem = soundSystem;
        _lights = lights;
    }

    public void WatchMovie(string movie)
    {
        Console.WriteLine("Get ready to watch a movie...");
        _lights.Dim(10);
        _projector.On();
        _soundSystem.On();
        _soundSystem.SetVolume(5);
        _dvdPlayer.On();
        _dvdPlayer.Play(movie);
    }

    public void EndMovie()
    {
        Console.WriteLine("Shutting movie theater down...");
        _dvdPlayer.Off();
        _soundSystem.Off();
        _projector.Off();
        _lights.Dim(100);
    }
}

\\Using the Facade
class Program
{
    static void Main(string[] args)
    {
        var dvdPlayer = new DVDPlayer();
        var projector = new Projector();
        var soundSystem = new SoundSystem();
        var lights = new Lights();

        var homeTheater = new HomeTheaterFacade(dvdPlayer, projector, soundSystem, lights);

        homeTheater.WatchMovie("Inception");
        Console.WriteLine("Press any key to end the movie...");
        Console.ReadKey();
        homeTheater.EndMovie();
    }
}


```

### Aggregate
- If we want to do a transaction where we first fetch the data and then update the data
- Use an aggregate class for this.
- Aggregate root means we modify through single route
- Aggregate root is a class itself. Many times a normal properly designed domain objects can also act as aggregate root.
- Aggregate root modifies the object through the root
- Command is different from the model
- We need Automapper to convert the command to the model and pass it to the repository
- Create Customer command first goes to the handler. 
- The handler goes to the aggregate root, gets the objects and saves it using the repository

### Event Sourcing
- Lets say that Create Customer command is executed, then an update customer, edit customer and delete customer
- Each of these commands correspond to events like creating a customer, updating a customer, editing a customer and deleting a customer
- Event Sourcing is a collection of events as they happen.
- Event Sourcing is a powerful pattern that captures all changes to an application's state as a sequence of events. This approach provides benefits like auditability, temporal queries, and improved scalability
- Critical concept used in Microservices
- Event sourcing has collection of events of what happened when the command fired.
- Aggregate root is the collection of objects needed during that command and the contained objects should be modified through a single root.
- Event Sourcing, Aggregate root are compulsory for microservices 
- Projection is the process of converting a model to another structure usually using Automapper.
- Event Source can be RabbitMq or Azure Service Bus.

### CQRS Pattern the nitty gritties
- We fire a command usually through a POST request
- Lets say CreateCustomer() post request fires the Create Customer Command
- Each command has a handler associated with it
- Each handler interacts with several other entities: For e.g it might connect to Aggregate to get the customer details
- It also talks to the repository to save the data in the database
- We would ideally want a solution where rather than calling the handler directly we associate a handler with the command itself. Enter:MediatR
- There can be a case where the handler calls another microservice and passes along some data to it
- What if there is a failure, then how do we reverse it?
- Our applications must be resilient, must be able to handle failures
- We must ensure eventual consistency. We first must create a customer, then send an event to a queue to update this particular customer details everywhere it used and read.
- Must follow optimistic concurrency.
- All inserts must happen in the local database. When this insert is done, send it to the queue. If there is any problem, there should be a retry mechanism in place.
- Command Handler is different from Facade Pattern

# Simple Factory Pattern
- Centralizes object creation (Container)

# Creational Pattern: Factory Method Pattern
- Factory method design pattern defines an interface for creating an object, but lets subclasses decide which class to instantiate.
- This pattern lets a class defer instantiation to its subclasses
- Inheritance for complex object creation, uses permutation and combination(remember Gold Customer, VAT Tax, Courier Delivery)
***Many developers think centralizing the object creation is Factory Pattern: Wrong!!!***
***Simple Factory Pattern is not Factory Method Pattern***
- This pattern is particularly useful when the exact type of object to be created isn’t known until runtime.
- Let’s consider a scenario where we need to create different types of credit cards. We will use the Factory Pattern to create instances of different credit card types based on user input.
- If we have multiple combination of objects that need to be created use the Factory Pattern
- Look at the case of Special Customer where we need to do Tax Calculation and Delivery as well(example in FactoryPattern.cs)
- Dont use if-else or switch statement in Factory Pattern
- Use Dictionary for example

```c#
static Dictionary<int,ICustomer> custs = new Dictionary<int,ICustomer>();
public static SimpleFactory
{
    custs.Add(1, new DiscountedCustomer());
    custs.Add(2, new GoldCustomer());
}

public static ICustomer Create(int i)
{
    //Clone it to avoid getting a singleton instance everytime it is called
    return custs[i].Clone();
}

```
- Example of Factory Pattern
```c#
\\Define an interface for the products the factory will create
public interface ICreditCard
{
    string GetCardType();
    int GetCreditLimit();
    int GetAnnualCharge();
}

\\Implement the concrete products
public class MoneyBackCreditCard : ICreditCard
{
    public string GetCardType() => "MoneyBack";
    public int GetCreditLimit() => 15000;
    public int GetAnnualCharge() => 500;
}

public class TitaniumCreditCard : ICreditCard
{
    public string GetCardType() => "Titanium";
    public int GetCreditLimit() => 25000;
    public int GetAnnualCharge() => 1500;
}

public class PlatinumCreditCard : ICreditCard
{
    public string GetCardType() => "Platinum";
    public int GetCreditLimit() => 35000;
    public int GetAnnualCharge() => 2000;
}

\\Create the Factory Class
public class CreditCardFactory
{
    public static ICreditCard GetCreditCard(string cardType)
    {
        switch (cardType.ToLower())
        {
            case "moneyback":
                return new MoneyBackCreditCard();
            case "titanium":
                return new TitaniumCreditCard();
            case "platinum":
                return new PlatinumCreditCard();
            default:
                throw new ArgumentException("Invalid card type");
        }
    }
}

\\Use the Factory in client code
class Program
{
    static void Main(string[] args)
    {
        ICreditCard card = CreditCardFactory.GetCreditCard("Platinum");

        Console.WriteLine($"Card Type: {card.GetCardType()}");
        Console.WriteLine($"Credit Limit: {card.GetCreditLimit()}");
        Console.WriteLine($"Annual Charge: {card.GetAnnualCharge()}");
    }
}


```

- Define an interface for creating an object and defer the instantiation to the sub-classes

```c#
 //Define an interface for creating an object
 public interface IFactoryCustomer
    {
        ICustomerType Create();
        ITax CreateTax();
        IDelivery CreateDelivery();
    }


    //Create an instantiation of the interface with a base class and mark its methods as virtual
    //Think of it as the Base Class
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
```


# Abstract Factory Pattern
- Abstract factory sits on top of the Factory Pattern
- It is referred to factory of factories because it helps create other factories
- It has the following components
1. Abstract Factory: Defines an interface for creating Abstract Product Objects
2. Concrete Factory: Implements the operations in Abstract Factory to create Concrete Product Objects
3. Abstract Product: Defines an interface for type of Product Object
4. Concrete Product: Defines the concrete implementation for the abstract product

- There are no subclasses
- It provides an interface for creating families of related objects
- Sits on top of factories
- You can go across factories(use from FactorySupplier and FactoryCustomer)
- Simple Factory is for one class and one family(centralized object creation, it is just a lookup). It is a container from where we can get instances.
- Factory Pattern is permutations and combinations of ITax, IDelivery and ICustomerType(use inheritance and subclasses)
- Abstract Factory is for going across different factories



```c#

\\Abstract Product
public interface ICar 
{
    void Drive();
}
public interface IBike
{
    void Ride();
}

\\Concrete Product
public class HondaCar: ICar
{
    void Drive()
    {
        Console.WriteLine("Driving a Honda Car");
    }
}
public class YamahaCar: ICar
{
    void Drive()
    {
        Console.WriteLine("Driving a Yamaha Car");
    }
}
public class YamahaBike: IBike
{
    void Ride()
    {
        Console.WriteLine("Riding a Yamaha Bike");
    }
}
public class HondaBike: IBike
{
    void Ride()
    {
        Console.WriteLine("Riding a Honda Bike");
    }
}

\\Abstract Factory
public interface IVehicleFactory
{
    ICar CreateCar();
    IBike CreateBike();
}

\\Concrete Factory
public class HondaFactory:IVehicleFactory
{
   public  ICar CreateCar()
    {
        return new HondaCar();
    }
   public  IBike CreateBike()
    {
        return new HondaBike();
    }
}

public class YamahaFactory:IVehicleFactory
{
   public  ICar CreateCar()
    {
        return new YamahaCar();
    }
   public  IBike CreateBike()
    {
        return new YamahaBike();
    }
}

\\Usage

IVehicleFactory factory = new HondaFactory();
ICar hondaCar = factory.CreateCar();
IBike hondaBike = factory.CreateBike();

hondaCar.Drive();
hondaBike.Ride();

IVehicleFactory factory = new YamahaFactory();
ICar yamahaCar = factory.CreateCar();
IBike yamahaBike = factory.CreateBike();

yamahaCar.Drive();
yamahaBike.Ride();


```

# Template Pattern
- Behavioral Design Pattern that defines the skeleton of an algorithm in a method, deferring some steps to subclasses
- It allows subclasses to redefine certain steps of the algorithm without changing the algorithm's structure
- Here subclasses can provide specific implementations for certain steps
- Think of connecting to database: open connection, read connection and close connection
- Similarly for reading from a file: open a file, read the file stream, close the file stream
- In the above process, steps are the same, we can have abstract methods like Open(), Read(), Close()
- Implementation can be inside a FileProcessor or DataProcessor
- It has 2 components
1. Abstract Class: Defines abstract methods that subclasses should implement and a template method that defines the algorithm's structure
2. Concrete Class: Implements the abstract methods defined in the Abstract Class
- Scenarios for using template pattern include: XML Parsing, ETL Process, Connecting to Db, Page Lifecycle, Payment Process, Parsing of file, fixed workflows, reporting templates, Customizing Logging utility
- Look up the 6 scenarios for template pattern by shivprasad koirala
- Template Pattern has fixed steps, inherit and change particular step in the child class


```c#

\\Define the Abstract Class
public abstract class DataProcessor
{
    \\Define the steps of the algorithm
    public void ProcessData()
    {
        ReadData();
        Process();
        SaveData();
    }
    
    \\Concrete implementations will be provided in subclasses
    public abstract void ReadData();
    public abstract void Process();
    public abstract void SaveData();
}

\\Define the Concrete Classes

public class CsvProcessor: DataProcessor
{
    protected override void ReadData()
    {
        Console.WriteLine("Reading data from CSV file.");
    }

    protected override void Process()
    {
        Console.WriteLine("Processing CSV data.");
    }

    protected override void SaveData()
    {
        Console.WriteLine("Saving processed data to CSV file.");
    }
}

public class XmlDataProcessor : DataProcessor
{
    protected override void ReadData()
    {
        Console.WriteLine("Reading data from XML file.");
    }

    protected override void Process()
    {
        Console.WriteLine("Processing XML data.");
    }

    protected override void SaveData()
    {
        Console.WriteLine("Saving processed data to XML file.");
    }
}

\\Usage
class Program
{
    static void Main(string[] args)
    {
        DataProcessor csvProcessor = new CsvDataProcessor();
        csvProcessor.ProcessData();

        DataProcessor xmlProcessor = new XmlDataProcessor();
        xmlProcessor.ProcessData();
    }
}


```

# Decorator Pattern (evolved from Bridge Pattern)
- The Decorator Pattern is a structural design pattern that allows you to dynamically add behavior to an object without altering its structure.
- This pattern is particularly useful when you want to add responsibilities to individual objects, not to an entire class.
- Key Components
    1. Component Interface: Defines the interface for objects that can have responsibilities added to them.
    2. Concrete Component: The class that implements the Component interface and provides the basic behavior.
    3. Decorator: An abstract class that implements the Component interface and contains a reference to a Component object. This class serves as the base for all decorators.
    4. Concrete Decorators: Classes that extend the Decorator class and add additional behavior.

***YAGNI: Your arent gonna need it(Martin Fowler)***
- Always implement things when you need them, never when you just forsee them.
- Do the simplest thing that could possibly work
- Dont do over-architecting

***Model vs Entity vs DTO***
- DTO is the odd man out
- Say we have client app running in React and we want to transfer data to WebAPIController
- DTO is a POCO object which helps to transfer data from one logical unit to another
- Goal of DTO is just to pass Data
- Model is similar to Entity (Model was in OOPs and Entity comes from DDD(Domain Driven Development))
- All models dont necessarily become an entity, some of them become a service class also
- Entity is like the implementation

## Design Patterns Summary

### Creational Patterns


- Abstract Factory:	Creates an instance of several families of classes
- Builder:	Separates object construction from its representation
- Factory Method:	Creates an instance of several derived classes
- Prototype:	A fully initialized instance to be copied or cloned
- Singleton:	A class of which only a single instance can exist

### Structural Patterns


- Adapter:	Match interfaces of different classes
- Bridge:	Separates an object’s interface from its implementation
- Composite:	A tree structure of simple and composite objects
- Decorator:	Add responsibilities to objects dynamically
- Facade:	A single class that represents an entire subsystem
- Flyweight:	A fine-grained instance used for efficient sharing
- Proxy: 	An object representing another object

### Behavioral Patterns


- Chain of Resp.:	A way of passing a request between a chain of objects
- Command:	Encapsulate a command request as an object
- Interpreter:	A way to include language elements in a program
- Iterator:	Sequentially access the elements of a collection
- Mediator:	Defines simplified communication between classes
- Memento:	Capture and restore an object's internal state
- Observer:	A way of notifying change to a number of classes
- State:	Alter an object's behavior when its state changes
- Strategy:	Encapsulates an algorithm inside a class
- Template Method:	Defer the exact steps of an algorithm to a subclass
- Visitor:	Defines a new operation to a class without change

