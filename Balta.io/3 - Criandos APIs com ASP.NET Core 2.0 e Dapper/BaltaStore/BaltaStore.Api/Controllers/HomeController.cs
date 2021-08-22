using System;
using Microsoft.AspNetCore.Mvc;

namespace BaltaStore.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Version 0.0.2";
        }


        [HttpGet]
        [Route("error")]
        public string Error()
        {
            throw new Exception("Something get wrong!");
        }
    }
}