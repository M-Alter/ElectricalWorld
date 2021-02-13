using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        PO.Customer cust;
        public NewSale()
        {
            InitializeComponent();
            lvBasket.DataContext = basket;
        }

        //private void btnCustSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    custs.Clear();

        //    foreach (var item in bl.GetCutomers(cust =>
        //        cust.CustomerID.ToString().ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.FirstName.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.LastName.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.Phone.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.Mobile.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.Address.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.PostCode.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
        //        cust.Email.ToLower().Contains(tboxCustSearch.Text.ToLower())
        //        )
        //        )
        //    {
        //        custs.Add(item);
        //    }
        //    cmbCusts.ItemsSource = custs;
        //}

        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {

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
            PO.Item item = (PO.Item)(sender as Button).DataContext as PO.Item;
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
            PO.Order order = new PO.Order
            {
                Customer = cust,
                Items = from item in basket
                        select item,
                OrderTime = DateTime.Now,
                TotalPrice = basket.Sum(it => it.Price)
            };

            string orderID = bl.AddOrder(PO.Tools.BOOrder(order));
            order.OrderID = orderID;
            Tools.CreateInvoice(order);

        }

        private void cmbCusts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cust = (PO.Customer)e.AddedItems[0] as PO.Customer;
        }
    }
}
