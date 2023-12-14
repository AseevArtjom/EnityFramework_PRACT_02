using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_PRACT_02
{
    public class Style
    {
        public int Id { get; set; }
        public string StyleName { get; set; }
        public Style(string stylename)
        {
            StyleName = stylename;
        }
        public Style()
        {

        }
        public override string ToString()
        {
            return $"Id : {Id},Style : {StyleName}";
        }
    }
}