using System.Collections.Generic;
using System.Windows.Forms;

namespace Tours
{
    public partial class Form1 : Form
    {
        bool IsBBus = false, IsBPlane = false, IsBTrain = false;
        private void BookTransfer_Click(object sender, System.EventArgs e)
        {
            if (IsBBus)
            {
                GetBookBuses();
            }

            else if (IsBPlane)
            {
                GetBookPlanes();
            }

            else if (IsBTrain)
            {
                GetBookTrains();
            }
        }

        private void TrcheckedListBoxBook_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (TrcheckedListBoxBook.SelectedItem.ToString() == "Автобус")
            {
                IsBBus = true;
                IsBPlane = false; IsBTrain = false;
                TrcheckedListBoxBook.SetItemChecked(1, false);
                TrcheckedListBoxBook.SetItemChecked(2, false);
            }
            else if (TrcheckedListBoxBook.SelectedItem.ToString() == "Самолет")
            {
                IsBPlane = true;
                IsBBus = false; IsBTrain = false;
                TrcheckedListBoxBook.SetItemChecked(0, false);
                TrcheckedListBoxBook.SetItemChecked(2, false);
            }
            else if (TrcheckedListBoxBook.SelectedItem.ToString() == "Поезд")
            {
                IsBTrain = true;
                IsBBus = false; IsBPlane = false;
                TrcheckedListBoxBook.SetItemChecked(0, false);
                TrcheckedListBoxBook.SetItemChecked(1, false);
            }
        }

        private void GetBookBuses()
        {
            AddColumnsBus();

            List<Tour> tours = tourist.GetAllBookings(UserID);
            int numTours = tours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = tours[i];
                    Transfer curTransfer = tourist.GetTransferByID(curTour.Transfer);

                    int curBusID = (int)curTransfer.Busticket;

                    if (curBusID > 0)
                    {
                        Busticket curBus = tourist.GetBusByID(curBusID);
                        TablesGrid.Rows.Add(curBus.Bustid, curBus.Bus, curBus.Seat, curBus.Cityfrom, curBus.Cityto,
                            curBus.Departuretime, curBus.Arrivaltime, curBus.Luggage, curBus.Cost);

                    }
                }
            }
            else
            {
                MessageBox.Show("Забронированный трансфер не найден!");
            }
        }

        private void GetBookPlanes()
        {
            AddColumnsPlane();

            List<Tour> tours = tourist.GetAllBookings(UserID);
            int numTours = tours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = tours[i];
                    Transfer curTransfer = tourist.GetTransferByID(curTour.Transfer);

                    int curPlaneID = (int)curTransfer.Planeticket;
                    if (curPlaneID > 0)
                    {
                        Planeticket curPlane = tourist.GetPlaneByID(curPlaneID);
                        TablesGrid.Rows.Add(curPlane.Planetid, curPlane.Plane, curPlane.Seat, curPlane.Class, curPlane.Cityfrom, curPlane.Cityto,
                            curPlane.Departuretime, curPlane.Luggage, curPlane.Cost);

                    }
                }
            }
            else
            {
                MessageBox.Show("Забронированный трансфер не найден!");
            }
        }

        private void GetBookTrains()
        {
            AddColumnsTrain();

            List<Tour> tours = tourist.GetAllBookings(UserID);
            int numTours = tours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = tours[i];
                    Transfer curTransfer = tourist.GetTransferByID(curTour.Transfer);

                    int curTrainID = (int)curTransfer.Trainticket;
                    if (curTrainID > 0)
                    {
                        Trainticket curTrain = tourist.GetTrainByID(curTrainID);
                        TablesGrid.Rows.Add(curTrain.Traintid, curTrain.Train, curTrain.Coach, curTrain.Seat,
                            curTrain.Cityfrom, curTrain.Cityto, curTrain.Departuretime, curTrain.Arrivaltime, curTrain.Linens, curTrain.Cost);

                    }
                }
            }
            else
            {
                MessageBox.Show("Забронированный трансфер не найден!");
            }
        }
    }
}
