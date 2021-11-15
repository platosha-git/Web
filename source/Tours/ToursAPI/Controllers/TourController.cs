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

    public class ApiTourController : ControllerBase
    {
        private readonly TourController _tourController;

        public ApiTourController(TourController tourController)
        {
            _tourController = tourController;
        }

        private List<TourDTO> ListTourDTO(List<Tour> lTours)
        {
            List<TourDTO> lToursDTO = new List<TourDTO>();
            foreach (var tour in lTours)
            {
                TourDTO tourDTO = new TourDTO(tour);
                lToursDTO.Add(tourDTO);
            }
            return lToursDTO;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TourDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTours()
        {
            var tours = _tourController.GetAllTours();
            if (tours == null)
            {
                return NotFound();
            }

            List<TourDTO> lToursDTO = ListTourDTO(tours);
            return Ok(lToursDTO);
        }
        
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

        [HttpGet]
        [Route("{DateBegin}/{DateEnd}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TourDTO>))]
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

            List<TourDTO> lToursDTO = ListTourDTO(tours);
            return Ok(lToursDTO);
        }

        [HttpGet]
        [Route("{City}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TourDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetToursByCity([FromRoute(Name = "City")] string city)
        {
            var tours = _tourController.GetToursByCity(city);
            if (tours == null)
            {
                return NotFound();
            }

            List<TourDTO> lToursDTO = ListTourDTO(tours);
            return Ok(lToursDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTour([FromBody] TourDTO tourDTO)
        {
            Tour aTour = tourDTO.GetTour();
            _tourController.AddTour(aTour);

            Tour tour = _tourController.GetTourByID(aTour.Tourid); 
            if (tour == null) 
            {
                return BadRequest();
            }

            TourDTO addedTour = new TourDTO(tour);
            return Ok(addedTour);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTour([FromBody] TourDTO tourDTO)
        {
            Tour uTour = tourDTO.GetTour();
            _tourController.UpdateTour(uTour);

            Tour tour = _tourController.GetTourByID(tourDTO.Tourid); 
            if (!tourDTO.AreEqual(tour))
            {
                return NotFound();
            }

            TourDTO updatedTour = new TourDTO(tour);
            return Ok(updatedTour);
        }

        [HttpDelete]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTour([FromRoute(Name = "TourID")] int tourID)
        {
            Tour delTour = _tourController.GetTourByID(tourID);
            if (delTour == null)
            {
                return NotFound();
            }
            
            _tourController.DeleteTourByID(tourID);
            TourDTO tour = new TourDTO(delTour);
            return Ok(tour);
        }
    }
}
