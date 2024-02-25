using DataTransfer.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Diagnostics;

namespace DataTransfer.Controllers
{
    public class HomeController : Controller
    {
        private CountryContext context;
        public HomeController(CountryContext ctx)
        {
            context = ctx;
        }
        public ViewResult Index(string activeGame = "All", string activeSport = "All") { 
           /* ViewBag.ActiveGame = activeGame;
            ViewBag.ActiveSport = activeSport;
            List<Game> games = context.games.ToList();
            List<Sport> sports = context.sports.ToList();
            games.Insert(0, new Game { GameId = "All", Name = "All" });
            sports.Insert(0, new Sport { SportId = "All", Name = "All" });
            ViewBag.Game = games;
            ViewBag.Sports = sports;
           */
            var model = new CountryViewModel
            {
                activeGame = activeGame,
                activeSport = activeSport,
                Games = context.games.ToList(),
                Sports = context.sports.ToList()
            };

            IQueryable<Country> query = context.countries;
            if(activeGame != "all")
            {
                query = query.Where(g => g.Game.GameId.ToLower() == activeGame.ToLower());
            }
            if(activeSport != "all")
            {
                query = query.Where(s => s.Sport.SportId.ToLower() == activeSport.ToLower());
            }
            
            model.Country = query.ToList();
            model.Country.OrderBy(Country => Country.CountryFlag);
            
            return View(model);
        }
    }
}
