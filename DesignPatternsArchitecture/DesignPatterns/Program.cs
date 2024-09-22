// See https://aka.ms/new-console-template for more information
using DesignPatterns;

Console.WriteLine("Hello, World!");

NormalCustomer x = new NormalCustomer(new Rating());
x.Name = "";
x.CalculateDiscount();

ICustomer cust = new NormalCustomer(new Rating());
cust = new CustomerAgeType();


ICustomer cust3 = new EnquiryCustomer();
IRepositoryRead read = new Repository();
read.Read();
//Enquiry Customer is not buying anything, no need for Amount property
//cust3.Amount will throw an exception
//cust3.Amount