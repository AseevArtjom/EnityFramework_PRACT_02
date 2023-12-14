using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_PRACT_02
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public int DeveloperId { get; set; }
        public int StyleId { get; set; }
        public Game(string name, DateTime release,int devId,int styleId)
        {
            this.Name = name;
            this.Release = release;
            this.DeveloperId = devId;
            this.StyleId = styleId;
        }
        public Game()
        {

        }
        public Game(int id,string name,DateTime release)
        {
            Id = id;
            Name = name;    
            Release = release;
        }
        public override string ToString()
        {
            return $"Id : {Id},Name : {Name},Realease : {Release}";
        }
    }
}
