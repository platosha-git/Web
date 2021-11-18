using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsDB;
using ToursWeb.Controllers;
using ToursAPI.ModelsDTO;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Tours")]

    public class ApiTourController : ControllerBase
    {
        private readonly TourController _tourController;
        private readonly HotelController _hotelController;
        private readonly FoodController _foodController;
        private readonly TransferController _transferController;

        public ApiTourController(TourController tourController, HotelController hotelController, 
            FoodController foodController, TransferController transferController)
        {
            _tourController = tourController;
            _hotelController = hotelController;
            _foodController = foodController;
            _transferController = transferController;
        }

        private List<UserTourDTO> ListUserTourDTO(List<Tour> lTours)
        {
            List<UserTourDTO> lUserToursDTO = new List<UserTourDTO>();
            foreach (var tour in lTours)
            {
                Hotel hotel = _hotelController.GetHotelByID(tour.Hotel); 
                Food food = _foodController.GetFoodByID(tour.Food);
                Transfer transfer = _transferController.GetTransferByID(tour.Transfer);
                
                UserTourDTO userTourDTO = new UserTourDTO(tour, hotel, food, transfer);
                lUserToursDTO.Add(userTourDTO);
            }
            return lUserToursDTO;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTourDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTours([FromQuery(Name = "City")] string city = null,
            [FromQuery(Name = "Hotel name")] string name = null,
            [FromQuery(Name = "DateBegin")] string dBegin = null, [FromQuery(Name = "DateEnd")] string dEnd = null)
        {
            List<Tour> tours = _tourController.GetAllTours();
            if (tours != null)
            {
                if (city != null)
                {
                    if (name != null)
                    {
                        List<Tour> toursCN = _tourController.GetToursByCityName(city, name);
                        List<Tour> res1 = tours.Intersect(toursCN).ToList();
                        tours = res1;
                    }
                    else
                    {
                        List<Tour> toursC = _tourController.GetToursByCity(city);
                        List<Tour> res2 = tours.Intersect(toursC).ToList();
                        tours = res2;
                    }
                }

                if (dBegin != null && dEnd != null)
                {
                    DateTime beg = Convert.ToDateTime(dBegin);
                    DateTime end = Convert.ToDateTime(dEnd);

                    List<Tour> toursDate = _tourController.GetToursByDate(beg, end);
                    List<Tour> res1 = tours.Intersect(toursDate).ToList();
                    tours = res1;
                }
            }
            
            if (tours == null || tours.Count == 0)
            {
                return NotFound();
            }

            List<UserTourDTO> lUserToursDTO = ListUserTourDTO(tours);
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

        /// <summary>Updating tour</summary>
        /// <param name="tourDTO">Tour to update</param>
        /// <returns>Updated tour</returns>
        /// <response code="200">Tour updated</response>
        /// <response code="400">Update error</response>
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
        
        /// <summary>Updating tour cost</summary>
        /// <returns>Updated tour</returns>
        /// <response code="200">Tour cost updated</response>
        /// <response code="400">Update error</response>
        [HttpPatch]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTourCost([FromRoute(Name = "TourID")] int tourID,
            [FromBody] int diff)
        {
            Tour uTour = _tourController.GetTourByID(tourID);
            if (uTour == null)
            {
                return NotFound();
            }
            
            bool isUpdated = _tourController.ChangeCost(tourID, diff);
            if (!isUpdated)
            {
                return BadRequest();
            }
            
            TourDTO updatedTour = new TourDTO(uTour);
            return Ok(updatedTour);
        }

        /// <summary>Removing transfer by ID</summary>
        /// <returns>Removed transfer</returns>
        /// <response code="200">Transfer removed</response>
        /// <response code="404">No transfer</response>
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
