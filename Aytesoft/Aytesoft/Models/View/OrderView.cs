using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.View
{
    public class OrderView
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
    }
}