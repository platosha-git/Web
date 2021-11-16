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
    
    public class ApiUserController : ControllerBase
    {
        private readonly UserController _userController;

        public ApiUserController(UserController userController)
        {
            _userController = userController;
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
        
        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns>Информация о всех пользователях</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsers()
        {
            var users = _userController.GetAllUsers();
            if (users == null)
            {
                return NotFound();
            }

            List<UserDTO> lUsersDTO = ListUsersDTO(users);
            return Ok(lUsersDTO);
        }
        
        /// <summary>
        /// Пользователь по ключу
        /// </summary>
        /// <param name="userID">ИД пользователя</param>
        /// <returns>Информация о пользователе по ключу</returns>
        [HttpGet]
        [Route("{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserInfo([FromRoute(Name = "UserID")] int userID)
        {
            var user = _userController.GetAllUserInfo(userID);
            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO(user);
            return Ok(userDTO);
        }
    }
}