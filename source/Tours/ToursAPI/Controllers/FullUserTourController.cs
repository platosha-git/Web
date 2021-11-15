using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsDB;
using ToursWeb.Controllers;
using ToursAPI.ModelsDTO;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ApiFullUserTourController : ControllerBase
    {
        private readonly FullUserTourController _futController;
        
        public ApiFullUserTourController(FullUserTourController futController)
        {
            _futController = futController;
        }
        
        [HttpGet]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FullUserTourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFullTour([FromRoute(Name = "TourID")] int tourID)
        {
            var tour = _futController.GetFullTour(tourID);
            if (tour == null)
            {
                return NotFound();
            }

            FullUserTourDTO tourDTO = new FullUserTourDTO(tour);
            return Ok(tourDTO);
        }
    }
}