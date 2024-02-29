using DataTransfer.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var model = new CountryListViewModel
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

        [HttpPost]
        public RedirectToActionResult Details(CountryViewModel country)
        {
           
            TempData["activeGame"] = country.activeGame;
            TempData["activeSport"] = country.activeSport;
            return RedirectToAction("Details", new { Id = country.Country.CountryId });
        }

        [HttpGet]
        public ViewResult Details(string id)
        {
            var model = new CountryViewModel
            {
                Country = context.countries.Include(c => c.Game).Include(c => c.Sport).FirstOrDefault(c => c.CountryId == id),
                activeGame = TempData?.Peek("activeGame")?.ToString() ?? "all",
                activeSport = TempData?.Peek("activeSport")?.ToString() ?? "all"
            };
            return View(model);
        }

    }
}
