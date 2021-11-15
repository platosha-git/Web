using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb;
using ToursWeb.Controllers;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("[api/v1/tours]")]

    public class ApiTourController : ControllerBase
    {
        private readonly TourController _tourController;

        public ApiTourController(TourController tourController)
        {
            _tourController = tourController;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTours()
        {
            var tours = _tourController.GetAllTours();
            if (tours == null)
            {
                return NotFound();
            }

            return Ok(tours);
        }

        [HttpGet]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tour))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTourByID([FromRoute(Name = "TourID")] int tourID)
        {
            var tour = _tourController.GetTourByID(tourID);
            if (tour == null)
            {
                return NotFound();
            }

            return Ok(tour);
        }

        [HttpGet]
        [Route("{DateBegin}/{DateEnd}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetToursByDate([FromRoute(Name = "DateBegin")] string dBegin,
            [FromRoute(Name = "DateEnd")] string dEnd)
        {
            //01-12-2020    01-12-2021
            DateTime beg = Convert.ToDateTime(dBegin);
            DateTime end = Convert.ToDateTime(dEnd);

            var tours = _tourController.GetToursByDate(beg, end);
            if (tours == null)
            {
                return NotFound();
            }

            return Ok(tours);
        }

        [HttpGet]
        [Route("{City}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetToursByCity([FromRoute(Name = "City")] string city)
        {
            var tours = _tourController.GetToursByCity(city);
            if (tours == null)
            {
                return NotFound();
            }

            return Ok(tours);
        }

        [HttpPost]
        [Route("{Cost:int}/{DateBegin}/{DateEnd}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tour))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTour([FromRoute(Name = "Cost")] int cost,
            [FromRoute(Name = "DateBegin")] string dBegin,
            [FromRoute(Name = "DateEnd")] string dEnd)
        {
            DateTime beg = Convert.ToDateTime(dBegin);
            DateTime end = Convert.ToDateTime(dEnd);
            Tour nTour = new Tour
                {Tourid = 31, Food = 4, Hotel = 7, Transfer = 3, Cost = cost, Datebegin = beg, Dateend = end};

            _tourController.AddTour(nTour);
            if (_tourController.GetTourByID(nTour.Tourid) == null)
            {
                return BadRequest();
            }

            return Ok(nTour);
        }

        [HttpPut]
        [Route("{TourID:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tour))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTour([FromRoute(Name = "TourID")] int tourID,
            [FromRoute(Name = "Cost")] int cost)
        {
            Tour tour = _tourController.GetTourByID(tourID);

            if (tour == null)
            {
                return NotFound();
            }

            tour.Cost = cost;
            _tourController.UpdateTour(tour);

            if (_tourController.GetTourByID(tourID).Cost != cost)
            {
                return NotFound();
            }

            return Ok(tour);
        }

        [HttpDelete]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTour([FromRoute(Name = "TourID")] int tourID)
        {
            _tourController.DeleteTourByID(tourID);

            if (_tourController.GetTourByID(tourID) != null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}