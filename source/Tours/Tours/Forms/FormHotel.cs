using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tours
{
    public partial class Form1 : Form
    {
        int Class = -1;
        bool IsSwimPool = false;
        string HotelCity = "", Type = "";

        private void AddColumnsHotel()
        {
            TablesGrid.Rows.Clear();
            TablesGrid.Columns.Clear();

            TablesGrid.Columns.Add("HotelID", "ИД отеля");
            TablesGrid.Columns.Add("City", "Город");
            TablesGrid.Columns.Add("Name", "Название");
            TablesGrid.Columns.Add("Type", "Тип размещения");
            TablesGrid.Columns.Add("Class", "Колиечство звезд");
            TablesGrid.Columns.Add("SwimPool", "Бассейн");
            TablesGrid.Columns.Add("Cost", "Стоимость");
        }

        private void AllHotels_Click(object sender, EventArgs e)
        {
            AddColumnsHotel();

            List<Hotel> hotelCity;
            if (HotelCity != "")
            {
                hotelCity = guest.GetHotelsByCity(HotelCity);
            }
            else
            {
                hotelCity = guest.GetAllHotels();
            }

            if (Type != "")
            {
                List<Hotel> hotelType = guest.GetHotelsByType(Type);
                List<Hotel> res1 = hotelCity.Intersect(hotelType).ToList();
                hotelCity = res1;
            }

            if (Class > 0)
            {
                List<Hotel> hotelClass = guest.GetHotelsByClass(Class);
                List<Hotel> res2 = hotelCity.Intersect(hotelClass).ToList();
                hotelCity = res2;
            }

            if (IsSwimPool)
            {
                List<Hotel> hotelSwimPool = guest.GetHotelsBySwimPool(IsSwimPool);
                List<Hotel> res3 = hotelCity.Intersect(hotelSwimPool).ToList();
                hotelCity = res3;
            }

            List<Hotel> hotels = hotelCity;
            int numHotels = hotels.Count;

            if (numHotels > 0)
            {
                for (int i = 0; i < numHotels; i++)
                {
                    Hotel curHotel = hotels[i];
                    TablesGrid.Rows.Add(curHotel.Hotelid, curHotel.City, curHotel.Name, curHotel.Type, curHotel.Class, curHotel.Swimpool, curHotel.Cost);
                }
            }
            else
            {
                MessageBox.Show("Отели не найдены");
            }
        }

        private void HcomboBoxCity_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            HotelCity = HcomboBoxCity.SelectedItem.ToString();
        }

        private void HcomboBoxType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Type = HcomboBoxType.SelectedItem.ToString();
        }

        private void FnumericUDStars_ValueChanged(object sender, System.EventArgs e)
        {
            Class = (int)FnumericUDStars.Value;
        }

        private void HcheckSwimPool_CheckedChanged(object sender, EventArgs e)
        {
            IsSwimPool = HcheckSwimPool.Checked;
        }

        private void HbuttonClearParams_Click(object sender, System.EventArgs e)
        {
            HcomboBoxCity.SelectedItem = "";
            HcomboBoxType.SelectedItem = "";
            FnumericUDStars.Value = 5;
            HcheckSwimPool.Checked = false;
            Class = -1; IsSwimPool = false; HotelCity = ""; Type = "";
        }

        /*--------------------------------------------------------------
         *                          Tourist
         * -----------------------------------------------------------*/

        private void HbuttonShowBook_Click(object sender, System.EventArgs e)
        {
            AddColumnsHotel();

            List<Tour> tours = tourist.GetAllBookings(UserID);
            int numTours = tours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = tours[i];
                    Hotel curHotel = tourist.GetHotelByID(curTour.Hotel);

                    TablesGrid.Rows.Add(curHotel.Hotelid, curHotel.City, curHotel.Name, curHotel.Type, curHotel.Class, curHotel.Swimpool, curHotel.Cost);
                }
            }
            else
            {
                MessageBox.Show("Забронированные отели не найдены!");
            }
        }

        /*--------------------------------------------------------------
         *                          Manager
         * -----------------------------------------------------------*/
        private void HbuttonAdd_Click(object sender, System.EventArgs e)
        {
            FormManageTour formManage = new FormManageTour(FormManageTour.ChangeObj.Hotel);
            formManage.ShowDialog();
            Hotel nhotel = formManage.ReturnHotel();

            if (nhotel != null)
            {
                manager.AddHotel(nhotel);
                MessageBox.Show("Отель был добавлен!");
            }
        }

        private void HbuttonChange_Click(object sender, System.EventArgs e)
        {
            FormManageTour formManage = new FormManageTour(FormManageTour.ChangeObj.Hotel);
            formManage.ShowDialog();
            Hotel chhotel = formManage.ReturnHotel();

            if (chhotel != null)
            {
                manager.UpdateHotel(chhotel);
                MessageBox.Show("Отель был обновлен!");
            }
        }

        private void HbuttonDelete_Click(object sender, System.EventArgs e)
        {
            int DelHotelID = Convert.ToInt32(HtextBoxDelHotel.Text);
            Hotel hotel = manager.GetHotelByID(DelHotelID);
            if (hotel != null)
            {
                manager.DeleteHotelByID(DelHotelID);
                MessageBox.Show("Отель " + DelHotelID + " был удален!");
            }
            else
            {
                MessageBox.Show("Указанного отеля не найдено!");
            }
        }
    }
}
