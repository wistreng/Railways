using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Railways.Models;
using Railways.Services;

namespace Railways.Controllers 
{
    public class QueryRoute : Controller
    {
        public RailServices railServices = new RailServices();
        public ActionResult Index()
        {   
            return View();
        }
        public ActionResult DistView()
        {
            return View();
        }
        public ActionResult PossiView()
        {
            return View();
        }
        public ActionResult ShortView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Distance(string route)
        {
            return Content(railServices.IsSolution(route).ToString());
        }
        [HttpPost]
        public ActionResult Possibilities(string start, string dest, double max)
        {   
            if (!string.IsNullOrWhiteSpace(start) || !string.IsNullOrWhiteSpace(dest))
            {
                Dictionary<string,double> tmp = railServices.FindRoutes(start, dest, max);
                string json = JsonConvert.SerializeObject(tmp,Formatting.Indented);
                return Content(json, "application/json");
            }
            else
            {
                throw new Exception("Input parameter error, parameters contain null or white space!");
            }
        }
        [HttpPost]
        public IActionResult Shortest(string start, string dest)
        {
            if (!string.IsNullOrWhiteSpace(start) || !string.IsNullOrWhiteSpace(dest))
            {
                Dictionary<string,double> tmp = railServices.FindRoutes(start, dest, Double.PositiveInfinity);
                tmp = railServices.ShortestRoute(tmp);
                string json = JsonConvert.SerializeObject(tmp,Formatting.Indented);
                return Content(json, "application/json");
            }
            else
            {  
                throw new Exception("Input parameter error, parameters contain null or white space!");
            }  
        }
    }
}