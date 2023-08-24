
using CustomersControllerProject;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

var connStr = "server=localhost\\sqlexpress;" +
                "database=SalesDb;" +
                "trusted_connection=true;" +
                "trustServerCertificate=true;";
var conn = new SqlConnection(connStr);
conn.Open();
if(conn.State != System.Data.ConnectionState.Open) {
    throw new Exception("Connection did not open!!!");
}
// Code goes here ***************************************

var custCtrl = new CustomersController(conn);

//var newCust = new Customer {
//    Id = 0, Name = "Acme Mfg", City = "Mason", State = "OH", Sales = 0, Active = true
//};
//custCtrl.InsertCustomer(newCust);


//custCtrl.DeleteCustomer(41);
//Customer? cust = custCtrl.GetCustomerById(41);
//if(cust == null) {
//    Console.WriteLine("Customer with id of 41 is not found!");
//} else {
//    Console.WriteLine(cust);
//}

//custCtrl.UpdateCustomer(cust);
//cust = custCtrl.GetCustomerById(10);
//Console.WriteLine(cust);

//List<Customer> customers = custCtrl.GetAllCustomers();
//foreach(var c in customers) {
//    Console.WriteLine(c);
//
List<Customer> customers = custCtrl.GetCustomersByPartialName("er");
foreach(var c in customers) {
    Console.WriteLine(c);
}

// No code after here ***********************************
conn.Close();