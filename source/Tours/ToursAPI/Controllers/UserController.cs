using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb;
using ToursWeb.ComponentsBL;
using ToursWeb.Repositories;
using ToursWeb.ImpRepositories;
using ToursWeb.ModelsDB;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiUserController : ControllerBase
    {
        private readonly UserController _userController;

        public ApiUserController(UserController userController)
        {
            _userController = userController;
        }
        
        [Route("Tour")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tour>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers()
        {
            var tours = _userController.GetAllTours();
            if (tours == null) {
                return NotFound();
            }
            
            return Ok(tours);
        }
    }
}