using Microsoft.Data.SqlClient;

using System.Data.SqlTypes;

namespace CustomersControllerProject;

public class CustomersController {

    public SqlConnection sqlConnection { get; set; }

    public CustomersController(SqlConnection sqlConnection) {
        this.sqlConnection = sqlConnection;
    }

    public Customer? GetCustomerById(int id) {
        if(id <= 0) {
            throw new ArgumentException("'id' must be greater than zero");
        }
        var sql = "SELECT * From Customers Where Id = @Id";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        var reader = cmd.ExecuteReader();
        if(!reader.HasRows) {
            reader.Close();
            return null;
        }
        reader.Read();
        var cust = new Customer();
        cust.Id = Convert.ToInt32(reader["Id"]);
        cust.Name = Convert.ToString(reader["Name"]);
        cust.City = Convert.ToString(reader["City"]);
        cust.State = Convert.ToString(reader["State"]);
        cust.Sales = Convert.ToDecimal(reader["Sales"]);
        cust.Active = Convert.ToBoolean(reader["Active"]);
        reader.Close();
        return cust;
    }

    public List<Customer> GetAllCustomers() {
        var sql = "SELECT * From Customers";
        var cmd = new SqlCommand(sql, sqlConnection);
        var reader = cmd.ExecuteReader();
        var customers = new List<Customer>();
        while(reader.Read()) {
            var cust = new Customer();
            cust.Id = Convert.ToInt32(reader["Id"]);
            cust.Name = Convert.ToString(reader["Name"]);
            cust.City = Convert.ToString(reader["City"]);
            cust.State = Convert.ToString(reader["State"]);
            cust.Sales = Convert.ToDecimal(reader["Sales"]);
            cust.Active = Convert.ToBoolean(reader["Active"]);
            customers.Add(cust);
        }
        reader.Close();
        return customers;
    }
}
