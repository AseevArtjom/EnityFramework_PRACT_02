using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_PRACT_02
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (GameContext db = new GameContext())
            {
                var Games = db.Game.ToList();
                var Developers = db.Developers.ToList();
                var Styles = db.Styles.ToList();
                var Sales = db.Sales.ToList();

                var query = from Game in Games
                            join Developer in Developers on Game.DeveloperId equals Developer.Id
                            join Style in Styles on Game.StyleId equals Style.Id
                            join Sale in Sales on Game.Id equals Sale.GameId
                            select new
                            {
                                DeveloperName = Developer.Name,
                                StyleName = Style.StyleName
                            };

                ConsoleKeyInfo key;

                do
                {

                    Console.Clear();
                    Console.WriteLine("Esc - quit\nnum1 - search game by name\nnum2 - search game by developer\nnum3 - search game by name and developer\n" +
                        "num4 - search game by style\nnum5 - search game by release\nnum6 - add new game\nnum7 - delete game\n" +
                        "num8 - Top3 developer with max count games\nnum9 - Top1 developer with max count games\nF1 - Top3 games styles\nF2 - Top1 games styles" +
                        "\nF3 - Top3 styles by sales\nF4 - Top1 style by sales\nF5 - Top3 single games by sales\nF6 - Top3 multiplayer games by sales\nF7 - Top1 single game by sales" +
                        "\nF8 - Top1 multiplayer game by sales\nF9 - Top1 Game by sales");
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.NumPad1)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the name of the game:");
                        string gameName = Console.ReadLine();

                        var foundGames = Games.Where(game => game.Name.Contains(gameName)).ToList();

                        if (foundGames.Any())
                        {
                            foreach (var foundGame in foundGames)
                            {
                                var developer = Developers.FirstOrDefault(dev => dev.Id == foundGame.DeveloperId);
                                var style = Styles.FirstOrDefault(style1 => style1.Id == foundGame.StyleId);
                                if (developer != null && style != null)
                                {
                                    Console.WriteLine($"{foundGame.Id} , {foundGame.Name} , {developer.Name} , {foundGame.Release.ToString("yyyy.MM.dd")} , {style.StyleName}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No games found with that name");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.NumPad2)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the name of the developer:");
                        string developerName = Console.ReadLine();

                        var foundDevelopers = Developers.Where(dev => dev.Name.Contains(developerName)).ToList();
                        if (foundDevelopers.Any())
                        {
                            foreach (var foundDev in foundDevelopers)
                            {
                                var gamesByDeveloper = Games.Where(game => game.DeveloperId == foundDev.Id);
                                foreach (var game in gamesByDeveloper)
                                {
                                    var style = Styles.FirstOrDefault(style1 => style1.Id == game.StyleId);
                                    if (style != null)
                                    {
                                        Console.WriteLine($"{game.Id} , {game.Name} , {foundDev.Name} , {game.Release.ToString("yyyy.MM.dd")} , {style.StyleName}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No developer found with that name");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.NumPad3)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the name of the game:");
                        var GameName = Console.ReadLine();
                        Console.WriteLine("Enter developer:");
                        var DeveloperName = Console.ReadLine();

                        var FoundGames = Games.Where(game => game.Name.Contains(GameName)).ToList();
                        var FoundDevelopers = Developers.Where(dev => dev.Name.Contains(DeveloperName)).ToList();

                        if(FoundDevelopers.Any() && FoundGames.Any())
                        {
                            foreach (var foundGame in FoundGames)
                            {
                                var developer = Developers.FirstOrDefault(dev => dev.Id == foundGame.DeveloperId);
                                if (developer != null && developer.Name.Contains(DeveloperName))
                                {
                                    var style = Styles.FirstOrDefault(style1 => style1.Id == foundGame.StyleId);
                                    if (style != null)
                                    {
                                        Console.WriteLine($"{foundGame.Id} , {foundGame.Name} , {developer.Name} , {foundGame.Release.ToString("yyyy.MM.dd")} , {style.StyleName}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No games found with that name and developer");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No games found with that name and developer");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.NumPad4)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter 1 - single game, 2 - multiplayer");
                        int GameStyle = Convert.ToInt32(Console.ReadLine());

                        if (GameStyle == 1 || GameStyle == 2)
                        {
                            foreach (var item in Games)
                            {
                                var Style = Styles.FirstOrDefault(style1 => style1.Id == item.StyleId);
                                if (Style != null)
                                {
                                    if ((GameStyle == 1 && Style.StyleName == "Single") || (GameStyle == 2 && Style.StyleName == "Multiplay"))
                                    {
                                        var developer = Developers.FirstOrDefault(dev => dev.Id == item.DeveloperId);
                                        if (developer != null)
                                        {
                                            Console.WriteLine($"{item.Id} , {item.Name} , {developer.Name} , {item.Release.ToString("yyyy.MM.dd")} , {Style.StyleName}");
                                        }
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please enter either 1 or 2.");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.NumPad5)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter date of release (yyyy.MM.dd) : ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime Release))
                        {
                            foreach (var item in Games)
                            {
                                var Style = Styles.FirstOrDefault(style1 => style1.Id == item.StyleId);
                                if (item.Release.Date == Release.Date)
                                {
                                    var FoundDeveloper = Developers.FirstOrDefault(dev => dev.Id == item.DeveloperId);
                                    if (FoundDeveloper != null && Style != null)
                                    {
                                        Console.WriteLine($"{item.Id} , {item.Name} , {FoundDeveloper.Name} , {item.Release.ToString("yyyy.MM.dd")} , {Style.StyleName}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No games found with that date release");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect date format!");
                        }
                        Console.ReadKey();
                    }
                    else if(key.Key == ConsoleKey.NumPad6)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter name of game : ");
                        string NameGame = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Choose game(id) developer(Press num1 if you want to add new Developer) : ");
                        foreach (var dev in Developers)
                        {
                            Console.WriteLine(dev.Id + "," + dev.Name);
                        }

                        key = Console.ReadKey();
                        int DeveloperId;
                        if (key.Key == ConsoleKey.NumPad1)
                        {
                            Console.Clear();

                            Console.WriteLine("Enter new developer name : ");
                            string DevName = Console.ReadLine();
                            AddNewDeveloperToBD(DevName);
                            Developers = db.Developers.ToList();
                            DeveloperId = db.Developers.OrderByDescending(dev => dev.Id).FirstOrDefault()?.Id ?? 0;
                        }
                        else
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Choose game(id) developer(Press num1 if you want to add new Developer) : ");
                                foreach (var dev in Developers)
                                {
                                    Console.WriteLine(dev.Id + "," + dev.Name);
                                }
                                int.TryParse(Console.ReadLine(), out DeveloperId);
                            } while (DeveloperId > Developers.Count || DeveloperId < 1);
                        }

                        int StyleId;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter style of game(1 - single,2 - multiplay) : ");
                            int.TryParse(Console.ReadLine(), out StyleId);
                        } while (StyleId < 1 || StyleId > 2);


                        DateTime ReleaseDate;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Enter date of release (yyyy.MM.dd) : ");
                        } while (!DateTime.TryParse(Console.ReadLine(), out ReleaseDate));

                        Game newgame = new Game(NameGame, ReleaseDate, DeveloperId, StyleId);

                        db.Game.Add(newgame);
                        Games.Add(newgame);
                        db.SaveChanges();
                    }
                    else if(key.Key == ConsoleKey.NumPad7)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose game to delete(id) : ");
                        foreach (var item in Games)
                        {
                            Console.WriteLine(item.Id + "," + item.Name + "," + item.Release);
                        }
                        int GameId = Convert.ToInt32(Console.ReadLine());

                        var SelectedGame = db.Game.FirstOrDefault(game => game.Id == GameId);
                        if(SelectedGame != null)
                        {
                            db.Game.Remove(SelectedGame);
                            Games.Remove(SelectedGame);
                            db.SaveChanges();  
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete game");
                        }
                    }
                    else if (key.Key == ConsoleKey.NumPad8)
                    {
                        Console.Clear();
                        var topDevelopers = Games
                        .GroupBy(game => game.DeveloperId)
                        .Select(group => new
                        {
                            DeveloperId = group.Key,
                            GameCount = group.Count()
                        })
                        .OrderByDescending(result => result.GameCount)
                        .Take(3)
                        .ToList();

                        foreach (var topDeveloper in topDevelopers)
                        {
                            var developer = Developers.FirstOrDefault(dev => dev.Id == topDeveloper.DeveloperId);
                            if (developer != null)
                            {
                                Console.WriteLine($"{developer.Name} - {topDeveloper.GameCount} games");
                            }
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.NumPad9)
                    {
                        Console.Clear();
                        var topDevelopers = Games
                        .GroupBy(game => game.DeveloperId)
                        .Select(group => new
                        {
                            DeveloperId = group.Key,
                            GameCount = group.Count()
                        })
                        .OrderByDescending(result => result.GameCount)
                        .Take(1)
                        .ToList();

                        foreach (var topDeveloper in topDevelopers)
                        {
                            var developer = Developers.FirstOrDefault(dev => dev.Id == topDeveloper.DeveloperId);
                            if (developer != null)
                            {
                                Console.WriteLine($"{developer.Name} - {topDeveloper.GameCount} games");
                            }
                        }
                        Console.ReadKey();
                    }
                    else if(key.Key == ConsoleKey.F1)
                    {
                        Console.Clear();
                        var topStyles = Games
                        .GroupBy(game => game.StyleId)
                        .Select(group => new
                        {
                            StyleId = group.Key,
                            GameCount = group.Count()
                        })
                        .OrderByDescending(result => result.GameCount)
                        .Take(3)
                        .ToList();

                        foreach (var item in topStyles)
                        {
                            var Style = Styles.FirstOrDefault(s => s.Id == item.StyleId);
                            if (Style != null)
                            {
                                var gamesCount = Games.Count(game => game.StyleId == item.StyleId);
                                Console.WriteLine($"{Style.StyleName} - {gamesCount}");
                            }
                        }
                        Console.ReadKey();
                    }
                    else if(key.Key == ConsoleKey.F2)
                    {
                        Console.Clear();
                        var topStyles = Games
                        .GroupBy(game => game.StyleId)
                        .Select(group => new
                        {
                            StyleId = group.Key,
                            GameCount = group.Count()
                        })
                        .OrderByDescending(result => result.GameCount)
                        .Take(1)
                        .ToList();

                        foreach (var item in topStyles)
                        {
                            var Style = Styles.FirstOrDefault(s => s.Id == item.StyleId);
                            if (Style != null)
                            {
                                var gamesCount = Games.Count(game => game.StyleId == item.StyleId);
                                Console.WriteLine($"{Style.StyleName} - {gamesCount}");
                            }
                        }
                        Console.ReadKey();
                    }
                    else if(key.Key == ConsoleKey.F3)
                    {
                        Console.Clear();

                        var TopStylesBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .Join(Styles, sg => sg.game.StyleId, style => style.Id, (sg, style) => new { sg, style })
                            .GroupBy(s => s.style.StyleName)
                            .Select(g => new
                            {
                                StyleName = g.Key,
                                TotalSales = g.Sum(s => s.sg.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(3);

                        foreach (var item in TopStylesBySales)
                        {
                            Console.WriteLine($"{item.StyleName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.F4)
                    {
                        Console.Clear();

                        var TopStylesBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .Join(Styles, sg => sg.game.StyleId, style => style.Id, (sg, style) => new { sg, style })
                            .GroupBy(s => s.style.StyleName)
                            .Select(g => new
                            {
                                StyleName = g.Key,
                                TotalSales = g.Sum(s => s.sg.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(1);

                        foreach (var item in TopStylesBySales)
                        {
                            Console.WriteLine($"{item.StyleName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                    else if(key.Key == ConsoleKey.F5)
                    {
                        Console.Clear();

                        var TopSingleGamesBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .Where(sg => sg.game.StyleId == 1)
                            .GroupBy(sg => sg.game.Name)
                            .Select(g => new
                            {
                                GameName = g.Key,
                                TotalSales = g.Sum(s => s.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(3);

                        foreach (var item in TopSingleGamesBySales)
                        {
                            Console.WriteLine($"{item.GameName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.F6)
                    {
                        Console.Clear();

                        var TopSingleGamesBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .Where(sg => sg.game.StyleId == 2)
                            .GroupBy(sg => sg.game.Name)
                            .Select(g => new
                            {
                                GameName = g.Key,
                                TotalSales = g.Sum(s => s.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(3);

                        foreach (var item in TopSingleGamesBySales)
                        {
                            Console.WriteLine($"{item.GameName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.F7)
                    {
                        Console.Clear();

                        var TopSingleGamesBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .Where(sg => sg.game.StyleId == 1)
                            .GroupBy(sg => sg.game.Name)
                            .Select(g => new
                            {
                                GameName = g.Key,
                                TotalSales = g.Sum(s => s.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(1);

                        foreach (var item in TopSingleGamesBySales)
                        {
                            Console.WriteLine($"{item.GameName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.F8)
                    {
                        Console.Clear();

                        var TopSingleGamesBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .Where(sg => sg.game.StyleId == 2)
                            .GroupBy(sg => sg.game.Name)
                            .Select(g => new
                            {
                                GameName = g.Key,
                                TotalSales = g.Sum(s => s.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(1);

                        foreach (var item in TopSingleGamesBySales)
                        {
                            Console.WriteLine($"{item.GameName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                    else if (key.Key == ConsoleKey.F9)
                    {
                        Console.Clear();

                        var TopGameBySales = Sales
                            .Join(Games, sale => sale.GameId, game => game.Id, (sale, game) => new { sale, game })
                            .GroupBy(sg => sg.game.Name)
                            .Select(g => new
                            {
                                GameName = g.Key,
                                TotalSales = g.Sum(s => s.sale.Count)
                            })
                            .OrderByDescending(s => s.TotalSales)
                            .Take(1);

                        foreach (var item in TopGameBySales)
                        {
                            Console.WriteLine($"{item.GameName} - {item.TotalSales}");
                        }
                        Console.ReadKey();
                    }
                } while (key.Key != ConsoleKey.Escape);
            }
        }
        static public void AddNewDeveloperToBD(string NewDeveloper)
        {
            using (GameContext db = new GameContext())
            {
                db.Developers.Add(new Developer(NewDeveloper));
                db.SaveChanges();
            }
        }

    }
}
