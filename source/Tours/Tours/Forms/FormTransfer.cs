using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tours
{
    public partial class Form1 : Form
    {
        public void AddColumnsBus()
        {
            TablesGrid.Rows.Clear();
            TablesGrid.Columns.Clear();

            TablesGrid.Columns.Add("BusTID", "ИД автобуса");
            TablesGrid.Columns.Add("Bus", "Номер автобуса");
            TablesGrid.Columns.Add("Seat", "Место");
            TablesGrid.Columns.Add("Cityfrom", "Откуда");
            TablesGrid.Columns.Add("Cityto", "Куда");
            TablesGrid.Columns.Add("Departuretime", "Время отправления");
            TablesGrid.Columns.Add("Arrivaltime", "Время прибытия");
            TablesGrid.Columns.Add("Luggage", "Багаж");
            TablesGrid.Columns.Add("Cost", "Стоимость");
        }

        public void AddColumnsPlane()
        {
            TablesGrid.Rows.Clear();
            TablesGrid.Columns.Clear();

            TablesGrid.Columns.Add("PlaneTID", "ИД самолета");
            TablesGrid.Columns.Add("Plane", "Номер рейса");
            TablesGrid.Columns.Add("Seat", "Место");
            TablesGrid.Columns.Add("Class", "Класс");
            TablesGrid.Columns.Add("Cityfrom", "Откуда");
            TablesGrid.Columns.Add("Cityto", "Куда");
            TablesGrid.Columns.Add("Departuretime", "Время отправления");
            TablesGrid.Columns.Add("Luggage", "Багаж");
            TablesGrid.Columns.Add("Cost", "Стоимость");
        }

        public void AddColumnsTrain()
        {
            TablesGrid.Rows.Clear();
            TablesGrid.Columns.Clear();

            TablesGrid.Columns.Add("TrainTID", "ИД поезда");
            TablesGrid.Columns.Add("Train", "Номер поезда");
            TablesGrid.Columns.Add("Coach", "Вагон");
            TablesGrid.Columns.Add("Seat", "Место");
            TablesGrid.Columns.Add("Cityfrom", "Откуда");
            TablesGrid.Columns.Add("Cityto", "Куда");
            TablesGrid.Columns.Add("Departuretime", "Время отправления");
            TablesGrid.Columns.Add("Arrivaltime", "Время прибытия");
            TablesGrid.Columns.Add("Linens", "Белье");
            TablesGrid.Columns.Add("Cost", "Стоимость");
        }

        bool IsBus = false, IsPlane = false, IsTrain = false;
        string CityFrom = "", CityTo = "";

        private void AllTransfer_Click(object sender, System.EventArgs e)
        {
            DateTime date = TrdateTimePickerBeg.Value;
            if (IsBus)
            {
                GetBuses(date);
            }

            else if (IsPlane)
            {
                GetPlanes(date);
            }

            else if (IsTrain)
            {
                GetTrains(date);
            }
        }

        private void TrcomboBoxCityF_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CityFrom = TrcomboBoxCityF.SelectedItem.ToString();
        }

        private void TrcomboBoxCityT_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CityTo = TrcomboBoxCityT.SelectedItem.ToString();
        }

        private void checkedListBoxTr_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (checkedListBoxTr.SelectedItem.ToString() == "Автобус")
            {
                IsBus = true;
                IsPlane = false; IsTrain = false;
                checkedListBoxTr.SetItemChecked(1, false);
                checkedListBoxTr.SetItemChecked(2, false);
            }
            else if (checkedListBoxTr.SelectedItem.ToString() == "Самолет")
            {
                IsPlane = true;
                IsBus = false; IsTrain = false;
                checkedListBoxTr.SetItemChecked(0, false);
                checkedListBoxTr.SetItemChecked(2, false);
            }
            else if (checkedListBoxTr.SelectedItem.ToString() == "Поезд")
            {
                IsTrain = true;
                IsBus = false; IsPlane = false;
                checkedListBoxTr.SetItemChecked(0, false);
                checkedListBoxTr.SetItemChecked(1, false);
            }
        }

        private void GetBuses(DateTime date)
        {
            AddColumnsBus();

            List<Busticket> busesF, busesT;
            if (CityFrom != "")
            {
                busesF = guest.GetBusesByCityFrom(CityFrom);
            }
            else
            {
                busesF = guest.GetAllBuses();
            }

            if (CityTo != "")
            {
                busesT = guest.GetBusesByCityTo(CityTo);
            }
            else
            {
                busesT = guest.GetAllBuses();
            }

            List<Busticket> busesD = guest.GetBusesByDate(date);

            List<Busticket> res1 = busesF.Intersect(busesT).ToList();
            List<Busticket> buses = res1.Intersect(busesD).ToList();
            int numBuses = buses.Count;

            if (numBuses > 0)
            {
                for (int i = 0; i < numBuses; i++)
                {
                    Busticket curBus = buses[i];
                    TablesGrid.Rows.Add(curBus.Bustid, curBus.Bus, curBus.Seat, curBus.Cityfrom, curBus.Cityto,
                        curBus.Departuretime, curBus.Arrivaltime, curBus.Luggage, curBus.Cost);
                }
            }
            else
            {
                MessageBox.Show("Автобусы не найдены!");
            }
        }

        private void GetPlanes(DateTime date)
        {
            AddColumnsPlane();

            List<Planeticket> planesF, planesT;
            if (CityFrom != "")
            {
                planesF = guest.GetPlanesByCityFrom(CityFrom);
            }
            else
            {
                planesF = guest.GetAllPlanes();
            }

            if (CityTo != "")
            {
                planesT = guest.GetPlanesByCityTo(CityTo);
            }
            else
            {
                planesT = guest.GetAllPlanes();
            }

            List<Planeticket> planesD = guest.GetPlanesByDate(date);

            List<Planeticket> res1 = planesF.Intersect(planesT).ToList();
            List<Planeticket> planes = res1.Intersect(planesD).ToList();
            int numPlanes = planes.Count;

            if (numPlanes > 0)
            {
                for (int i = 0; i < numPlanes; i++)
                {
                    Planeticket curPlane = planes[i];
                    TablesGrid.Rows.Add(curPlane.Planetid, curPlane.Plane, curPlane.Seat, curPlane.Class, curPlane.Cityfrom, curPlane.Cityto,
                        curPlane.Departuretime, curPlane.Luggage, curPlane.Cost);
                }
            }
            else
            {
                MessageBox.Show("Самолеты не найдены!");
            }
        }

        private void GetTrains(DateTime date)
        {
            AddColumnsTrain();

            List<Trainticket> trainF, trainT;
            if (CityFrom != "")
            {
                trainF = guest.GetTrainsByCityFrom(CityFrom);
            }
            else
            {
                trainF = guest.GetAllTrains();
            }

            if (CityTo != "")
            {
                trainT = guest.GetTrainsByCityTo(CityTo);
            }
            else
            {
                trainT = guest.GetAllTrains();
            }

            List<Trainticket> trainD = guest.GetTrainsByDate(date);

            List<Trainticket> res1 = trainF.Intersect(trainT).ToList();
            List<Trainticket> trains = res1.Intersect(trainD).ToList();
            int numTrains = trains.Count;

            if (numTrains > 0)
            {
                for (int i = 0; i < numTrains; i++)
                {
                    Trainticket curTrain = trains[i];
                    TablesGrid.Rows.Add(curTrain.Traintid, curTrain.Train, curTrain.Coach, curTrain.Seat,
                        curTrain.Cityfrom, curTrain.Cityto, curTrain.Departuretime, curTrain.Arrivaltime, curTrain.Linens, curTrain.Cost);
                }
            }
            else
            {
                MessageBox.Show("Поезда не найдены!");
            }
        }
    }
}
