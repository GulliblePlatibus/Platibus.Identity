using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platibus.Identity.ViewModels;

namespace Platibus.Identity.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }      

        /// <summary>
        /// Entry point for login page.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="returnUrl">Return URL.</param>
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/LoginView.cshtml");
        }


        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {

            //Login!
            await HttpContext.SignInAsync("1", "Ulsan");
		    
            return Redirect(loginInputModel.ReturnUrl);
        }
    }
}