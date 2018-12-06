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
            //apparently Guids can be made 'nullable', so a null check is made, although guid is a value type and thus cannot be null pr. default
            if (id == Guid.Empty || id == null) return StatusCode(400);
            
            var result = await _userHandler.UpdateUser(id, requestModel);

            return StatusCode(!result.IsSuccessful ? 400 : 200, result.Message);
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