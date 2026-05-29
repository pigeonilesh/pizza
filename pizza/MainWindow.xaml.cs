using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pizza
{
    public partial class MainWindow : Window
    {
        private int priceMargarita = 450;
        private int pricePepperoni = 550;
        private int priceHawaii = 500;
        private int priceCola = 100;
        private int priceJuice = 80;
        private int priceWater = 50;
        private int priceCheese = 50;
        private int priceBecon = 80;
        private int priceOlive = 100;
        private int priceMushrum = 80;
        private int deliveryPrice = 200;
        private int discount = 20;
        public MainWindow()
        {
            InitializeComponent();
        }
        private int GetPizzaPrice()
        {
            if(MargaritaRbtn.IsChecked == true)
                return priceMargarita;
            if (PepperoniRbtn.IsChecked == true)
                return pricePepperoni;
            else
                return priceHawaii;
        }
        private string GetPizzaName()
        {
            if (MargaritaRbtn.IsChecked == true)
                return "Маргарита";
            if (PepperoniRbtn.IsChecked == true)
                return "Пепперони";
            else
                return "Гавайская";
        }
        private int GetPizzaCount()
        {
            if(int.TryParse(QuantityTB.Text, out int count))
                return count;
            return 1;
        }
        private int GetDrinkPrice()
        {
            if (DrinkChbx.IsChecked != true)
                return 0;
            if (ColaRbtn.IsChecked == true) 
                return priceCola;
            if (JuiceRbtn.IsChecked == true)
                return priceJuice;
            else 
                return priceWater;
        }
        private string GetDringName()
        {
            if (DrinkChbx.IsChecked != true)
                return "не выбран";
            if (ColaRbtn.IsChecked == true)
                return "кола";
            if (JuiceRbtn.IsChecked == true)
                return "сок";
            else
                return "вода";
        }
        private int GetDopIngredionPrice()
        {
            int price = 0;
            if(CheeseChbx.IsChecked == true)
                price += priceCheese;
            if(BeconChbx.IsChecked == true)
                price += priceBecon;
            if(OliveChbx.IsChecked == true)
                price += priceOlive;
            if(MushrumChbx.IsChecked == true)
                price += priceMushrum;

            return price;
        }
        private string GetDopIngredionName()
        {
            string cheese = null;
            string becon = null;
            string olive = null;
            string mushrum = null;
            string ingredName;

            if (CheeseChbx.IsChecked == true)
                cheese = " сыр ";
            if (BeconChbx.IsChecked == true)
                becon = " бекон ";
            if (OliveChbx.IsChecked == true)
                olive = " оливки ";
            if (MushrumChbx.IsChecked == true)
                mushrum = " грибы ";

            if (CheeseChbx.IsChecked == false && BeconChbx.IsChecked == false && OliveChbx.IsChecked == false && MushrumChbx.IsChecked == false)
            {
                ingredName = "Не выбраны";
            }
            else 
                ingredName = cheese + becon + olive + mushrum;
            return ingredName.Trim();
        }
        private int GetDeliveryPrice()
        {
            if (PickupRbt.IsChecked == true)
                return 0;
            else
                return deliveryPrice;
        }
        private string GetDeliveryName()
        {
            if (PickupRbt.IsChecked == true)
                return "Самовывоз";
            else
                return "Доставка - 200 руб.";
        }
        private void UpdateTotal()
        {
            int pizzaPrica = GetPizzaPrice();
            int kol = GetPizzaCount();
            int drinkPrice = GetDrinkPrice();
            int dopingprice = GetDopIngredionPrice();
            int devprice = GetDeliveryPrice();
            int total = (pizzaPrica * kol) + drinkPrice + dopingprice + devprice;

            if (total > 1000)
            {
                TotalTl.Foreground = Brushes.Green;
            }

            TotalTl.Text = $"Итого: {total} руб.";
        }
        private void AnyParameter_Changed(object sender, RoutedEventArgs e)
        {
            UpdateTotal();
        }
        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            int kol = GetPizzaCount();
            if (kol > 1)
            {
                QuantityTB.Text = (kol - 1).ToString();
            }
            UpdateTotal();
        }
        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            int kol = GetPizzaCount();
            if (kol < 10)
            {
                QuantityTB.Text = (kol + 1).ToString();
            }
            UpdateTotal();
        }
        private void DrinkChbx_Click(object sender, RoutedEventArgs e)
        {
            bool isEnable = DrinkChbx.IsChecked == true;
            DrinkPanel.IsEnabled = isEnable;
            UpdateTotal();
        }
        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            string pizzaName = GetPizzaName();
            int kol = GetPizzaCount();
            string drinkName = GetDringName();
            string deliveryName = GetDeliveryName();
            string dobingedname = GetDopIngredionName();
            int total = (GetPizzaPrice() * kol) + GetDrinkPrice() + GetDopIngredionPrice() + GetDeliveryPrice();

            string message = "ВАШ ЗАКАЗ\n" +
                            $"Пицца: {pizzaName}, {kol} шт.\n" +
                            $"Доп. ингредиенты: {dobingedname}\n" +
                            $"Напиток: {drinkName}\n" +
                            $"Доставка: {deliveryName}\n" +
                            $"Сумма {total} руб.\n" +
                            $"Спасибо за заказ";
            MessageBox.Show(message, "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetAll();
        }
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }
        private void ResetAll()
        {
            MargaritaRbtn.IsChecked = true;
            QuantityTB.Text = "1";
            DrinkChbx.IsChecked = false;
            WaterRbtn.IsChecked = true;
            DrinkPanel.IsEnabled = false;
            PickupRbt.IsChecked = true;
            CheeseChbx.IsChecked = false;
            BeconChbx.IsChecked = false;
            OliveChbx.IsChecked = false;
            MushrumChbx.IsChecked = false;
            UpdateTotal();
        }

        private void CheckPromoBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
