using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tours
{
    public partial class Form1 : Form
    {
        bool IsVegMenu = false, IsChildMenu = false, IsBar = false;
        string Category = "";

        private void AddColumnsFood()
        {
            TablesGrid.Rows.Clear();
            TablesGrid.Columns.Clear();

            TablesGrid.Columns.Add("FoodID", "ИД питания");
            TablesGrid.Columns.Add("Category", "Тип");
            TablesGrid.Columns.Add("VegMenu", "Вегетарианское меню");
            TablesGrid.Columns.Add("ChildrenMenu", "Детское меню");
            TablesGrid.Columns.Add("Bar", "Бар");
            TablesGrid.Columns.Add("Cost", "Стоимость");
        }

        private void AllFood_Click(object sender, System.EventArgs e)
        {
            AddColumnsFood();

            List<Food> foodCat;
            if (Category != "")
            {
                foodCat = guest.GetFoodByCategory(Category);
            }
            else
            {
                foodCat = guest.GetAllFood();
            }

            if (IsVegMenu)
            {
                List<Food> foodVegMenu = guest.GetFoodByVegMenu(IsVegMenu);
                List<Food> res1 = foodCat.Intersect(foodVegMenu).ToList();
                foodCat = res1;
            }

            if (IsChildMenu)
            {
                List<Food> foodChildMenu = guest.GetFoodByChildMenu(IsChildMenu);
                List<Food> res2 = foodCat.Intersect(foodChildMenu).ToList();
                foodCat = res2;
            }

            if (IsBar)
            {
                List<Food> foodBar = guest.GetFoodByBar(IsChildMenu);
                List<Food> res3 = foodCat.Intersect(foodBar).ToList();
                foodCat = res3;
            }

            List<Food> food = foodCat;
            int numFood = food.Count;

            if (numFood > 0)
            {
                for (int i = 0; i < numFood; i++)
                {
                    Food curFood = food[i];
                    TablesGrid.Rows.Add(curFood.Foodid, curFood.Category, 
                                        curFood.Vegmenu, curFood.Childrenmenu, curFood.Bar, curFood.Cost);
                }
            }
            else
            {
                MessageBox.Show("Питание не найдено");
            }
        }

        private void FcheckVegMenu_CheckedChanged(object sender, EventArgs e)
        {
            IsVegMenu = FcheckVegMenu.Checked;
        }

        private void FcheckChildMenu_CheckedChanged(object sender, EventArgs e)
        {
            IsChildMenu = FcheckChildMenu.Checked;
        }

        private void FcheckBar_CheckedChanged(object sender, EventArgs e)
        {
            IsBar = FcheckBar.Checked;
        }

        private void FcomboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category = FcomboBoxCategory.SelectedItem.ToString();
        }

        /*--------------------------------------------------------------
         *                          Tourist
         * -----------------------------------------------------------*/

        private void FbuttonShowBook_Click(object sender, System.EventArgs e)
        {
            AddColumnsFood();

            List<Tour> tours = tourist.GetAllBookings(UserID);
            int numTours = tours.Count;

            if (numTours > 0)
            {
                for (int i = 0; i < numTours; i++)
                {
                    Tour curTour = tours[i];
                    Food curFood = tourist.GetFoodByID(curTour.Food);
                    TablesGrid.Rows.Add(curFood.Foodid, curFood.Category,
                                        curFood.Vegmenu, curFood.Childrenmenu, curFood.Bar, curFood.Cost);

                }
            }
            else
            {
                MessageBox.Show("Забронированное питание не найдено!");
            }
        }

        /*--------------------------------------------------------------
         *                          Manager
         * -----------------------------------------------------------*/
        private void FbuttonAdd_Click(object sender, System.EventArgs e)
        {
            FormManageTour formManage = new FormManageTour(FormManageTour.ChangeObj.Food);
            formManage.ShowDialog();
            Food nfood = formManage.ReturnFood();

            if (nfood != null)
            {
                manager.AddFood(nfood);
                MessageBox.Show("Питание было добавлено!");
            }
        }
        private void FbuttonChange_Click(object sender, System.EventArgs e)
        {
            FormManageTour formManage = new FormManageTour(FormManageTour.ChangeObj.Food);
            formManage.ShowDialog();
            Food chfood = formManage.ReturnFood();

            if (chfood != null)
            {
                manager.UpdateFood(chfood);
                MessageBox.Show("Питание было обновлено!");
            }
        }

        private void FbuttonDelete_Click(object sender, System.EventArgs e)
        {
            int DelFoodID = Convert.ToInt32(FtextBoxDelFood.Text);
            Food food = manager.GetFoodByID(DelFoodID);
            if (food != null)
            {
                manager.DeleteFoodByID(DelFoodID);
                MessageBox.Show("Питание " + DelFoodID + " было удалено!");
            }
            else
            {
                MessageBox.Show("Указанного питания не найдено!");
            }
        }
    }
}
