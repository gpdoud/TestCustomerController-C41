using CustomersControllerProject;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrdersControllerProject;

public class OrdersController {

    public SqlConnection sqlConnection { get; set; }

    public OrdersController(SqlConnection sqlConnection) {
        this.sqlConnection = sqlConnection;
    }

    public void DeleteOrder(int id) {
        var sql = " DELETE Orders " +
                    " Where Id = @Id; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Delete failed! RA is {rowsAffected}");
        }
    }

    public void UpdateOrder(Order order) {
        var sql = " UPDATE Orders Set " +
                    " Date = @Date, " +
                    " Description = @Description, " +
                    " CustomerId = @CustomerId " +
                    " Where Id = @Id; ";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", order.Id);
        cmd.Parameters.AddWithValue("@Date", order.Date);
        cmd.Parameters.AddWithValue("@Description", order.Description);
        cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Update failed! RA is {rowsAffected}");
        }
    }

    public void InsertOrder(Order order) {
        var sql = " INSERT orders " +
                    " (Date, Description, CustomerId) VALUES " +
                    " (@Date, @Description, @CustomerId) ";
        var cmd = new SqlCommand(sql, sqlConnection);
        //cmd.Parameters.AddWithValue("@Id", 0);
        cmd.Parameters.AddWithValue("@Date", order.Date);
        cmd.Parameters.AddWithValue("@Description", order.Description);
        cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Insert failed! RA is {rowsAffected}");
        }
    }

    public Order? GetOrderById(int id) {
        if(id <= 0) {
            throw new ArgumentException("'id' must be greater than zero");
        }
        var sql = "SELECT * From Orders Where Id = @Id";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        var reader = cmd.ExecuteReader();
        if(!reader.HasRows) {
            reader.Close();
            return null;
        }
        reader.Read();
        var ord = new Order();
        ord.Id = Convert.ToInt32(reader["Id"]);
        ord.Date = Convert.ToDateTime(reader["Date"]);
        ord.Description = Convert.ToString(reader["Description"]);
        ord.CustomerId = Convert.ToInt32(reader["CustomerId"]);
        reader.Close();
        return ord;
    }

    public List<Order> GetAllOrders() {
        var sql = "SELECT * From Orders";
        var cmd = new SqlCommand(sql, sqlConnection);
        var reader = cmd.ExecuteReader();
        var orders = new List<Order>();
        while(reader.Read()) {
            var ord = new Order();
            ord.Id = Convert.ToInt32(reader["Id"]);
            ord.Date = Convert.ToDateTime(reader["Date"]);
            ord.Description = Convert.ToString(reader["Description"]);
            ord.CustomerId = Convert.ToInt32(reader["CustomerId"]);
            orders.Add(ord);
        }
        reader.Close();
        return orders;
    }

}