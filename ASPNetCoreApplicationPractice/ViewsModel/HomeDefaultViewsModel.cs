using ASPNetCoreApplicationPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApplicationPractice.ViewsModel
{
    public class HomeDefaultViewsModel
    {
        public IEnumerable<Employee>  AllEmployee{ get; set; }
        public string Title { get; set; }
    }
}
