using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.Controllers;
using ToursWeb.ModelsDTO;
using ToursWeb.ModelsBL;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Tours")]

    public class ApiTourController : ControllerBase
    {
        private readonly TourController _tourController;

        public ApiTourController(TourController tourController)
        {
            _tourController = tourController;
        }

        /// <summary>Tours by parameters</summary>
        /// <returns>Tours information</returns>
        /// <param name="city"></param>
        /// <param name="name"></param>
        /// <param name="dBegin">Format: dd-mm-yyyy</param>
        /// <param name="dEnd">Format: dd-mm-yyyy</param>
        /// <response code="200">Tours found</response>
        /// <response code="404">No tours</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTours([FromQuery(Name = "City")] string city = null,
            [FromQuery(Name = "HotelName")] string name = null,
            [FromQuery(Name = "DateBegin")] string dBegin = null, [FromQuery(Name = "DateEnd")] string dEnd = null)
        {
            List<TourBL> tours = _tourController.GetAllTours();
            if (tours != null)
            {
                if (city != null)
                {
                    if (name != null)
                    {
                        List<TourBL> toursCN = _tourController.GetToursByCityName(city, name);
                        List<TourBL> res1 = tours.Intersect(toursCN).ToList();
                        tours = res1;
                    }
                    else
                    {
                        List<TourBL> toursC = _tourController.GetToursByCity(city);
                        List<TourBL> res2 = tours.Intersect(toursC).ToList();
                        tours = res2;
                    }
                }

                if (dBegin != null && dEnd != null)
                {
                    DateTime beg = Convert.ToDateTime(dBegin);
                    DateTime end = Convert.ToDateTime(dEnd);

                    List<TourBL> toursDate = _tourController.GetToursByDate(beg, end);
                    List<TourBL> res1 = tours.Intersect(toursDate).ToList();
                    tours = res1;
                }
            }
            
            if (tours == null || tours.Count == 0)
            {
                return NotFound();
            }

            List<UserTour> lUserToursDTO = _tourController.ToUserTour(tours);
            return Ok(lUserToursDTO);
        }
        
        /// <summary>Tour by ID</summary>
        /// <returns>Tour information</returns>
        /// <response code="200">Tour found</response>
        /// <response code="404">No tour</response>
        [HttpGet]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTourByID([FromRoute(Name = "TourID")] int tourID)
        {
            var tour = _tourController.GetTourByID(tourID);
            if (tour == null)
            {
                return NotFound();
            }

            TourDTO tourDTO = new TourDTO(tour);
            return Ok(tourDTO);
        }

        /// <summary>Adding tour</summary>
        /// <param name="tourDTO">Tour to add</param>
        /// <returns>Added tour</returns>
        /// <response code="200">Tour added</response>
        /// <response code="400">Add error</response>
        /// <response code="409">Constraint error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddTour([FromBody] TourUserDTO tourDTO)
        {
            TourBL aTour = tourDTO.GetTour();
            ExitCode result = _tourController.AddTour(aTour);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }

            TourDTO addedTour = new TourDTO(aTour);
            return Ok(addedTour);
        }

        /// <summary>Updating tour</summary>
        /// <param name="tourDTO">Tour to update</param>
        /// <returns>Updated tour</returns>
        /// <response code="200">Tour updated</response>
        /// <response code="400">Update error</response>
        /// <response code="409">Constraint error</response>
        [HttpPut]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateTour([FromRoute(Name = "TourID")] int tourID, [FromBody] TourUserDTO tourDTO)
        {
            TourBL uTour = tourDTO.GetTour(tourID);
            ExitCode result = _tourController.UpdateTour(uTour);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }

            TourDTO updatedTour = new TourDTO(uTour);
            return Ok(updatedTour);
        }

        /// <summary>Removing transfer by ID</summary>
        /// <returns>Removed transfer</returns>
        /// <response code="200">Transfer removed</response>
        /// <response code="400">Remove error</response>
        /// <response code="404">No transfer</response>
        [HttpDelete]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTour([FromRoute(Name = "TourID")] int tourID)
        {
            TourBL delTour = _tourController.GetTourByID(tourID);
            if (delTour == null)
            {
                return NotFound();
            }
            
            ExitCode result = _tourController.DeleteTourByID(tourID);
            if (result == ExitCode.Error)
            {
                return BadRequest();
            }
            
            TourDTO tour = new TourDTO(delTour);
            return Ok(tour);
        }
    }
}
