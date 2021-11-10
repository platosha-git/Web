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
    
    public class ApiManagerController : ControllerBase
    {
        private readonly ManagerController _managerController;

        public ApiManagerController(ManagerController managerController)
        {
            _managerController = managerController;
        }

        [HttpGet]
        [Route("Users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers()
        {
            var users = _managerController.GetAllUsers();
            if (users == null) 
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("Tour/{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tour))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTourByID([FromRoute(Name = "TourID")] int tourID)
        {
            var tour = _managerController.GetTourByID(tourID);
            if (tour == null)
            {
                return NotFound();
            }

            return Ok(tour);
        }

        /*--------------------------------------------------------------
         *                          Add
         * -----------------------------------------------------------*/
        [HttpPost]
        [Route("AddTour/{Cost:int}/{DateBegin}/{DateEnd}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddTour([FromRoute(Name = "Cost")] int cost, 
            [FromRoute(Name = "DateBegin")] string dBegin, 
            [FromRoute(Name = "DateEnd")] string dEnd)
        {
            DateTime beg = Convert.ToDateTime(dBegin);
            DateTime end = Convert.ToDateTime(dEnd);
            Tour nTour = new Tour { Tourid = 31, Food = 4, Hotel = 7, Transfer = 3, Cost = cost, Datebegin = beg, Dateend = end };
            
            _managerController.AddTour(nTour);
            if (_managerController.GetTourByID(nTour.Tourid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("AddHotel/{Name}/{Class:int}/{SwimPool:bool}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddHotel([FromRoute(Name = "Name")] string name, 
            [FromRoute(Name = "Class")] int cls, 
            [FromRoute(Name = "SwimPool")] bool sp,
            [FromRoute(Name = "Cost")] int cost)
        {
            Hotel nHotel = new Hotel { Hotelid = 31, Name = name, Type = "Отель", Class = cls, Swimpool = sp, City = "Москва", Cost = cost };
            
            _managerController.AddHotel(nHotel);
            if (_managerController.GetHotelByID(nHotel.Hotelid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("AddFood/{VegMenu:bool}/{ChildrenMenu:bool}/{Bar:bool}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddFood([FromRoute(Name = "VegMenu")] bool vegMenu, 
            [FromRoute(Name = "ChildrenMenu")] bool childMenu, 
            [FromRoute(Name = "Bar")] bool bar,
            [FromRoute(Name = "Cost")] int cost)
        {
            Food nFood = new Food { Foodid = 31, Category = "Завтрак", Vegmenu = vegMenu, Childrenmenu = childMenu, Bar = bar, Cost = cost };
            
            _managerController.AddFood(nFood);
            if (_managerController.GetFoodByID(nFood.Foodid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }

        /*--------------------------------------------------------------
         *                          Update
         * -----------------------------------------------------------*/
        [HttpPost]
        [Route("UpdateTour/{TourID:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddTour([FromRoute(Name = "TourID")] int tourID,
            [FromRoute(Name = "Cost")] int cost)
        {
            Tour tour = _managerController.GetTourByID(tourID);

            if (tour == null)
            {
                return NotFound();
            }

            tour.Cost = cost;
            _managerController.UpdateTour(tour);
            
            if (_managerController.GetTourByID(tourID).Cost != cost) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        /*public void UpdateTour(Tour ntour)
        public void UpdateHotel(Hotel nhotel)
        public void UpdateFood(Food nfood)
        */

        /*--------------------------------------------------------------
         *                          Delete
         * -----------------------------------------------------------*/
        /*public void DeleteTourByID(int tourID)
        public void DeleteHotelByID(int hotelID)
        public void DeleteHotelByName(string name)
        public void DeleteFoodByID(int foodID)
        */
    }
}