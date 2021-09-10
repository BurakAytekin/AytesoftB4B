using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.View
{
    public class ProductView
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ImagePath { get; set; }

        public int Stock { get; set; }
    }
}