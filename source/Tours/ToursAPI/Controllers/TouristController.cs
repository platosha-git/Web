using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb;
using ToursWeb.ComponentsBL;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ApiTouristController : ControllerBase
    {
        private readonly TouristController _touristController;

        public ApiTouristController(TouristController touristController)
        {
            _touristController = touristController;
        }

        [HttpGet]
        [Route("Tour/{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tour))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTourByID([FromRoute(Name = "TourID")] int tourID)
        {
            var tour = _touristController.GetTourByID(tourID);
            if (tour == null) 
            {
                return NotFound();
            }
            return Ok(tour);
        }
        
        [HttpGet]
        [Route("User/{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserInfo([FromRoute(Name = "UserID")] int userID)
        {
            var user = _touristController.GetAllUserInfo(userID);
            if (user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}