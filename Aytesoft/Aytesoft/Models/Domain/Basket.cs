using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.Domain
{
    public class Basket
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int UserID { get; set; }
        public int ProductPrice { get; set; }
        public string ProductName { get; set; }

    }
}