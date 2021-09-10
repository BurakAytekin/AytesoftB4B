using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccess.Entity
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public int product_Id { get; set; }
        public virtual Product Product { get; set; }
    }
}