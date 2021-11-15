/*using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb;
using ToursWeb.ComponentsBL;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ApiTransferManagerController : ControllerBase
    {
        private readonly TransferManagerController _transferManagerController;

        public ApiTransferManagerController(TransferManagerController transferManagerController)
        {
            _transferManagerController = transferManagerController;
        }

        [HttpGet]
        [Route("Transfer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Transfer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTransfer()
        {
            var transfer = _transferManagerController.GetAllTransfer();
            if (transfer == null) 
            {
                return NotFound();
            }
            return Ok(transfer);
        }
*/
        /*--------------------------------------------------------------
         *                          Add
         * -----------------------------------------------------------*/
/*
    [HttpPost]
        [Route("AddTransfer/{PlaneTID:int}/{TrainTID:int}/{BusTID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddTransfer([FromRoute(Name = "PlaneTID")] int planeTID, 
            [FromRoute(Name = "TrainTID")] int trainTID, 
            [FromRoute(Name = "BusTID")] int busTID)
        {
            Transfer nTransfer = new Transfer { Transferid = 31, Planeticket = planeTID, Trainticket = trainTID, Busticket = busTID};

            _transferManagerController.AddTransfer(nTransfer);
            if (_transferManagerController.GetTransferByID(nTransfer.Transferid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("AddBus/{Bus:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddBus([FromRoute(Name = "Bus")] int bus, 
            [FromRoute(Name = "Cost")] int cost)
        {
            DateTime dep = Convert.ToDateTime("11-12-2021");
            DateTime arr = Convert.ToDateTime("12-12-2021");
            
            Busticket nBus = new Busticket { Bustid = 31, Bus = bus, Seat = 1, Cityfrom = "Москва", Cityto = "Воронеж", 
                Departuretime = dep, Arrivaltime = arr, Luggage = false, Cost = cost };

            _transferManagerController.AddBus(nBus);
            if (_transferManagerController.GetBusByID(nBus.Bustid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("AddPlane/{Plane:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddPlane([FromRoute(Name = "Plane")] int plane, 
            [FromRoute(Name = "Cost")] int cost)
        {
            DateTime dep = Convert.ToDateTime("11-12-2021");

            Planeticket nPlane = new Planeticket { Planetid = 31, Plane = plane, Seat = 1, Class = 2, Cityfrom = "Москва", Cityto = "Воронеж", 
                Departuretime = dep, Luggage = false, Cost = cost };

            _transferManagerController.AddPlane(nPlane);
            if (_transferManagerController.GetPlaneByID(nPlane.Planetid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("AddTrain/{Train:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddTrain([FromRoute(Name = "Train")] int train, 
            [FromRoute(Name = "Cost")] int cost)
        {
            DateTime dep = Convert.ToDateTime("11-12-2021");
            DateTime arr = Convert.ToDateTime("12-12-2021");

            Trainticket nTrain = new Trainticket { Traintid = 31, Train = train, Coach = 1, Seat = 1, Cityfrom = "Москва", Cityto = "Воронеж", 
                Departuretime = dep, Arrivaltime = arr, Linens = false, Cost = cost };

            _transferManagerController.AddTrain(nTrain);
            if (_transferManagerController.GetTrainByID(nTrain.Traintid) == null) 
            {
                return NotFound();
            }
            return Ok();
        }
*/
        /*--------------------------------------------------------------
         *                          Update
         * -----------------------------------------------------------*/
/*
        [HttpPost]
        [Route("UpdateTransfer/{TransferID:int}/{Bus:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTransfer([FromRoute(Name = "TransferID")] int transferID,
            [FromRoute(Name = "Bus")] int bus)
        {
            Transfer transfer = _transferManagerController.GetTransferByID(transferID);

            if (transfer == null)
            {
                return NotFound();
            }

            transfer.Busticket = bus;
            _transferManagerController.UpdateTransfer(transfer);
            
            if (_transferManagerController.GetTransferByID(transferID).Busticket != bus) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("UpdateBusTicket/{BusTID:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBus([FromRoute(Name = "BusTID")] int busTID,
            [FromRoute(Name = "Cost")] int cost)
        {
            Busticket busTicket = _transferManagerController.GetBusByID(busTID);

            if (busTicket == null)
            {
                return NotFound();
            }

            busTicket.Cost = cost;
            _transferManagerController.UpdateBus(busTicket);
            
            if (_transferManagerController.GetBusByID(busTID).Cost != cost) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("UpdatePlaneTicket/{PlaneTID:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePlane([FromRoute(Name = "PlaneTID")] int planeTID,
            [FromRoute(Name = "Cost")] int cost)
        {
            Planeticket planeTicket = _transferManagerController.GetPlaneByID(planeTID);

            if (planeTicket == null)
            {
                return NotFound();
            }

            planeTicket.Cost = cost;
            _transferManagerController.UpdatePlane(planeTicket);
            
            if (_transferManagerController.GetPlaneByID(planeTID).Cost != cost) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost]
        [Route("UpdateTrainTicket/{TrainTID:int}/{Cost:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTrain([FromRoute(Name = "TrainTID")] int trainTID,
            [FromRoute(Name = "Cost")] int cost)
        {
            Trainticket trainTicket = _transferManagerController.GetTrainByID(trainTID);

            if (trainTicket == null)
            {
                return NotFound();
            }

            trainTicket.Cost = cost;
            _transferManagerController.UpdateTrain(trainTicket);
            
            if (_transferManagerController.GetTrainByID(trainTID).Cost != cost) 
            {
                return NotFound();
            }
            return Ok();
        }
*/
        /*--------------------------------------------------------------
         *                          Delete
         * -----------------------------------------------------------*/
/*
        [HttpDelete]
        [Route("DeleteTransfer/{TransferID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTransfer([FromRoute(Name = "TransferID")] int transferID)
        {
            _transferManagerController.DeleteTransferByID(transferID);

            if (_transferManagerController.GetTransferByID(transferID) != null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpDelete]
        [Route("DeleteBus/{BusTID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBus([FromRoute(Name = "BusTID")] int busTID)
        {
            _transferManagerController.DeleteBusByID(busTID);

            if (_transferManagerController.GetBusByID(busTID) != null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpDelete]
        [Route("DeletePlane/{PlaneTID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePlane([FromRoute(Name = "PlaneTID")] int planeTID)
        {
            _transferManagerController.DeletePlaneByID(planeTID);

            if (_transferManagerController.GetPlaneByID(planeTID) != null) 
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpDelete]
        [Route("DeleteTrain/{TrainTID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrain([FromRoute(Name = "TrainTID")] int trainTID)
        {
            _transferManagerController.DeleteTrainByID(trainTID);

            if (_transferManagerController.GetTrainByID(trainTID) != null) 
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
*/