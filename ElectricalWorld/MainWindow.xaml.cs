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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLAPI.BLFactory.GetBL();
        ObservableCollection<BO.Item> items = new ObservableCollection<BO.Item>();
        ObservableCollection<BO.Order> sales = new ObservableCollection<BO.Order>();


        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in bl.GetItems(it => true))
            {
                items.Add(item);
            }
            lvItems.DataContext = items;

        }

        private void lvItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Item item = lvItems.SelectedItem as BO.Item;
            if (item is BO.Item)
            {
                ItemInfo itemInfo = new ItemInfo(item);
                itemInfo.Show();
            }
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.Show();
        }

        private void btnSalesSearch_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in bl.GetOrders(
                ord => ord.Customer.CustomerID.ToString() == tboxSalesSearch.Text ||
                ord.Customer.FirstName.Contains(tboxSalesSearch.Text) ||
                ord.Customer.LastName.Contains(tboxSalesSearch.Text) ||
                ord.Customer.Phone.Contains(tboxSalesSearch.Text) ||
                ord.Customer.Mobile.Contains(tboxSalesSearch.Text) ||
                ord.Customer.PostCode.Contains(tboxSalesSearch.Text) ||
                ord.Customer.Address.Contains(tboxSalesSearch.Text) ||
                ord.Customer.Email.Contains(tboxSalesSearch.Text) ||
                ord.OrderID.ToString().StartsWith(tboxSalesSearch.Text)
                 )
                )
            {
                sales.Add(item);
            }
            lvSales.DataContext = sales;
        }
    }
}
