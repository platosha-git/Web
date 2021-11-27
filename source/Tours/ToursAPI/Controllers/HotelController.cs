using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsBL;
using ToursWeb.ModelsDTO;
using ToursWeb.Controllers;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Hotels")]

    public class ApiHotelController : ControllerBase
    {
        private readonly HotelController _hotelController;

        public ApiHotelController(HotelController hotelController)
        {
            _hotelController = hotelController;
        }

        private List<HotelDTO> ListHotelDTO(List<HotelBL> lHotels)
        {
            List<HotelDTO> lHotelDTO = new List<HotelDTO>();
            foreach (var hotel in lHotels)
            {
                HotelDTO hotelDTO = new HotelDTO(hotel);
                lHotelDTO.Add(hotelDTO);
            }
            return lHotelDTO;
        }
        
        /// <summary>Hotels by parameters</summary>
        /// <param name="city"></param>
        /// <param name="cls">Class (0 - 5)</param>
        /// <param name="type">Hotel, Apartment, Hostel, Guest house, Motel, Vila, Camping, BnB</param>
        /// <param name="sp"></param>
        /// <returns>Hotels information</returns>
        /// <response code="200">Hotels found</response>
        /// <response code="404">No hotels</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HotelDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllHotels([FromQuery(Name = "City")] string city = null,
            [FromQuery(Name = "Class")] int? cls = null, [FromQuery(Name = "Type")] string type = null,
            [FromQuery(Name = "Swimming pool")] bool? sp = null)
        {
            List<HotelBL> hotels = _hotelController.GetAllHotels();
            if (hotels != null)
            {
                if (city != null)
                {
                    hotels = _hotelController.GetHotelsByCity(city);
                }

                if (cls != null)
                {
                    List<HotelBL> hotelsCls = _hotelController.GetHotelsByClass((int)cls);
                    List<HotelBL> res1 = hotels.Intersect(hotelsCls).ToList();
                    hotels = res1;
                }

                if (type != null)
                {
                    List<HotelBL> hotelsType = _hotelController.GetHotelsByType(type);
                    List<HotelBL> res2 = hotels.Intersect(hotelsType).ToList();
                    hotels = res2;
                }
                
                if (sp != null)
                {
                    List<HotelBL> hotelsSP = _hotelController.GetHotelsBySwimPool((bool)sp);
                    List<HotelBL> res3 = hotels.Intersect(hotelsSP).ToList();
                    hotels = res3;
                }
            }
            
            if (hotels == null || hotels.Count == 0)
            {
                return NotFound();
            }

            List<HotelDTO> lHotelsDTO = ListHotelDTO(hotels);
            return Ok(lHotelsDTO);
        }
        
        /// <summary>Hotel by ID</summary>
        /// <returns>Hotel information</returns>
        /// <response code="200">Hotel found</response>
        /// <response code="404">No hotel</response>
        [HttpGet]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelByID([FromRoute(Name = "HotelID")] int hotelID)
        {
            var hotel = _hotelController.GetHotelByID(hotelID);
            if (hotel == null)
            {
                return NotFound();
            }

            HotelDTO hotelDTO = new HotelDTO(hotel);
            return Ok(hotelDTO);
        }

        /// <summary>Adding hotel</summary>
        /// <param name="hotelDTO">Hotel to add</param>
        /// <returns>Added hotel</returns>
        /// <response code="200">Hotel added</response>
        /// <response code="400">Add error</response>
        /// <response code="409">Constraint error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddHotel([FromBody] HotelUserDTO hotelDTO)
        {
            HotelBL aHotel = hotelDTO.GetHotel();
            ExitCode result = _hotelController.AddHotel(aHotel);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }

            HotelDTO addedHotel = new HotelDTO(aHotel);
            return Ok(addedHotel);
        }

        /// <summary>Updating hotel</summary>
        /// <param name="hotelDTO">Hotel to update</param>
        /// <returns>Updated hotel</returns>
        /// <response code="200">Hotel updated</response>
        /// <response code="400">Update error</response>
        /// <response code="409">Constraint error</response>
        [HttpPut]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateHotel([FromRoute(Name = "HotelID")] int hotelID, [FromBody] HotelUserDTO hotelDTO)
        {
            HotelBL uHotel = hotelDTO.GetHotel(hotelID);
            ExitCode result = _hotelController.UpdateHotel(uHotel);

            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }

            HotelDTO updatedHotel = new HotelDTO(uHotel);
            return Ok(updatedHotel);
        }

        /// <summary>Removing hotel by ID</summary>
        /// <returns>Removed hotel</returns>
        /// <response code="200">Hotel removed</response>
        /// <response code="400">Remove error</response>
        /// <response code="404">No hotel</response>
        [HttpDelete]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel([FromRoute(Name = "HotelID")] int hotelID)
        {
            HotelBL delHotel = _hotelController.GetHotelByID(hotelID);
            if (delHotel == null)
            {
                return NotFound();
            }
            
            ExitCode result = _hotelController.DeleteHotelByID(hotelID);
            if (result == ExitCode.Error)
            {
                return BadRequest();
            }
            
            HotelDTO hotel = new HotelDTO(delHotel);
            return Ok(hotel);
        }
    }
}
