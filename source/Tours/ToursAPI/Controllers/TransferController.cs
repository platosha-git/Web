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
        /// Список всех трансферов
        /// </summary>
        /// <returns>Информация о всех трансферах</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransferDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTransfer()
        {
            var transfers = _transferController.GetAllTransfer();
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
