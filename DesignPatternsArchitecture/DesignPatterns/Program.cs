// See https://aka.ms/new-console-template for more information
using DesignPatterns;

Console.WriteLine("Hello, World!");

NormalCustomer x = new NormalCustomer(new Rating());
x.Name = "";
x.CalculateDiscount();

ICustomer cust = new NormalCustomer(new Rating());
cust = new CustomerAgeType();

