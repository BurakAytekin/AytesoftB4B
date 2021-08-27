using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.Domain
{
    public class Order
    {
        public int id { get; set; }
        public string productid { get; set; }
        public string date { get; set; }
        public int status { get; set; }
        public double productprice { get; set; }
        public int quantity { get; set; }
        public int userid { get; set; }
        public int price { get; set; }
    }
}