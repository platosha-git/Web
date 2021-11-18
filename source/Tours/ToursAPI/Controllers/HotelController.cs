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
    [Route("/api/v1/Hotel")]

    public class ApiHotelController : ControllerBase
    {
        private readonly HotelController _hotelController;

        public ApiHotelController(HotelController hotelController)
        {
            _hotelController = hotelController;
        }

        private List<HotelDTO> ListHotelDTO(List<Hotel> lHotels)
        {
            List<HotelDTO> lHotelDTO = new List<HotelDTO>();
            foreach (var hotel in lHotels)
            {
                HotelDTO hotelDTO = new HotelDTO(hotel);
                lHotelDTO.Add(hotelDTO);
            }
            return lHotelDTO;
        }
        
        /// <summary>
        /// Список отелей в соответствии с параметрами
        /// </summary>
        /// <returns>Информация о всех отелях</returns>
        /// <response code="200">Отели найдены</response>
        /// <response code="404">Отели отсутсвуют</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HotelDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllHotels([FromQuery(Name = "City")] string? city = null,
            [FromQuery(Name = "Class")] int? cls = null, [FromQuery(Name = "Type")] HType? type = null,
            [FromQuery(Name = "Swimming pool")] bool? sp = null)
        {
            List<Hotel> hotels = _hotelController.GetAllHotels();
            if (hotels != null)
            {
                if (city != null)
                {
                    hotels = _hotelController.GetHotelsByCity(city);
                }

                if (cls != null)
                {
                    List<Hotel> hotelsCls = _hotelController.GetHotelsByClass((int)cls);
                    List<Hotel> res1 = hotels.Intersect(hotelsCls).ToList();
                    hotels = res1;
                }

                if (type != null)
                {
                    List<Hotel> hotelsType = _hotelController.GetHotelsByType(type.ToString());
                    List<Hotel> res2 = hotels.Intersect(hotelsType).ToList();
                    hotels = res2;
                }
                
                if (sp != null)
                {
                    List<Hotel> hotelsSP = _hotelController.GetHotelsBySwimPool((bool)sp);
                    List<Hotel> res3 = hotels.Intersect(hotelsSP).ToList();
                    hotels = res3;
                }
            }
            
            if (hotels == null || hotels.Count == 0)
            {
                return NotFound();
            }

            List<HotelDTO> lHotelsDTO = ListHotelDTO(hotels);
            return Ok(lHotelsDTO);
        }
        
        /// <summary>
        /// Отель по ключу
        /// </summary>
        /// <param name="hotelID">ИД отеля</param>
        /// <returns>Информация об отеле по ключу</returns>
        /// <response code="200">Отель найден</response>
        /// <response code="404">Отель отсутсвует</response>
        [HttpGet]
        [Route("HotelID/{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelByID([FromRoute(Name = "HotelID")] int hotelID)
        {
            var hotel = _hotelController.GetHotelByID(hotelID);
            if (hotel == null)
            {
                return NotFound();
            }

            HotelDTO hotelDTO = new HotelDTO(hotel);
            return Ok(hotelDTO);
        }

        /// <summary>
        /// Добавление отлея
        /// </summary>
        /// <param name="hotelDTO">Добавлемый отель</param>
        /// <returns>Результат добавления</returns>
        /// <response code="200">Отель добавлен</response>
        /// <response code="400">Ошибка добавления</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddHotel([FromBody] HotelDTO hotelDTO)
        {
            Hotel aHotel = hotelDTO.GetHotel();
            _hotelController.AddHotel(aHotel);

            Hotel hotel = _hotelController.GetHotelByID(aHotel.Hotelid); 
            if (hotel == null) 
            {
                return BadRequest();
            }

            HotelDTO addedHotel = new HotelDTO(hotel);
            return Ok(addedHotel);
        }

        /// <summary>
        /// Обновление отеля
        /// </summary>
        /// <param name="hotelDTO">Обновляемый отель</param>
        /// <returns>Результат обновления</returns>
        /// <response code="200">Отель обновлен</response>
        /// <response code="400">Ошибка обновления</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateHotel([FromBody] HotelDTO hotelDTO)
        {
            Hotel uHotel = hotelDTO.GetHotel();
            _hotelController.UpdateHotel(uHotel);

            Hotel hotel = _hotelController.GetHotelByID(hotelDTO.Hotelid); 
            if (!hotelDTO.AreEqual(hotel))
            {
                return NotFound();
            }

            HotelDTO updatedHotel = new HotelDTO(hotel);
            return Ok(updatedHotel);
        }

        /// <summary>
        /// Удаление отеля по ключу
        /// </summary>
        /// <param name="hotelID">ИД отеля</param>
        /// <returns>Результат удаления</returns>
        /// <response code="200">Отель удален</response>
        /// <response code="404">Отель отсутсвует</response>
        [HttpDelete]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel([FromRoute(Name = "HotelID")] int hotelID)
        {
            Hotel delHotel = _hotelController.GetHotelByID(hotelID);
            if (delHotel == null)
            {
                return NotFound();
            }
            
            _hotelController.DeleteHotelByID(hotelID);
            HotelDTO hotel = new HotelDTO(delHotel);
            return Ok(hotel);
        }
    }
}