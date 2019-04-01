using Microsoft.AspNetCore.Mvc;
using System;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet("Chat")]
        public void Test(int q)
        {
            Console.WriteLine(q);
        }
    }
}