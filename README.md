# DesignPatterns
Design Patterns

**Human Beings understand things using classification**
Eric Evans wrote the popular book on Domain Driven Design

- Understand Object Oriented Programming(Helps us to identify entities)
- Once entities are identified, we can start classifying them
- Abstraction is the process where we identity the entity and what are the important things required for that entity

**Abstraction in code is done through interfaces**

- Nouns become the entity

### Ensure that abstraction is respected. 

-In the implementation part, ensure that only the interface methods being implemented
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
