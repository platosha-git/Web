using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb;
using ToursWeb.ComponentsBL;
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
        
        /*--------------------------------------------------------------
         *                          Tours
         * -----------------------------------------------------------*/
        
        [HttpGet]
        [Route("Tours")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTours()
        {
            var tours = _userController.GetAllTours();
            if (tours == null) 
            {
                return NotFound();
            }
            return Ok(tours);
        }

        [HttpGet]
        [Route("FullTour/{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FullUserTour))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetFullTour([FromRoute(Name = "TourID")] int tourID)
        {
            var fullTour = _userController.GetFullTour(tourID);
            if (fullTour == null)
            {
                return NotFound();
            }
            return Ok(fullTour);
        }

        [HttpGet]
        [Route("TourByDate/{DateBegin}/{DateEnd}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetToursByDate([FromRoute(Name = "DateBegin")] string dBegin, 
                                            [FromRoute(Name = "DateEnd")] string dEnd)
        {
            //01-12-2020    01-12-2021
            DateTime beg = Convert.ToDateTime(dBegin);
            DateTime end = Convert.ToDateTime(dEnd);
            
            var tours = _userController.GetToursByDate(beg, end);
            if (tours == null)
            {
                return NotFound();
            }
            return Ok(tours);
        }
        
        [HttpGet]
        [Route("TourByCity/{City}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetToursByCity([FromRoute(Name = "City")] string city)
        {
            var tours = _userController.GetToursByCity("Москва");
            if (tours == null)
            {
                return NotFound();
            }
            return Ok(tours);
        }

        /*--------------------------------------------------------------
         *                          Hotels
         * -----------------------------------------------------------*/
        [HttpGet]
        [Route("Hotels")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllHotels()
        {
            var hotels = _userController.GetAllHotels();
            if (hotels == null)
            {
                return NotFound();
            }
            return Ok(hotels);
        }
        
        [HttpGet]
        [Route("HotelByID/{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelByID([FromRoute(Name = "HotelID")] int hotelID)
        {
            var hotel = _userController.GetHotelByID(hotelID);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }
        
        [HttpGet]
        [Route("HotelByCity/{City}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelsByCity([FromRoute(Name = "City")] string city)
        {
            var hotels = _userController.GetHotelsByCity("Москва");
            if (hotels == null)
            {
                return NotFound();
            }
            return Ok(hotels);
        }
        
        [HttpGet]
        [Route("HotelByName/{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelByName([FromRoute(Name = "Name")] string name)
        {
            var hotel = _userController.GetHotelByName(name);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }
        
        [HttpGet]
        [Route("HotelsByType/{Type}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelsByType([FromRoute(Name = "Type")] string type)
        {
            var hotels = _userController.GetHotelsByType("Апартамент");
            if (hotels == null)
            {
                return NotFound();
            }
            return Ok(hotels);
        }
        
        [HttpGet]
        [Route("HotelsByClass/{Class:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelsByClass([FromRoute(Name = "Class")] int cls)
        {
            var hotels = _userController.GetHotelsByClass(cls);
            if (hotels == null)
            {
                return NotFound();
            }
            return Ok(hotels);
        }
        
        [HttpGet]
        [Route("HotelsBySwimPool/{SP:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Hotel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelsBySwimPool([FromRoute(Name = "SP")] bool sp)
        {
            var hotels = _userController.GetHotelsBySwimPool(sp);
            if (hotels == null)
            {
                return NotFound();
            }
            return Ok(hotels);
        }

        /*--------------------------------------------------------------
         *                          Food
         * -----------------------------------------------------------*/
        /*public List<Food> GetAllFood()
        public Food GetFoodByID(int foodID)
        public List<Food> GetFoodByCategory(string cat)
        public List<Food> GetFoodByVegMenu(bool vm)
        public List<Food> GetFoodByChildMenu(bool cm)
        public List<Food> GetFoodByBar(bool bar)
        
        //public Transfer GetTransferByID(int transfID)
        */

        /*--------------------------------------------------------------
         *                          BusTicket
         * -----------------------------------------------------------*/
        /*public List<Busticket> GetAllBuses()
        public Busticket GetBusByID(int busID)
        public List<Busticket> GetBusesByCityFrom(string city)
        public List<Busticket> GetBusesByCityTo(string city)
        public List<Busticket> GetBusesByDate(DateTime date)
        */

        /*--------------------------------------------------------------
         *                          PlaneTicket
         * -----------------------------------------------------------*/
        /*public List<Planeticket> GetAllPlanes()
        public Planeticket GetPlaneByID(int planeID)
        public List<Planeticket> GetPlanesByCityFrom(string city)
        public List<Planeticket> GetPlanesByCityTo(string city)
        public List<Planeticket> GetPlanesByDate(DateTime date)
        */

        /*--------------------------------------------------------------
         *                          TrainTicket
         * -----------------------------------------------------------*/
        /*public List<Trainticket> GetAllTrains()
        public Trainticket GetTrainByID(int trainID)
        public List<Trainticket> GetTrainsByCityFrom(string city)
        public List<Trainticket> GetTrainsByCityTo(string city)
        public List<Trainticket> GetTrainsByDate(DateTime date)
        */
    }
}