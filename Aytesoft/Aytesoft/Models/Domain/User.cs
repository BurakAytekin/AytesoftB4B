using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aytesoft.Models.Domain
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}