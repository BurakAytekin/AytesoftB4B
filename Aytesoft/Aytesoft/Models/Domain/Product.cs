using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aytesoft.Models.Domain
{
    public class Product
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ImagePath { get; set; }
        public string Stock { get; set; }
        public int Status { get; set; }
    }
}