using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersControllerProject;

public class Order {

    public int Id { get; set; } = 0;
    public DateTime Date { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public int? CustomerId { get; set; } = null;

    public override string ToString() {
        return $"{Id,2} | {Date} | {Description,-30} | {CustomerId,2}";
    }
}
