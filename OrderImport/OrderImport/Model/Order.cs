using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderImport
{
    /*
    Id (int, PK) |
    CustomerId (int, FK to Customer) |
    OrderDate (date + time) |
    OrderValue (decimal, length 8, precision 2)
    */
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(8,2)")] public decimal OrderValue { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}