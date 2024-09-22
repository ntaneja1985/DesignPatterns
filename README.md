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
- The LSP ensures that substituting a Rectangle or a Circle for a Shape doesn’t break the program. The behavior (calculating the area) remains consistent.
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

By following DIP, we’ve achieved better separation of concerns and improved testability. Plus, we’re ready to adapt to changes in data access without breaking our entire system.

Remember, DIP isn’t just about interfaces; it’s about designing your system to depend on abstractions rather than concrete detail

### Now in latest version of C#, we can write concrete methods in Interfaces, but it is a hack. Earlier people used to create Abstract classes for this purpose. Any common logic between classes can be implemented in Abstract class

- In Factory pattern, consumer doesnot create concrete implementation. It depends on IFactory

- Interfaces is very uesful if we have complex classification of families.
- SOA is an architectural style, Event Sourcing is a microservices design pattern
- If there is no common logic between classes, then abstract class is not really required. However utilizing interfaces is a good practice.
- Dont create abstract classes first. Usually they are made while refactoring.Also called Late architecting