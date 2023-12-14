using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_PRACT_02
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Developer(string name)
        {
            Name = name;
        }
        public Developer()
        {

        }
        public override string ToString()
        {
            return $"Id : {Id},Name : {Name}";
        }
    }
}
