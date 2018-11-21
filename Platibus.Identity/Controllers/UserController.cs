using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Handlers;
using Platibus.Identity.UpdateUserModel;

namespace Platibus.Identity.Controllers
{
    [Route("identity/users")]
    public class UserController : Controller
    {
        private readonly IUserHandler _userHandler;

        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequestModel requestModel)
        {
            var result =  await _userHandler.CreateUser(requestModel);

            if (!result.IsSuccessful)
            {
                return StatusCode(400, result.Message);
            }

            return new ObjectResult(new {id = result.Entity.Id});
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody]UpdateUserRequestModel requestModel)
        {
            var result = await _userHandler.UpdateUser(id, requestModel);

            if (!result.IsSuccessful)
            {
                return StatusCode(400, result.Message);
            }

            return StatusCode(200, result.Message);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _userHandler.GetUser(id);
            
            return new ObjectResult(result);
        }
    }
}