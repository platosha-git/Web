using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.Controllers;
using ToursWeb.ModelsDTO;
using ToursWeb.ModelsBL;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Foods")]

    public class ApiFoodController : ControllerBase
    {
        private readonly FoodController _foodController;

        public ApiFoodController(FoodController foodController)
        {
            _foodController = foodController;
        }

        private List<FoodDTO> ListFoodDTO(List<FoodBL> foods)
        {
            List<FoodDTO> foodsDTO = new List<FoodDTO>();
            foreach (var food in foods)
            {
                FoodDTO foodDTO = new FoodDTO(food);
                foodsDTO.Add(foodDTO);
            }

            return foodsDTO;
        }
        
        /// <summary>Foods by parameters</summary>
        /// <param name="category">Breakfast, Half board, Full board, All inclusive, Continental breakfast, American breakfast</param>
        /// <param name="menu"></param>
        /// <param name="bar"></param>
        /// <returns>Foods information</returns>
        /// <response code="200">Foods found</response>
        /// <response code="404">No food</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FoodBL>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Nullable))]
        public IActionResult GetAllFood([FromQuery(Name = "Category")] string category = null,
            [FromQuery(Name = "Menu")] FMenu? menu = null, [FromQuery(Name = "Bar")] bool? bar = null)
        {
            List<FoodBL> foods = _foodController.GetAllFood();
            if (foods != null)
            {
                if (category != null)
                {
                    foods = _foodController.GetFoodByCategory(category);
                }

                if (menu != null)
                {
                    List<FoodBL> foodsMenu = _foodController.GetFoodByMenu(menu.ToString());
                    List<FoodBL> res1 = foods.Intersect(foodsMenu).ToList();
                    foods = res1;
                }

                if (bar != null)
                {
                    List<FoodBL> foodsBar = _foodController.GetFoodByBar((bool) bar);
                    List<FoodBL> res2 = foods.Intersect(foodsBar).ToList();
                    foods = res2;
                }
            }

            if (foods == null || foods.Count == 0)
            {
                return NotFound();
            }
            
            List<FoodDTO> lFoodDTO = ListFoodDTO(foods);
            return Ok(lFoodDTO);
        }
        
        /// <summary>Food by ID</summary>
        /// <returns>Food information</returns>
        /// <response code="200">Food found</response>
        /// <response code="404">No food</response>
        [HttpGet]
        [Route("{FoodID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFoodByID([FromRoute(Name = "FoodID")] int foodID)
        {
            var food = _foodController.GetFoodByID(foodID);
            if (food == null)
            {
                return NotFound();
            }

            FoodDTO foodDTO = new FoodDTO(food);
            return Ok(foodDTO);
        }

        /// <summary>Adding food</summary>
        /// <param name="foodDTO">Food to add</param>
        /// <returns>Added food</returns>
        /// <response code="200">Food added</response>
        /// <response code="400">Add error</response>
        /// <response code="409">Constraint error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddFood([FromBody] FoodUserDTO foodDTO)
        {
            FoodBL aFood = foodDTO.GetFood();
            ExitCode result = _foodController.AddFood(aFood);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }
            
            FoodDTO addedFood = new FoodDTO(aFood);
            return Ok(addedFood);
        }

        /// <summary>Updating food</summary>
        /// <param name="foodDTO">Food to update</param>
        /// <returns>Updated food</returns>
        /// <response code="200">Food updated</response>
        /// <response code="400">Update error</response>
        /// <response code="409">Constraint error</response>
        [HttpPut]
        [Route("{FoodID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateFood([FromRoute(Name = "FoodID")] int foodID, [FromBody] FoodUserDTO foodDTO)
        {
            FoodBL uFood = foodDTO.GetFood(foodID);
            ExitCode result = _foodController.UpdateFood(uFood);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return BadRequest();
            }

            FoodDTO updatedFood = new FoodDTO(uFood);
            return Ok(updatedFood);
        }

        /// <summary>Removing food by ID</summary>
        /// <returns>Removed food</returns>
        /// <response code="200">Food removed</response>
        /// <response code="400">Remove error</response>
        /// <response code="404">No food</response>
        [HttpDelete]
        [Route("{FoodID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteFood([FromRoute(Name = "FoodID")] int foodID)
        {
            FoodBL delFood = _foodController.GetFoodByID(foodID);
            if (delFood == null)
            {
                return NotFound();
            }
            
            ExitCode result = _foodController.DeleteFoodByID(foodID);
            if (result == ExitCode.Error)
            {
                return BadRequest();
            }
            
            FoodDTO food = new FoodDTO(delFood);
            return Ok(food);
        }
    }
}
