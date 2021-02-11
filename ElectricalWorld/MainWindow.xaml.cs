using BL;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
        ObservableCollection<BO.Customer> custs = new ObservableCollection<BO.Customer>();


        public MainWindow()
        {
            InitializeComponent();

            //foreach (var item in bl.GetItems(it => true))
            //{
            //    items.Add(item);
            //}
            //lvItems.DataContext = items;

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
            sales.Clear();

            foreach (var item in bl.GetOrders(ord =>
                ord.Customer.CustomerID.ToString().ToLower() == tboxSalesSearch.Text.ToLower() ||
                ord.Customer.FirstName.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.LastName.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Phone.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Mobile.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.PostCode.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Address.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Email.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.OrderID.ToString().ToLower().StartsWith(tboxSalesSearch.Text.ToLower())
                 )
                )
            {
                sales.Add(item);
            }
            lvSales.DataContext = sales;
        }

        private void btnCustSearch_Click(object sender, RoutedEventArgs e)
        {
            custs.Clear();

            foreach (var item in bl.GetCutomers(cust =>
                cust.CustomerID.ToString().ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.FirstName.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.LastName.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Phone.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Mobile.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Address.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.PostCode.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Email.ToLower().Contains(tboxCustSearch.Text.ToLower())
                )
                )
            {
                custs.Add(item);
            }
            lvCusts.DataContext = custs;
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
                items.Add(item);
            }
            lvItems.DataContext = items;
        }

        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
        }

        private void btnAddSale_Click(object sender, RoutedEventArgs e)
        {
            NewSale newSale = new NewSale();
            newSale.Show();
        }
    }
}
