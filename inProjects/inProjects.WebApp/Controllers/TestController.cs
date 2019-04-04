using inProjects.WebApp.Models;
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
        
        [HttpPost("postTest")]
        public void PostTest([FromBody] ErrorViewModel model)
        {
            Console.WriteLine(model.RequestId);
        }
    }
}