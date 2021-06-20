using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivitiesManager.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CustomerTypeID { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public string Address { get; set; }
    }
}