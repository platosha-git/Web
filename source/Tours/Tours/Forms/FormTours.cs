using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;
using static Tours.FormManageTour;
using Tours.ModelsDB;

namespace Tours
{
    public partial class Form1 : Form
    {
        string TourCity = "";

        public void AddColumnsTour()
        {
            TablesGrid.Rows.Clear();
            TablesGrid.Columns.Clear();

            TablesGrid.Columns.Add("TourID", "ИД тура");
            TablesGrid.Columns.Add("City", "Город");
            TablesGrid.Columns.Add("Name", "Имя отеля");
            TablesGrid.Columns.Add("Type", "Категория отеля");
            TablesGrid.Columns.Add("Category", "Питание");
            TablesGrid.Columns.Add("Transfer", "ИД трансфера");
            TablesGrid.Columns.Add("Cost", "Стоимость");
            TablesGrid.Columns.Add("DateBegin", "Дата начала");
            TablesGrid.Columns.Add("DateEnd", "Дата конца");
        }

        private void AllTours_Click(object sender, System.EventArgs e)
        {
            AddColumnsTour();

            DateTime DateBegin = TimePickerBegin.Value;
            DateTime DateEnd = TimePickerEnd.Value;

            List<Tour> toursCity;
            if (TourCity != "")
            {
                toursCity = guest.GetToursByCity(TourCity);
            }
            else
            {
                toursCity = guest.GetAllTours();
            }

            List<Tour> toursDate = guest.GetToursByDate(DateBegin, DateEnd);
            
            List<Tour> resTours = toursCity.Intersect(toursDate).ToList();
            int numTours = resTours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = resTours[i];
                    FullUserTour curFTour = guest.GetFullTour(curTour.Tourid);

                    TablesGrid.Rows.Add(curFTour.tourid, curFTour.city, curFTour.name, curFTour.type,
                        curFTour.category, curFTour.transfer, curFTour.cost, 
                        curFTour.datebegin.ToString("d"), curFTour.dateend.ToString("d"));
                }
            }
            else
            {
                MessageBox.Show("Туры не найдены!");
            }
        }

        private void TcomboBoxCity_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TourCity = TcomboBoxCity.SelectedItem.ToString();
        }

        /*--------------------------------------------------------------
         *                          Tourist
         * -----------------------------------------------------------*/

        int BookTourID = 0, DelBookTourID = 0;
        private void TbuttonShowBook_Click(object sender, System.EventArgs e)
        {
            AddColumnsTour(); 
            
            List<Tour> tours = tourist.GetAllBookings(UserID);
            int numTours = tours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = tours[i];
                    FullUserTour curFTour = guest.GetFullTour(curTour.Tourid);

                    TablesGrid.Rows.Add(curFTour.tourid, curFTour.city, curFTour.name, curFTour.type,
                        curFTour.category, curFTour.transfer, curFTour.cost,
                        curFTour.datebegin.ToString("d"), curFTour.dateend.ToString("d"));
                }
            }
            else
            {
                MessageBox.Show("Туры не найдены!");
            }
        }

        private void TbuttonBook_Click(object sender, System.EventArgs e)
        {
            BookTourID = Convert.ToInt32(TtextBoxBookTour.Text);
            Tour btour = tourist.GetTourByID(BookTourID);
            if (btour != null)
            {
                List<Tour> atours = tourist.GetAllBookings(UserID);
                if (atours.Contains(btour))
                {
                    MessageBox.Show("Тур " + BookTourID + " уже забронирован!");
                }
                else
                {
                    tourist.BookTour(BookTourID, UserID);
                    MessageBox.Show("Тур " + BookTourID + " забронирован!");
                }
            }
            else
            {
                MessageBox.Show("Тур для брони не найден!");
            }
        }

        private void TbuttonDelBook_Click(object sender, System.EventArgs e)
        {
            DelBookTourID = Convert.ToInt32(TtextBoxDeleteBook.Text);
            Tour dbtour = tourist.GetTourByID(DelBookTourID);
            if (dbtour != null)
            {
                List<Tour> atours = tourist.GetAllBookings(UserID);
                if (atours.Contains(dbtour))
                {
                    tourist.RemoveTour(DelBookTourID, UserID);
                    MessageBox.Show("Бронирование тура " + DelBookTourID + " отменено!");
                }
                else
                {
                    MessageBox.Show("Бронь тура " + DelBookTourID + " отсутсвует!");
                }
            }
            else
            {
                MessageBox.Show("Забронированный тур не найден!");
            }
        }

        /*--------------------------------------------------------------
         *                          Manager
         * -----------------------------------------------------------*/

        private void TbuttonAdd_Click(object sender, System.EventArgs e)
        {
            FormManageTour formManage = new FormManageTour(ChangeObj.Tour);
            formManage.ShowDialog();
            Tour ntour = formManage.ReturnTour();

            if (ntour != null)
            {
                manager.AddTour(ntour);
                MessageBox.Show("Тур был добавлен!");
            }
        }

        private void TbuttonChTour_Click(object sender, System.EventArgs e)
        {
            FormManageTour formManage = new FormManageTour(ChangeObj.Tour);
            formManage.ShowDialog();
            Tour chtour = formManage.ReturnTour();

            if (chtour != null)
            {
                manager.UpdateTour(chtour);
                MessageBox.Show("Тур был обновлен!");
            }
        }

        private void TbuttonDelTour_Click(object sender, System.EventArgs e)
        {
            int DelTourID = Convert.ToInt32(TtextBoxDelTour.Text);
            Tour tour = manager.GetTourByID(DelTourID);
            if (tour != null)
            {
                manager.DeleteTourByID(DelTourID);
                MessageBox.Show("Тур " + DelTourID + " был удален!");
            }
            else
            {
                MessageBox.Show("Указанного тура не найдено!");
            }
        }
    }
}
