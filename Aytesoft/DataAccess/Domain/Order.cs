using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Domain
{
    public class Order
    {
        public int ID { get; set; }
        public string ProductID { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public int Price { get; set; }
    }
}