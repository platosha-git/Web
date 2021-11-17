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
        
        /// <summary>
        /// Список всего питания
        /// </summary>
        /// <returns>Информация обо всем питании</returns>
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
        
        /// <summary>
        /// Питание по ключу
        /// </summary>
        /// <param name="foodID">ИД питания</param>
        /// <returns>Информация о питании по ключу</returns>
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

        /// <summary>
        /// Список питания в зависимости от меню
        /// </summary>
        /// <param name="menu">Наличие вегетарианского меню</param>
        /// <returns>Информация о питании в зависимости от меню</returns>
        [HttpGet]
        [Route("VegMenu/{Menu}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FoodDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFoodByVegMenu([FromRoute(Name = "Menu")] string menu)
        {
            var food = _foodController.GetFoodByMenu(menu);
            if (food == null)
            {
                return NotFound();
            }

            List<FoodDTO> lFoodDTO = ListFoodDTO(food);
            return Ok(lFoodDTO);
        }

        /// <summary>
        /// Список питания в зависимости от бара
        /// </summary>
        /// <param name="bar">Наличие бара</param>
        /// <returns>Информация о питании в зависимости от бара</returns>
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

        /// <summary>
        /// Добавление питания
        /// </summary>
        /// <param name="foodDTO">Добавляемое питание</param>
        /// <returns>Результат добавления</returns>
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

        /// <summary>
        /// Обновление питания
        /// </summary>
        /// <param name="foodDTO">Обновляемый элемент</param>
        /// <returns>Результат обновления</returns>
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

        /// <summary>
        /// Удаление питания по ключу
        /// </summary>
        /// <param name="foodID">ИД питания для удаления</param>
        /// <returns>Результат удаления</returns>
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