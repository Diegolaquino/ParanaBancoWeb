using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParanaBancoWeb.Application;
using ParanaBancoWeb.Models;
using ParanaBancoWeb.ViewModels;

namespace ParanaBancoWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _application;
        public UsersController(IUsersApplication application)
        {
            _application = application;
        }

        [HttpGet("email")]
        public async Task<IActionResult> Get([FromQuery]string email)
        {
            var user = await _application.GetUser(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _application.GetAll();

            if(!users.Any())
                return NoContent();

            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UserViewModel user)
        {
            var response = _application.UpdateUser(user);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSignUp user)
        {
            var response = await _application.CreateUser(user);
            return Created("users", response);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]string email)
        {
            _application.DeleteUser(email);

            return Ok();
        }
    }
}
