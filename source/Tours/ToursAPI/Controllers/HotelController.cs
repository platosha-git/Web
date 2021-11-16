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
        /// Список всех отелей
        /// </summary>
        /// <returns>Информация о всех отелях</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HotelDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllHotels()
        {
            var hotels = _hotelController.GetAllHotels();
            if (hotels == null)
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
        /// Список отелей в зависимости от города
        /// </summary>
        /// <param name="city">Город отеля</param>
        /// <returns>Информация об отелях в зависимости от города</returns>
        [HttpGet]
        [Route("{City}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HotelDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelsByCity([FromRoute(Name = "City")] string city)
        {
            var hotels = _hotelController.GetHotelsByCity(city);
            if (hotels == null)
            {
                return NotFound();
            }

            List<HotelDTO> lHotelsDTO = ListHotelDTO(hotels);
            return Ok(lHotelsDTO);
        }

        /// <summary>
        /// Список отелей в зависимости от класса
        /// </summary>
        /// <param name="cls">Класс отеля</param>
        /// <returns>Информация об отелях в зависимости от класса</returns>
        [HttpGet]
        [Route("{Class:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HotelDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelsByClass([FromRoute(Name = "Class")] int cls)
        {
            var hotels = _hotelController.GetHotelsByClass(cls);
            if (hotels == null)
            {
                return NotFound();
            }

            List<HotelDTO> lHotelsDTO = ListHotelDTO(hotels);
            return Ok(lHotelsDTO);
        }

        /// <summary>
        /// Добавление отлея
        /// </summary>
        /// <param name="hotelDTO">Добавлемый отель</param>
        /// <returns>Результат добавления</returns>
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