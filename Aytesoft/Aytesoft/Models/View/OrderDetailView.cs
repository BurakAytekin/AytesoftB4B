using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.View
{
    public class OrderDetailView
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
    }
}