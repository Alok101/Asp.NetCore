using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApplicationPractice.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Alok1 Gautam", Address = "India1", Department = Department.BSC, Salary = 1200000 },
                new Employee() { Id = 2, Name = "Alok2 Gautam", Address = "India2", Department = Department.CE, Salary = 2200000 },
                new Employee() { Id = 3, Name = "Alok3 Gautam", Address = "India3", Department = Department.CSE, Salary = 3200000 },
                new Employee() { Id = 4, Name = "Alok4 Gautam", Address = "India4", Department = Department.ECE, Salary = 4200000 },
                new Employee() { Id = 5, Name = "Alok5 Gautam", Address = "India5", Department = Department.EEE, Salary = 5200000 },
                new Employee() { Id = 6, Name = "Alok6 Gautam", Address = "India6", Department = Department.ME, Salary = 6200000 },
                new Employee() { Id = 7, Name = "Alok7 Gautam", Address = "India7", Department = Department.MSC, Salary = 7200000 },
                new Employee() { Id = 8, Name = "Alok8 Gautam", Address = "India8", Department = Department.None, Salary = 7200000 }

                );
        }
    }
}
