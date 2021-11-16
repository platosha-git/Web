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
    
    public class ApiBusController : ControllerBase
    {
        private readonly BusController _busController;

        public ApiBusController(BusController busController) 
        {
            _busController = busController;
        }

        private List<BusticketDTO> ListBusDTO(List<Busticket> lBuses) 
        {
            List<BusticketDTO> lBusesDTO = new List<BusticketDTO>(); 
            foreach (var bus in lBuses)
            {
                BusticketDTO busDTO = new BusticketDTO(bus);
                lBusesDTO.Add(busDTO);
            }

            return lBusesDTO;
        }
            
        /// <summary>
        /// Список всех автобусов
        /// </summary>
        /// <returns>Информация о всех автобусох</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BusticketDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllBuses()
        {
            var buses = _busController.GetAllBuses();
            if (buses == null)
            {
                return NotFound();
            }

            List<BusticketDTO> lBusesDTO = ListBusDTO(buses);
            return Ok(lBusesDTO);
        }
        
        /// <summary>
        /// Автобус по ключу
        /// </summary>
        /// <param name="busID">ИД автобуса</param>
        /// <returns>Информация об автобусе по ключу</returns>
        [HttpGet]
        [Route("{BusID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBusByID([FromRoute(Name = "BusID")] int busID)
        {
            var bus = _busController.GetBusByID(busID);
            if (bus == null)
            {
                return NotFound();
            }

            BusticketDTO busDTO = new BusticketDTO(bus);
            return Ok(busDTO);
        }
        
        /// <summary>
        /// Добавление автобуса
        /// </summary>
        /// <param name="busDTO">Добавляемый автобус</param>
        /// <returns>Результат добавления</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusticketDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddBus([FromBody] BusticketDTO busDTO)
        {
            Busticket aBus = busDTO.GetBusticket();
            _busController.AddBus(aBus);

            Busticket bus = _busController.GetBusByID(aBus.Bustid); 
            if (bus == null) 
            {
                return BadRequest();
            }

            BusticketDTO addedBus = new BusticketDTO(bus);
            return Ok(addedBus);
        }

        /// <summary>
        /// Обновление автобуса
        /// </summary>
        /// <param name="busDTO">Обновляемый автобус</param>
        /// <returns>Результат обновления</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBus([FromBody] BusticketDTO busDTO)
        {
            Busticket uBus = busDTO.GetBusticket();
            _busController.UpdateBus(uBus);

            Busticket bus = _busController.GetBusByID(busDTO.Bustid); 
            if (!busDTO.AreEqual(bus))
            {
                return NotFound();
            }

            BusticketDTO updatedBus = new BusticketDTO(bus);
            return Ok(updatedBus);
        }

        /// <summary>
        /// Удаление автобуса по ключу
        /// </summary>
        /// <param name="busID">ИД автобуса</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete]
        [Route("{BusID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBus([FromRoute(Name = "BusID")] int busID)
        {
            Busticket delBus = _busController.GetBusByID(busID);
            if (delBus == null)
            {
                return NotFound();
            }
            
            _busController.DeleteBusByID(busID);
            BusticketDTO bus = new BusticketDTO(delBus);
            return Ok(bus);
        }
    }
}