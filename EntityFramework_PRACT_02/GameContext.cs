using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EntityFramework_PRACT_02.Program;

namespace EntityFramework_PRACT_02
{
    public class GameContext : DbContext
    {
        public string ConnectionString = @"Data Source=DESKTOP-0S5CE1C;Initial Catalog=Games;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DbSet<Game> Game { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Style> Styles { get; set; }

        public GameContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
