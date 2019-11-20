using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApplicationPractice.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required,MaxLength(300)]
        public string Address { get; set; }
        [Required]
        public Department? Department { get; set; }
        [Required]
        public long? Salary { get; set; }
        public string  PhotoPath { get; set; }
    }
}
