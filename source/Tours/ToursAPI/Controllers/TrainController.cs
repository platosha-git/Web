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
    
    public class ApiTrainController : ControllerBase
    {
        private readonly TrainController _trainController;

        public ApiTrainController(TrainController trainController) 
        {
            _trainController = trainController;
        }

        private List<TrainticketDTO> ListTrainDTO(List<Trainticket> lTrains) 
        {
            List<TrainticketDTO> lTrainsDTO = new List<TrainticketDTO>(); 
            foreach (var train in lTrains)
            {
                TrainticketDTO trainDTO = new TrainticketDTO(train);
                lTrainsDTO.Add(trainDTO);
            }

            return lTrainsDTO;
        }
            
        /// <summary>
        /// Список всех поездов
        /// </summary>
        /// <returns>Информация о всех поездах</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TrainticketDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTrains()
        {
            var trains = _trainController.GetAllTrains();
            if (trains == null)
            {
                return NotFound();
            }

            List<TrainticketDTO> lTrainsDTO = ListTrainDTO(trains);
            return Ok(lTrainsDTO);
        }
        
        /// <summary>
        /// Поезд по ключу
        /// </summary>
        /// <param name="trainID">ИД поезда</param>
        /// <returns>Информация о поезде по ключу</returns>
        [HttpGet]
        [Route("{TrainID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTrainByID([FromRoute(Name = "TrainID")] int trainID)
        {
            var train = _trainController.GetTrainByID(trainID);
            if (train == null)
            {
                return NotFound();
            }

            TrainticketDTO trainDTO = new TrainticketDTO(train);
            return Ok(trainDTO);
        }
        
        /// <summary>
        /// Добавление поезда
        /// </summary>
        /// <param name="trainDTO">Добавляемый поезд</param>
        /// <returns>Результат добавления</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainticketDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTrain([FromBody] TrainticketDTO trainDTO)
        {
            Trainticket aTrain = trainDTO.GetTrainticket();
            _trainController.AddTrain(aTrain);

            Trainticket train = _trainController.GetTrainByID(aTrain.Traintid); 
            if (train == null) 
            {
                return BadRequest();
            }

            TrainticketDTO addedTrain = new TrainticketDTO(train);
            return Ok(addedTrain);
        }

        /// <summary>
        /// Обновление поезда
        /// </summary>
        /// <param name="trainDTO">Обновляемый поезд</param>
        /// <returns>Результат обновления</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTrain([FromBody] TrainticketDTO trainDTO)
        {
            Trainticket uTrain = trainDTO.GetTrainticket();
            _trainController.UpdateTrain(uTrain);

            Trainticket train = _trainController.GetTrainByID(trainDTO.Traintid); 
            if (!trainDTO.AreEqual(train))
            {
                return NotFound();
            }

            TrainticketDTO updatedTrain = new TrainticketDTO(train);
            return Ok(updatedTrain);
        }

        /// <summary>
        /// Удаление поезда по ключу
        /// </summary>
        /// <param name="trainID">ИД поезда</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete]
        [Route("{TrainID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrainticketDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrain([FromRoute(Name = "TrainID")] int trainID)
        {
            Trainticket delTrain = _trainController.GetTrainByID(trainID);
            if (delTrain == null)
            {
                return NotFound();
            }
            
            _trainController.DeleteTrainByID(trainID);
            TrainticketDTO train = new TrainticketDTO(delTrain);
            return Ok(train);
        }
    }
}