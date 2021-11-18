using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsDB;
using ToursWeb.Controllers;
using ToursAPI.ModelsDTO;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Users")]
    
    public class ApiUserController : ControllerBase
    {
        private readonly UserController _userController;
        private readonly TourController _tourController;

        public ApiUserController(UserController userController, TourController tourController)
        {
            _userController = userController;
            _tourController = tourController;
        }

        private List<UserDTO> ListUsersDTO(List<User> lUsers)
        {
            List<UserDTO> lUsersDTO = new List<UserDTO>();
            foreach (var user in lUsers)
            {
                UserDTO userDTO = new UserDTO(user);
                lUsersDTO.Add(userDTO);
            }
            return lUsersDTO;
        }
        
        /// <summary>Users by params</summary>
        /// <returns>Users information</returns>
        /// <response code="200">Users found</response>
        /// <response code="404">No users</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers([FromQuery(Name = "Login")] string login = null, [FromQuery(Name = "Password")] string password = null)
        {
            List<User> users = _userController.GetAllUsers();
            if (login != null && password != null)
            {
                User user = _userController.GetUserByLP(login, password);
                List<User> newUsers = new List<User>();
                newUsers.Add(user);
                users = newUsers;
            }
            
            if (users == null)
            {
                return NotFound();
            }

            List<UserDTO> lUsersDTO = ListUsersDTO(users);
            return Ok(lUsersDTO);
        }
        
        /// <summary>User by params</summary>
        /// <returns>User information</returns>
        /// <response code="200">User found</response>
        /// <response code="404">No user</response>
        [HttpGet]
        [Route("{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserInfo([FromRoute(Name = "UserID")] int userID)
        {
            User user = _userController.GetAllUserInfo(userID);
            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO(user);
            return Ok(userDTO);
        }
        
        /*[HttpGet]
        [Route("Tours/{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTourDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllBookings([FromRoute(Name = "UserID")] int userID)
        {
            int[] toursID = _userController.GetBookedTours(userID);
            if (toursID == null)
            {
                return NotFound();
            }
            
            List<Tour> tours = new List<Tour>();
            foreach (int tour in toursID)
            {
                Tour curTour = _tourController.GetTourByID(tour);
                tours.Add(curTour);
            }

            List<UserTourDTO> lTours = null;//ListUserTours(tours);
            return Ok(lTours);
        }
        */
    }
}