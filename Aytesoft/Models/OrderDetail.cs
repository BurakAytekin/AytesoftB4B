using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public int productprice { get; set; }
        public int quantity { get; set; }
        public Product product { get; set; }
    }
}