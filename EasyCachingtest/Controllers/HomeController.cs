﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyCachingtest.Models;
using EasyCaching.Core;

namespace EasyCachingtest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEasyCachingProviderFactory _factory;
        public HomeController(IEasyCachingProviderFactory factory)
        {
            this._factory = factory;
        }

        public IActionResult Index()
        {

            var provider = _factory.GetCachingProvider(EasyCachingConstValue.DefaultRedisName);
            var val = Guid.NewGuid().ToString();
            var res = provider.Get("named-provider", () => { return val; } , TimeSpan.FromMinutes(1));


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
