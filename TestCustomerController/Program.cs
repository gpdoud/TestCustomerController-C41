
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

//Customer? cust = custCtrl.GetCustomerById(10);
//Console.WriteLine(cust);

//List<Customer> customers = custCtrl.GetAllCustomers();
//foreach(var c in customers) {
//    Console.WriteLine(c);
//}

// No code after here ***********************************
conn.Close();