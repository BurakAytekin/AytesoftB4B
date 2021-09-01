using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.Domain
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}