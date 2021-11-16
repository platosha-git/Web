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
    
    public class ApiPlaneController : ControllerBase
    {
        private readonly PlaneController _planeController;

        public ApiPlaneController(PlaneController planeController) 
        {
            _planeController = planeController;
        }

        private List<PlaneticketDTO> ListPlaneDTO(List<Planeticket> lPlanes) 
        {
            List<PlaneticketDTO> lPlanesDTO = new List<PlaneticketDTO>(); 
            foreach (var plane in lPlanes)
            {
                PlaneticketDTO planeDTO = new PlaneticketDTO(plane);
                lPlanesDTO.Add(planeDTO);
            }

            return lPlanesDTO;
        }
            
        /// <summary>
        /// Список всех самолетов
        /// </summary>
        /// <returns>Информация о всех самолетах</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PlaneticketDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllPlanes()
        {
            var planes = _planeController.GetAllPlanes();
            if (planes == null)
            {
                return NotFound();
            }

            List<PlaneticketDTO> lPlanesDTO = ListPlaneDTO(planes);
            return Ok(lPlanesDTO);
        }
        
        /// <summary>
        /// Самолет по ключу
        /// </summary>
        /// <param name="planeID">ИД самолета</param>
        /// <returns>Информация о самолете по ключу</returns>
        [HttpGet]
        [Route("{PlaneID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlaneticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPlaneByID([FromRoute(Name = "PlaneID")] int planeID)
        {
            var plane = _planeController.GetPlaneByID(planeID);
            if (plane == null)
            {
                return NotFound();
            }

            PlaneticketDTO planeDTO = new PlaneticketDTO(plane);
            return Ok(planeDTO);
        }
        
        /// <summary>
        /// Добавление самолета
        /// </summary>
        /// <param name="planeDTO">Добавляемый самолет</param>
        /// <returns>Результат добавления</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlaneticketDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPlane([FromBody] PlaneticketDTO planeDTO)
        {
            Planeticket aPlane = planeDTO.GetPlaneticket();
            _planeController.AddPlane(aPlane);

            Planeticket plane = _planeController.GetPlaneByID(aPlane.Planetid); 
            if (plane == null) 
            {
                return BadRequest();
            }

            PlaneticketDTO addedPlane = new PlaneticketDTO(plane);
            return Ok(addedPlane);
        }

        /// <summary>
        /// Обновление самолета
        /// </summary>
        /// <param name="planeDTO">Обновляемый самолет</param>
        /// <returns>Результат обновления</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlaneticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePlane([FromBody] PlaneticketDTO planeDTO)
        {
            Planeticket uPlane = planeDTO.GetPlaneticket();
            _planeController.UpdatePlane(uPlane);

            Planeticket plane = _planeController.GetPlaneByID(planeDTO.Planetid); 
            if (!planeDTO.AreEqual(plane))
            {
                return NotFound();
            }

            PlaneticketDTO updatedPlane = new PlaneticketDTO(plane);
            return Ok(updatedPlane);
        }

        /// <summary>
        /// Удаление самолета по ключу
        /// </summary>
        /// <param name="planeID">ИД самолета</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete]
        [Route("{PlaneID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlaneticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePlane([FromRoute(Name = "PlaneID")] int planeID)
        {
            Planeticket delPlane = _planeController.GetPlaneByID(planeID);
            if (delPlane == null)
            {
                return NotFound();
            }
            
            _planeController.DeletePlaneByID(planeID);
            PlaneticketDTO plane = new PlaneticketDTO(delPlane);
            return Ok(plane);
        }
    }
}