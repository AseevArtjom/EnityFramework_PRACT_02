using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_PRACT_02
{
    public class Sale
    {
       public int Id { get; set; }
       public int GameId { get; set; }
       public int Count { get; set; }

       public Sale()
       {

       }

       public Sale(int gameid,int count)
       {
            GameId = gameid;
            Count = count;
       }
    }
}
