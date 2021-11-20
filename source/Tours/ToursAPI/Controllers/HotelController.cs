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
    [Route("/api/v1/Hotels")]

    public class ApiHotelController : ControllerBase
    {
        private readonly HotelController _hotelController;

        public ApiHotelController(HotelController hotelController)
        {
            _hotelController = hotelController;
        }

        private List<HotelDTO> ListHotelDTO(List<Hotel> lHotels)
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
            List<Hotel> hotels = _hotelController.GetAllHotels();
            if (hotels != null)
            {
                if (city != null)
                {
                    hotels = _hotelController.GetHotelsByCity(city);
                }

                if (cls != null)
                {
                    List<Hotel> hotelsCls = _hotelController.GetHotelsByClass((int)cls);
                    List<Hotel> res1 = hotels.Intersect(hotelsCls).ToList();
                    hotels = res1;
                }

                if (type != null)
                {
                    List<Hotel> hotelsType = _hotelController.GetHotelsByType(type);
                    List<Hotel> res2 = hotels.Intersect(hotelsType).ToList();
                    hotels = res2;
                }
                
                if (sp != null)
                {
                    List<Hotel> hotelsSP = _hotelController.GetHotelsBySwimPool((bool)sp);
                    List<Hotel> res3 = hotels.Intersect(hotelsSP).ToList();
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
        /// <param name="hotelUserDTO">Hotel to add</param>
        /// <returns>Added hotel</returns>
        /// <response code="200">Hotel added</response>
        /// <response code="400">Add error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddHotel([FromBody] HotelUserDTO hotelUserDTO)
        {
            Hotel aHotel = hotelUserDTO.GetHotel();
            _hotelController.AddHotel(aHotel);

            Hotel hotel = _hotelController.GetHotelByID(aHotel.Hotelid); 
            if (hotel == null) 
            {
                return BadRequest();
            }

            HotelDTO addedHotel = new HotelDTO(hotel);
            return Ok(addedHotel);
        }

        /// <summary>Updating hotel</summary>
        /// <param name="hotelDTO">Hotel to update</param>
        /// <returns>Updated hotel</returns>
        /// <response code="200">Hotel updated</response>
        /// <response code="400">Update error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateHotel([FromBody] HotelDTO hotelDTO)
        {
            Hotel uHotel = hotelDTO.GetHotel(hotelDTO.Hotelid);
            _hotelController.UpdateHotel(uHotel);

            Hotel hotel = _hotelController.GetHotelByID(hotelDTO.Hotelid); 
            if (!hotelDTO.AreEqual(hotel))
            {
                return NotFound();
            }

            HotelDTO updatedHotel = new HotelDTO(hotel);
            return Ok(updatedHotel);
        }

        /// <summary>Removing hotel by ID</summary>
        /// <returns>Removed hotel</returns>
        /// <response code="200">Hotel removed</response>
        /// <response code="404">No hotel</response>
        [HttpDelete]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel([FromRoute(Name = "HotelID")] int hotelID)
        {
            Hotel delHotel = _hotelController.GetHotelByID(hotelID);
            if (delHotel == null)
            {
                return NotFound();
            }
            
            _hotelController.DeleteHotelByID(hotelID);
            HotelDTO hotel = new HotelDTO(delHotel);
            return Ok(hotel);
        }
    }
}
