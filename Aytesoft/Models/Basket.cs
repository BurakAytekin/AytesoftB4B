using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models
{
    public class Basket
    {
        public int id { get; set; }
        public int productid { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int userid { get; set; }
        public int productprice { get; set; }
        public string productname { get; set; }

    }
}