using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multitenant.Models;
using Multitenant.Providers;
using CP.Repositories.Interfaces;
using Multitenant.Data;
using Multitenant.Data.Entities;

namespace Multitenant.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<ApplicationDbContext> _repository;

        public HomeController(IRepository<ApplicationDbContext> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var colleges = await _repository.GetAllAsync<College>();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
