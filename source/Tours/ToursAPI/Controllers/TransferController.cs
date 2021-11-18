using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsDB;
using ToursWeb.Controllers;
using ToursAPI.ModelsDTO;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Transfers")]

    public class ApiTransferController : ControllerBase
    {
        private readonly TransferController _transferController;

        public ApiTransferController(TransferController transferController)
        {
            _transferController = transferController;
        }

        private List<TransferDTO> ListTransferDTO(List<Transfer> lTransfers)
        {
            List<TransferDTO> lTransfersDTO = new List<TransferDTO>();
            foreach (var transfer in lTransfers)
            {
                TransferDTO transferDTO = new TransferDTO(transfer);
                lTransfersDTO.Add(transferDTO);
            }
            return lTransfersDTO;
        }

        /// <summary>
        /// Список трафнсферов в соответствии с параметрами
        /// </summary>
        /// <returns>Информация о всех трансферах</returns>
        /// <response code="200">Трфнсфер найден</response>
        /// <response code="404">Трансфер отсутсвует</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransferDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTransfer([FromQuery(Name = "Type")] TType? type = null,
            [FromQuery(Name = "City from")] string? cityFrom = null, [FromQuery(Name = "City to")] string cityTo = null,
            [FromQuery(Name = "Departure date")] DateTime? date = null)
        {
            List<Transfer> transfers = _transferController.GetAllTransfer();
            if (transfers != null)
            {
                if (type != null)
                {
                    transfers = _transferController.GetTransferByType(type.ToString());
                }

                if (cityFrom != null && cityTo != null)
                {
                    List<Transfer> transfersCities = _transferController.GetTransfersByCities(cityFrom, cityTo);
                    List<Transfer> res1 = transfers.Intersect(transfersCities).ToList();
                    transfers = res1;
                }

                if (date != null)
                {
                    List<Transfer> transfersDate = _transferController.GetTransfersByDate((DateTime)date);
                    List<Transfer> res2 = transfers.Intersect(transfersDate).ToList();
                    transfers = res2;
                }
                
            }

            if (transfers == null)
            {
                return NotFound();
            }

            List<TransferDTO> lTtransfersDTO = ListTransferDTO(transfers);
            return Ok(lTtransfersDTO);
        }

        /// <summary>
        /// Трансфер по ключу
        /// </summary>
        /// <param name="transferID">ИД трансфера</param>
        /// <returns>Информация о трансфере по ключу</returns>
        /// <response code="200">Transfer found</response>
        /// <response code="404">No transfers</response>
        [HttpGet]
        [Route("{TransferID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTransferByID([FromRoute(Name = "TransferID")] int transferID)
        {
            var transfer = _transferController.GetTransferByID(transferID);
            if (transfer == null)
            {
                return NotFound();
            }

            TransferDTO transferDTO = new TransferDTO(transfer);
            return Ok(transferDTO);
        }

        /// <summary>
        /// Добавление трансфера
        /// </summary>
        /// <param name="transferDTO">Добавляемый трансфер</param>
        /// <returns>Результат добавления</returns>
        /// <response code="200">Transfer added</response>
        /// <response code="400">Add error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTransfer([FromBody] TransferDTO transferDTO)
        {
            Transfer aTransfer = transferDTO.GetTransfer();
            _transferController.AddTransfer(aTransfer);

            Transfer transfer = _transferController.GetTransferByID(aTransfer.Transferid); 
            if (transfer == null) 
            {
                return BadRequest();
            }

            TransferDTO addedTransfer = new TransferDTO(transfer);
            return Ok(addedTransfer);
        }

        /// <summary>
        /// Обновление трансфера
        /// </summary>
        /// <param name="transferDTO">Обновляемый трансфер</param>
        /// <returns>Результат обновления</returns>
        /// <response code="200">Transfer updated</response>
        /// <response code="400">Update error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTransfer([FromBody] TransferDTO transferDTO)
        {
            Transfer uTransfer = transferDTO.GetTransfer();
            _transferController.UpdateTransfer(uTransfer);

            Transfer transfer = _transferController.GetTransferByID(transferDTO.Transferid); 
            if (!transferDTO.AreEqual(transfer))
            {
                return NotFound();
            }

            TransferDTO updatedTransfer = new TransferDTO(transfer);
            return Ok(updatedTransfer);
        }

        /// <summary>
        /// Удаление трансфера по ключу
        /// </summary>
        /// <param name="transferID">ИД трансфера</param>
        /// <returns>Результат удаления</returns>
        /// <response code="200">Transfer deleted</response>
        /// <response code="404">No transfer</response>
        [HttpDelete]
        [Route("{TransferID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTransfer([FromRoute(Name = "TransferID")] int transferID)
        {
            Transfer delTransfer = _transferController.GetTransferByID(transferID);
            if (delTransfer == null)
            {
                return NotFound();
            }
            
            _transferController.DeleteTransferByID(transferID);
            TransferDTO transfer = new TransferDTO(delTransfer);
            return Ok(transfer);
        }
    }
}
