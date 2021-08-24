using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aytesoft.Models
{
    public class Product
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string imagepath { get; set; }
        public string stock { get; set; }
        public int status { get; set; }
    }
}