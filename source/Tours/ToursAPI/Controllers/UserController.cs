using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsDB;
using ToursWeb.Controllers;
using ToursWeb.ModelsDTO;
using ToursWeb.ModelsBL;

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
        
        private List<UserTour> GetAllUserBookings(int userID)
        {
            int[] toursID = _userController.GetBookedTours(userID);
            if (toursID.Length == 0)
            {
                return null;
            }
            
            List<Tour> tours = new List<Tour>();
            foreach (int tour in toursID)
            {
                Tour curTour = _tourController.GetTourByID(tour);
                tours.Add(curTour);
            }

            List<UserTour> lTours = _tourController.ToUserTour(tours);
            return lTours;
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
        
        /// <summary>User by ID</summary>
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

        /// <summary>User tours</summary>
        /// <returns>Tours information</returns>
        /// <response code="200">Tours found</response>
        /// <response code="404">No tours</response>
        [HttpGet]
        [Route("{UserID:int}/Tour")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTour>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllBookings([FromRoute(Name = "UserID")] int userID)
        {
            List<UserTour> lTours = GetAllUserBookings(userID);
            if (lTours == null)
            {
                return NotFound();
            }
            
            return Ok(lTours);
        }

        public enum Action
        {
            Book,
            Cancel
        }

        /// <summary>Manage tours</summary>
        /// <returns>Tours information</returns>
        /// <response code="200">Tours updated</response>
        /// <response code="204">No booked tours</response>
        /// <response code="400">Error manage</response>
        /// <response code="409">Constraint error</response>
        [HttpPatch]
        [Route("{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTour>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult ManageTours([FromQuery(Name = "Action"), Required] Action action, 
            [FromRoute(Name = "UserID")] int userID, [FromQuery(Name = "TourID"), Required] int tourID)
        {
            if (_userController.GetAllUserInfo(userID) == null)
            {
                return BadRequest();
            }
            
            int[] toursID = _userController.GetBookedTours(userID);
            int size = toursID.Length;

            bool isExists = false;
            for (int i = 0; i < size && !isExists; i++)
            {
                if (toursID[i] == tourID)
                {
                    isExists = true;
                }
            }

            ExitCode result = ExitCode.Success;
            if (action == Action.Book)
            {
                if (isExists)
                {
                    return BadRequest();
                }
                result = _userController.BookTour(userID, tourID);
            }
            else
            {
                if (!isExists)
                {
                    return BadRequest();
                }
                result = _userController.CancelTour(userID, tourID);
            }
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }

            List<UserTour> lTours = GetAllUserBookings(userID);
            if (lTours == null)
            {
                return NoContent();
            }
            return Ok(lTours);
        }
    }
}