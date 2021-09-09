using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccess.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public double Price { get; set; }
    }
}