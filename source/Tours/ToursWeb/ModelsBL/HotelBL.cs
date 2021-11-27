using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsBL
{
    public class HotelBL
    {
        public int Hotelid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Class { get; set; }
        public bool? Swimpool { get; set; }
        public string City { get; set; }
        public int Cost { get; set; }

        public HotelBL() { }

        public HotelBL(Hotel hotel)
        {
            Hotelid = hotel.Hotelid;
            Name = hotel.Name;
            Type = hotel.Type;
            Class = hotel.Class;
            Swimpool = hotel.Swimpool;
            City = hotel.City;
            Cost = hotel.Cost;
        }

        public Hotel GetHotel()
        {
            Hotel hotel = new Hotel()
            {
                Hotelid = Hotelid,
                Name = Name,
                Type = Type,
                Class = Class,
                Swimpool = Swimpool,
                City = City,
                Cost = Cost
            };

            return hotel;
        }
        
        private bool Equals(HotelBL hotel)
        {
            if (hotel is null)
            {
                return false;
            }

            return Hotelid == hotel.Hotelid && 
                   Name == hotel.Name && 
                   Type == hotel.Type && 
                   Class == hotel.Class && 
                   Swimpool == hotel.Swimpool && 
                   City == hotel.City && 
                   Cost == hotel.Cost;
        }

        public override bool Equals(object obj) => Equals(obj as HotelBL);
        public override int GetHashCode() => (Hotelid, Name, Type, Class, Swimpool, City, Cost).GetHashCode();
    }
}