using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Models
{
    public class Category
    {
        [Key]
        public int category_Id { get; set; }
        public string category_name { get; set; }
    }
}
