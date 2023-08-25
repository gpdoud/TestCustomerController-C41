
using CustomersControllerProject;
using Microsoft.Data.SqlClient;

using OrdersControllerProject;

using System.Data.SqlTypes;

var connStr = "server=localhost\\sqlexpress;" +
                "database=SalesDb;" +
                "trusted_connection=true;" +
                "trustServerCertificate=true;";

var conn = OpenConnection(connStr);

TestOrdersController(conn);

CloseConnection();

// methods defined here *********************************

void TestOrdersController(SqlConnection conn) {
    
    var ordCtrl = new OrdersController(conn);
    
    //var order = ordCtrl.GetOrderById(28);
    //Console.WriteLine(order);
    
    var order = new Order() { Id = 0, Date = new DateTime(2023, 8, 27), 
                           Description = "Test Order For PG", CustomerId = 0 };
    ordCtrl.InsertOrder(order, "PG");
    
    //order.Id = 27;
    //order.CustomerId = 2;
    //ordCtrl.UpdateOrder(order);

    //ordCtrl.DeleteOrder(28);

    var orders = ordCtrl.GetAllOrders();
    orders.ForEach(x => Console.WriteLine(x));
}

void TestCustomersController(SqlConnection conn) {
    //var custCtrl = new CustomersController(conn);

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

    //List<Customer> customers = custCtrl.GetCustomersByPartialName("er");
    //foreach(var c in customers) {
    //    Console.WriteLine(c);
    //}

}

SqlConnection OpenConnection(string connStr) {
    var conn = new SqlConnection(connStr);
    conn.Open();
    if(conn.State != System.Data.ConnectionState.Open) {
        throw new Exception("Connection did not open!!!");
    }
    return conn;
}
void CloseConnection() {
    conn.Close();
}