using AdminDashboard.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_Description { get; set; }
        public float product_price { get; set; }
        public int category_id { get; set; }
        public List<Category>  Categories { get; set; }
        public IFormFile File { get; set; }
        public string imageURL { get; set; }
    } 
}
