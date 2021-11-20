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

        /// <summary>Transfers by parameters</summary>
        /// <returns>Transfers information</returns>
        /// <response code="200">Transfers found</response>
        /// <response code="404">No transfers</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransferDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTransfer([FromQuery(Name = "Type")] TType? type = null,
            [FromQuery(Name = "City from")] string cityFrom = null, [FromQuery(Name = "dd-mm-yyyy")] string date = null)
        {
            List<Transfer> transfers = _transferController.GetAllTransfer();
            if (transfers != null)
            {
                if (type != null)
                {
                    transfers = _transferController.GetTransferByType(type.ToString());
                }

                if (cityFrom != null)
                {
                    List<Transfer> transfersCities = _transferController.GetTransfersByCity(cityFrom);
                    List<Transfer> res1 = transfers.Intersect(transfersCities).ToList();
                    transfers = res1;
                }

                if (date != null)
                {
                    DateTime dateTr = Convert.ToDateTime(date);
                    List<Transfer> transfersDate = _transferController.GetTransfersByDate(dateTr);
                    List<Transfer> res2 = transfers.Intersect(transfersDate).ToList();
                    transfers = res2;
                }
            }

            if (transfers == null || transfers.Count == 0)
            {
                return NotFound();
            }

            List<TransferDTO> lTtransfersDTO = ListTransferDTO(transfers);
            return Ok(lTtransfersDTO);
        }

        /// <summary>Transfer by ID</summary>
        /// <returns>Transfer information</returns>
        /// <response code="200">Transfer found</response>
        /// <response code="404">No transfer</response>
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

        /// <summary>Adding transfer</summary>
        /// <param name="transferDTO">Transfer to add</param>
        /// <returns>Added transfer</returns>
        /// <response code="200">Transfer added</response>
        /// <response code="400">Add error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTransfer([FromBody] TransferUserDTO transferDTO)
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

        /// <summary>Updating transfer</summary>
        /// <param name="transferDTO">Transfer to update</param>
        /// <returns>Updated transfer</returns>
        /// <response code="200">Transfer updated</response>
        /// <response code="400">Update error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTransfer([FromBody] TransferDTO transferDTO)
        {
            Transfer uTransfer = transferDTO.GetTransfer(transferDTO.Transferid);
            _transferController.UpdateTransfer(uTransfer);

            Transfer transfer = _transferController.GetTransferByID(transferDTO.Transferid); 
            if (!transferDTO.AreEqual(transfer))
            {
                return NotFound();
            }

            TransferDTO updatedTransfer = new TransferDTO(transfer);
            return Ok(updatedTransfer);
        }

        /// <summary>Removing transfer by ID</summary>
        /// <returns>Removed transfer</returns>
        /// <response code="200">Transfer removed</response>
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
