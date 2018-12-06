using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Platibus.Identity.Handlers;
using Platibus.Identity.Helpers;
using Platibus.Identity.ViewModels;

namespace Platibus.Identity.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserHandler _userHandler;
        private readonly IOptions<WebAppConfiguration> _config;

        public AccountController(IUserHandler userHandler, IOptions<WebAppConfiguration> config)
        {
            _userHandler = userHandler;
            _config = config;
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
            var vm = new LoginViewModel
            {
                Error = "",
                ReturnUrl = $"{_config.Value.WebsiteUrl}/Booking/Booking_Index",
                IsSuccessfull = true
            };
            return View("~/Views/LoginView.cshtml", vm);
        }

        /// <summary>
        /// Accept the login form from the view
        /// </summary>
        /// <param name="loginInputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {
            //Login the user
            var response = await _userHandler.Login(loginInputModel.Email, loginInputModel.Password);

            if (response.IsSuccessful)
            {
                //Issue authentication cookie signed with the tempkey.RSA and bearing user info
                await HttpContext.SignInAsync(response.Entity.Id.ToString(), response.Entity.AuthLevel.ToString());
                
                if (loginInputModel.ReturnUrl == null)
                {
                    return Redirect($"{_config.Value.WebsiteUrl}/home");
                }
                
                return Redirect(loginInputModel.ReturnUrl);
            }
            
            //Show user error; example wrong login credentials
            ViewBag.ReturnUrl = loginInputModel.ReturnUrl;
            var vm = new LoginViewModel{Error = response.Message, ReturnUrl = loginInputModel.ReturnUrl, IsSuccessfull = response.IsSuccessful };
            return View("~/Views/LoginView.cshtml", vm);
        }

        
        /// <summary>
        /// The view to handle sign-outs
        /// </summary>
        /// <param name="logoutId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("logout")]
        
        public async Task<IActionResult> Logout(string logoutId)
        {
            //Make sure the cookies get cleared if front end forgets
            await HttpContext.SignOutAsync("idsrv.session");

            
            await HttpContext.SignOutAsync("idsrv");
            

            ViewBag.ReturnUrl = _config.Value.WebsiteUrl;
            //Return the login view right away so they may log in again.
            var vm = new LoginViewModel{Error = "", ReturnUrl = "", IsSuccessfull = true};
            return View("~/Views/LoginView.cshtml", vm);
        }
    }
}