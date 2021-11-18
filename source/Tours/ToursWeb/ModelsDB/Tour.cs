using System;
using System.Collections.Generic;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public class UserTour
    {
        public int Tourid { get; set; }
        public string City { get; set; }
        public string HName { get; set; }
        public string HType { get; set; }
        public int HClass { get; set; }
        public string FCategory { get; set; }
        public string TType { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }
        public int Cost { get; set; }
        
        public UserTour()
        {
        }
        
        public UserTour(Tour tour, Hotel hotel, Food food, Transfer transfer)
        {
            Tourid = tour.Tourid;
            City = hotel.City;
            HName = hotel.Name;
            HType = hotel.Type;
            HClass = (int) hotel.Class;
            FCategory = food.Category;
            TType = transfer.Type;
            Datebegin = tour.Datebegin;
            Dateend = tour.Dateend;
            Cost = tour.Cost;
        }
    }
    public partial class Tour
    {
        public int Tourid { get; set; }
        public int Food { get; set; }
        public int Hotel { get; set; }
        public int Transfer { get; set; }
        public int Cost { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }

        public virtual Food FoodNavigation { get; set; }
        public virtual Hotel HotelNavigation { get; set; }
        public virtual Transfer TransferNavigation { get; set; }
    }
}
