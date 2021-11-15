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

    public class ApiFoodController : ControllerBase
    {
        private readonly FoodController _foodController;

        public ApiFoodController(FoodController foodController)
        {
            _foodController = foodController;
        }

        private List<FoodDTO> ListFoodDTO(List<Food> lFood)
        {
            List<FoodDTO> lFoodDTO = new List<FoodDTO>();
            foreach (var food in lFood)
            {
                FoodDTO foodDTO = new FoodDTO(food);
                lFoodDTO.Add(foodDTO);
            }
            return lFoodDTO;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FoodDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllFood()
        {
            var food = _foodController.GetAllFood();
            if (food == null)
            {
                return NotFound();
            }

            List<FoodDTO> lFoodDTO = ListFoodDTO(food);
            return Ok(lFoodDTO);
        }
        
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

        [HttpGet]
        [Route("VegMenu/{VegMenu:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FoodDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFoodByVegMenu([FromRoute(Name = "VegMenu")] bool vegMenu)
        {
            var food = _foodController.GetFoodByVegMenu(vegMenu);
            if (food == null)
            {
                return NotFound();
            }

            List<FoodDTO> lFoodDTO = ListFoodDTO(food);
            return Ok(lFoodDTO);
        }

        [HttpGet]
        [Route("Bar/{Bar:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FoodDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFoodByBar([FromRoute(Name = "Bar")] bool bar)
        {
            var food = _foodController.GetFoodByBar(bar);
            if (food == null)
            {
                return NotFound();
            }

            List<FoodDTO> lFoodDTO = ListFoodDTO(food);
            return Ok(lFoodDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddFood([FromBody] FoodDTO foodDTO)
        {
            Food aFood = foodDTO.GetFood();
            _foodController.AddFood(aFood);

            Food food = _foodController.GetFoodByID(aFood.Foodid); 
            if (food == null) 
            {
                return BadRequest();
            }

            FoodDTO addedFood = new FoodDTO(food);
            return Ok(addedFood);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateFood([FromBody] FoodDTO foodDTO)
        {
            Food uFood = foodDTO.GetFood();
            _foodController.UpdateFood(uFood);

            Food food = _foodController.GetFoodByID(foodDTO.Foodid); 
            if (!foodDTO.AreEqual(food))
            {
                return NotFound();
            }

            FoodDTO updatedFood = new FoodDTO(food);
            return Ok(updatedFood);
        }

        [HttpDelete]
        [Route("{FoodID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteFood([FromRoute(Name = "FoodID")] int foodID)
        {
            Food delFood = _foodController.GetFoodByID(foodID);
            if (delFood == null)
            {
                return NotFound();
            }
            
            _foodController.DeleteFoodByID(foodID);
            FoodDTO food = new FoodDTO(delFood);
            return Ok(food);
        }
    }
}