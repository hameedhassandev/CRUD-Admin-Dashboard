using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string product_name { get; set; }
        public string product_Description { get; set; }
        public float product_price { get; set; }
        public string imageURL { get; set; }
        public Category category { get; set; }

    }
}
