using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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
                

                var query = from Game in Games
                            join Developer in Developers on Game.DeveloperId equals Developer.Id
                            join Style in Styles on Game.StyleId equals Style.Id
                            select new
                            {
                                DeveloperName = Developer.Name,
                                StyleName = Style.StyleName
                            };

                ConsoleKeyInfo key;

                do
                {

                    Console.Clear();
                    Console.WriteLine("Esc - quit\nnum1 - search game by name\nnum2 - search game by developer\nnum3 - search game by name and developer\nnum4 - search game by style\nnum5 - search game by release\nnum6 - Top3 developer with max count games\nnum7 - add new game\nnum8 - delete game");
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
                    else if(key.Key == ConsoleKey.NumPad7)
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
                    else if(key.Key == ConsoleKey.NumPad8)
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
