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
        
        [Route("Tour")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tour>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAllTours()
        {
            var tours = _userController.GetAllTours();
            if (tours == null) {
                return NotFound();
            }
            return Ok(tours);
        }
        
        /*public FullUserTour GetFullTour(int TID)
        public List<Tour> GetToursByDate(DateTime beg, DateTime end)
        public List<Tour> GetToursByCity(string city)
        */

        /*--------------------------------------------------------------
         *                          Hotels
         * -----------------------------------------------------------*/
        /*public List<Hotel> GetAllHotels()
        public Hotel GetHotelByID(int hotelID)
        public List<Hotel> GetHotelsByCity(string city)
        public Hotel GetHotelByName(string name)
        public List<Hotel> GetHotelsByType(string type)
        public List<Hotel> GetHotelsByClass(int cls)
        public List<Hotel> GetHotelsBySwimPool(bool sp)
        */

        /*--------------------------------------------------------------
         *                          Food
         * -----------------------------------------------------------*/
        /*public List<Food> GetAllFood()
        public Food GetFoodByID(int foodID)
        public List<Food> GetFoodByCategory(string cat)
        public List<Food> GetFoodByVegMenu(bool vm)
        public List<Food> GetFoodByChildMenu(bool cm)
        public List<Food> GetFoodByBar(bool bar)
        public Transfer GetTransferByID(int transfID)
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