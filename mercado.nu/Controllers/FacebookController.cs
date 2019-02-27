using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mercado.nu.Controllers
{
    //[Route("api")]
    public class FacebookController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsername()
        {
            return View();
        }


        //[HttpGet("me")]
        //public IActionResult UserInfo()
        //{
        //    //ta emot faktan som vi får 


        //    throw new Exception();
        //}

        //[HttpGet("SquareRoot")]
        //public IActionResult SquareRoot(int? number)
        //{
        //    if (number == null)
        //        return BadRequest("Du måste ange ett tal! ");

        //    if (number < 0)
        //        return BadRequest("Tal måste vara positivt");

        //    return Ok(Math.Sqrt((int)number));
        //}





        //Direkt
        //Skicka vidare så att användare får ange anvädarnamn/mail





        //Skapa en användare och en person --> fylla de två tabellerna






    }
}
