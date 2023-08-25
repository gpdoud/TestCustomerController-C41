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
        AddSqlParameters(cmd, order);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Update failed! RA is {rowsAffected}");
        }
    }

    private void AddSqlParameters(SqlCommand cmd, Order order) {
        cmd.Parameters.AddWithValue("@Date", order.Date);
        cmd.Parameters.AddWithValue("@Description", order.Description);
        cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
    }

    public void InsertOrder(Order order) {
        var sql = " INSERT orders " +
                    " (Date, Description, CustomerId) VALUES " +
                    " (@Date, @Description, @CustomerId) ";
        var cmd = new SqlCommand(sql, sqlConnection);
        //cmd.Parameters.AddWithValue("@Id", 0);
        AddSqlParameters(cmd, order);
        var rowsAffected = cmd.ExecuteNonQuery();
        if(rowsAffected != 1) {
            throw new Exception($"Insert failed! RA is {rowsAffected}");
        }
    }

    public void InsertOrder(Order order, string customerCode) {
        var sql = "SELECT Id From Customers Where Code = @Code;";
        var cmd = new SqlCommand(sql, sqlConnection);
        cmd.Parameters.AddWithValue("@Code", customerCode);
        var custId = Convert.ToInt32(cmd.ExecuteScalar());
        order.CustomerId = custId;
        InsertOrder(order);
    }

    public Order? GetOrderById(int id) {
        if(id <= 0) {
            throw new ArgumentException("'id' must be greater than zero");
        }
        var cmd = new SqlCommand(Order.SqlGetById, sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);
        var reader = cmd.ExecuteReader();
        if(!reader.HasRows) {
            reader.Close();
            return null;
        }
        reader.Read();
        var ord = CreateOrderFromReader(reader);
        reader.Close();
        return ord;
    }

    private Order CreateOrderFromReader(SqlDataReader reader) {
        var ord = new Order();
        ord.Id = Convert.ToInt32(reader["Id"]);
        ord.Date = Convert.ToDateTime(reader["Date"]);
        ord.Description = Convert.ToString(reader["Description"]);
        ord.CustomerId = Convert.ToInt32(reader["CustomerId"]);
        return ord;
    }

    public List<Order> GetAllOrders() {
        var cmd = new SqlCommand(Order.SqlGetAll, sqlConnection);
        var reader = cmd.ExecuteReader();
        var orders = new List<Order>();
        while(reader.Read()) {
            var ord = CreateOrderFromReader(reader);
            orders.Add(ord);
        }
        reader.Close();
        return orders;
    }

}