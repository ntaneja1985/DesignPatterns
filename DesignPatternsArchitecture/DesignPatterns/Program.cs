// See https://aka.ms/new-console-template for more information
using DesignPatterns;
using Ninject;
using System.Reflection;
using static DesignPatterns.IEvent;

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

Customer customer2 = new Customer();
customer2.Name = "Nishant";
IRepository<Customer> repo2 = null;//Repo EF, Repo ADO, Repo File
repo2.Save(customer2);


//Created in the Post Method
var createCustomer = new CreateCustomerCommand();
createCustomer.Name = "Test";
//Handler is the mediator
var handler = new CreateCustomerCommandHandler();
handler.Execute(createCustomer);

//Using the Facade Pattern
var accounting = new Accounting();
var billing = new Billing();
BillingFacade billingFacade = new BillingFacade(accounting,billing);
billingFacade.StartBilling();

ICustomerType custType = new GoldCustomer(new GSTTax(),new CourierDelivery());
//Wrong
ICustomerType custType2 = SimpleFactoryCustomerType.Create(0);
//Right
ICustomerType custType3 = CustomerTypeFactory.GetCustomerType(1);

//More simple, we define the interface and concrete factory implementation it has to use
IFactoryCustomer factoryCustomer = new FactoryCustomer();
ICustomerType cust4 = factoryCustomer.Create();

//Use the customer with pickup instead of courier
IFactoryCustomer factoryCustomerPickup = new FactoryCustomerPickup();
ICustomerType cust5 = factoryCustomerPickup.Create();

//use the customer with pickup instead of courier and VAT Tax instead of GST Tax
IFactoryCustomer factoryCustomerPickupVAT = new FactoryCustomerVATPickup();
ICustomerType cust6 = factoryCustomerPickupVAT.Create();

//use a gold customer instead of Basic Customer with pickup instead of courier and VAT  Tax instead of GST Tax
IFactoryCustomer factoryGoldCustomerPickupVAT = new FactoryGoldCustomerVATPickup();
ICustomerType cust7 = factoryGoldCustomerPickupVAT.Create();


CustomerDb customerDb = new CustomerDb();
customerDb.Execute();

//Decoupled the abstraction and implementation
ICustomerAbstraction customerAbstraction = new PaidCustomerNew();
IValidate validate = new PaidValidate();
validate.Validate(customerAbstraction);

IValidate validate1 = new BasicValidation();
validate1 = new PhoneCheckValidation(validate1);
validate1 = new BillCheckValidation(validate1);
validate1.Validate(customerAbstraction);

IValidate v = new BillCheckValidation(new PhoneCheckValidation(new BasicValidation()));

//Object of Ninject
IKernel _kernel = new StandardKernel();
_kernel.Load(Assembly.GetExecutingAssembly());  
//Will Inject Oracle Dal
MySupplier sup1 = _kernel.Get<MySupplier>(x=>x.Name=="O");

var countryList = SingletonCache.GetCountries();
countryList = null;

var singletonInstance = Singleton.Instance;
singletonInstance.DoSomething();

Menu mainMenu = new Menu();
mainMenu.SubMenu = new Menu();
mainMenu.SubMenu.SubMenu = new Menu();

SupplierClass supplierClass = new SupplierClass();
supplierClass.Id = 1;
supplierClass.Name = "Nishant";
supplierClass.Address.City = "Chandigarh";
Console.WriteLine(supplierClass.Id);
SupplierClass suppClone = supplierClass.Clone();
//Changing the clone should not affect the original object
suppClone.Id = 2;
supplierClass.Address.City = "Delhi";
Console.WriteLine(suppClone.Id);

//Memento Pattern
SupplierRepository supplierRepository = new SupplierRepository();
var supp1 = supplierRepository.Load();
supp1.Name = "newTest";
supp1.Revert();

//Strategy Pattern
var gstTax = new GstTaxDef();
var taxContext = new TaxContext(gstTax);
taxContext.CalculateTax(10);





