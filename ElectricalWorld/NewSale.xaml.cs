using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : Window
    {
        IBL bl = BLAPI.BLFactory.GetBL();
        ObservableCollection<PO.Customer> custs = new ObservableCollection<PO.Customer>();
        ObservableCollection<PO.Item> items = new ObservableCollection<PO.Item>();
        ObservableCollection<PO.Item> basket = new ObservableCollection<PO.Item>();

        PO.Order order = new PO.Order();

        public NewSale()
        {
            InitializeComponent();
            lvBasket.DataContext = basket;
        }


        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
        }


        private void btnProdSearch_Click(object sender, RoutedEventArgs e)
        {
            items.Clear();

            foreach (var item in bl.GetItems(it =>
                it.ItemID.ToString().ToLower().Contains(tboxProdSearch.Text.ToLower()) ||
                it.Brand.ToLower().Contains(tboxProdSearch.Text.ToLower()) ||
                it.ModelNumber.ToLower().Contains(tboxProdSearch.Text.ToLower().ToLower())
                )
                )
            {
                items.Add(PO.Tools.POItem(item));
            }
            lvItems.DataContext = items;
        }

        private void btnChooseItem_Click(object sender, RoutedEventArgs e)
        {
            PO.Item item = (sender as Button).DataContext as PO.Item;
            if (item is PO.Item)
                basket.Add(item);
            lblTotal.Content = basket.Sum(it => it.Price);
        }




        private void cmbCusts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            custs.Clear();
            cmbCusts.IsDropDownOpen = true;

            string input = (cmbCusts.Text + e.Text).ToLower();

            foreach (var item in bl.GetCutomers(cust =>
                cust.CustomerID.ToString().ToLower().Contains(input) ||
                cust.Name.ToLower().Contains(input) ||
                cust.Company.ToLower().Contains(input) ||
                cust.Phone.ToLower().Contains(input) ||
                cust.Mobile.ToLower().Contains(input) ||
                cust.Address.ToLower().Contains(input) ||
                cust.PostCode.ToLower().Contains(input) ||
                cust.Email.ToLower().Contains(input)
                )
                )
            {
                custs.Add(PO.Tools.POCustomer(item));
            }
            cmbCusts.ItemsSource = custs;
        }

        private void btnEndSale_Click(object sender, RoutedEventArgs e)
        {
            order.Items = from item in basket
                          select item;
            order.OrderTime = DateTime.Now;
            order.TotalPrice = basket.Sum(it => it.Price);
            order.OrderID = bl.AddOrder(PO.Tools.BOOrder(order));
            new Thread(() =>
                {
                    Tools.CreateInvoice(order);
                }).Start();

            Close();
        }

        private void cmbCusts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            order.Customer = (sender as ComboBox).SelectedItem as PO.Customer;
        }

        private void lvBasket_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Item item = (sender as ListView).SelectedItem as PO.Item;
            if (item is PO.Item)
            {
                ItemInfo itemInfo = new ItemInfo(item);
                itemInfo.ShowDialog();
            }
        }

        private void lvItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Item item = (sender as ListView).SelectedItem as PO.Item;
            if (item is PO.Item)
            {
                ItemInfo itemInfo = new ItemInfo(item);
                itemInfo.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PO.Item item = (sender as ListView).SelectedItem as PO.Item;
            if (item is PO.Item)
            {
                ItemInfo itemInfo = new ItemInfo(item);
                itemInfo.ShowDialog();
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            order.Items = from item in basket
                          select item;
            order.OrderTime = DateTime.Now;
            order.TotalPrice = basket.Sum(it => it.Price);
            order.OrderID = bl.AddOrder(PO.Tools.BOOrder(order));
            new Thread(() =>
            {
                Tools.CreateInvoice(order, true);
            }).Start();

            Close();
        }

        private void tboxProdSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            items.Clear();

            foreach (var item in bl.GetItems(it =>
                it.ItemID.ToString().ToLower().Contains(tboxProdSearch.Text.ToLower()) ||
                it.Brand.ToLower().Contains(tboxProdSearch.Text.ToLower()) ||
                it.ModelNumber.ToLower().Contains(tboxProdSearch.Text.ToLower().ToLower())
                )
                )
            {
                items.Add(PO.Tools.POItem(item));
            }
            lvItems.DataContext = items;
        }

        private void btnEndSaleWithEmail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
