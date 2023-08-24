using Microsoft.Data.SqlClient;

using System.Data.SqlTypes;

namespace CustomersControllerProject;

public class CustomersController {

    public SqlConnection sqlConnection { get; set; }

    public CustomersController(SqlConnection sqlConnection) {
        this.sqlConnection = sqlConnection;
    }

    public void DeleteCustomer(int id) {
        var sql = " DELETE Customers " +
                    " Where Id = @Id; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Delete failed! RA is {rowsAffected}");
        }
    }

    public void UpdateCustomer(Customer customer) {
        var sql = " UPDATE Customers Set " +
                    " Name = @Name, " +
                    " City = @City, " +
                    " State = @State, " +
                    " Sales = @Sales, " +
                    " Active = @Active " +
                    " Where Id = @Id; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", customer.Id);
        cmd.Parameters.AddWithValue("@Name", customer.Name);
        cmd.Parameters.AddWithValue("@City", customer.City);
        cmd.Parameters.AddWithValue("@State", customer.State);
        cmd.Parameters.AddWithValue("@Sales", customer.Sales);
        cmd.Parameters.AddWithValue("@Active", customer.Active);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Update failed! RA is {rowsAffected}");
        }
    }

    public void InsertCustomer(Customer customer) {
        var sql = " INSERT Customers " +
                    " (Name, City, State, Sales, Active) VALUES " +
                    " (@Name, @City, @State, @Sales, @Active) ";
        var cmd = new SqlCommand(sql, sqlConnection);
        //cmd.Parameters.AddWithValue("@Id", 0);
        cmd.Parameters.AddWithValue("@Name", customer.Name);
        cmd.Parameters.AddWithValue("@City", customer.City);
        cmd.Parameters.AddWithValue("@State", customer.State);
        cmd.Parameters.AddWithValue("@Sales", customer.Sales);
        cmd.Parameters.AddWithValue("@Active", customer.Active);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Insert failed! RA is {rowsAffected}");
        }
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

    public List<Customer> GetCustomersByPartialName(string substr) {
        var sql = "SELECT * From Customers " +
                    " Where Name like '%'+@substr+'%' " +
                    " Order by Sales desc; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@substr", substr);
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
