﻿// See https://aka.ms/new-console-template for more information
using DesignPatterns;

Console.WriteLine("Hello, World!");

NormalCustomer x = new NormalCustomer(new Rating());
x.Name = "";
x.CalculateDiscount();

ICustomer cust = new NormalCustomer(new Rating());
cust = new CustomerAgeType();


ICustomer cust3 = new EnquiryCustomer();
//IRepositoryRead read = new Repository();
//read.Read();
//Enquiry Customer is not buying anything, no need for Amount property
//cust3.Amount will throw an exception
//cust3.Amount

//IDiscount discount = new AgeDiscount();
IDiscount disount = new DiscountRegion();
IDiscount discount2 = SimpleFactory.CreateDiscount(2);
discount2.Calculate(cust);

ICustomer customer = SimpleFactory.CreateCustomer(0);

//CustomerRating rating = new CustomerRating();
//rating.Rating = 100;

//IRepository<Customer> dbCust = null;
//dbCust.Save(new Customer());

Customer c = new Customer();
IRepository<Customer> rep = FactoryRepository<Customer>.Create();
rep.Add(c);
rep.Save(c);

Supplier supplier = new Supplier();
IRepository<Supplier> sup = FactoryRepository<Supplier>.Create();
sup.Add(supplier);
sup.Save(supplier);

var generics = new Generics();
Console.WriteLine(generics.Compare<string>("test", "test"));

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

