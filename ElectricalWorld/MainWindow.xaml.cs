using BL;
using PO;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLAPI.BLFactory.GetBL();
        ObservableCollection<PO.Item> items = new ObservableCollection<PO.Item>();
        ObservableCollection<PO.Item> stockItems = new ObservableCollection<PO.Item>();
        ObservableCollection<PO.Order> sales = new ObservableCollection<PO.Order>();
        ObservableCollection<PO.Order> custOrders = new ObservableCollection<PO.Order>();
        ObservableCollection<PO.Customer> custs = new ObservableCollection<PO.Customer>();


        public MainWindow()
        {
            InitializeComponent();


            this.Width = SystemParameters.WorkArea.Width;
            this.Height = SystemParameters.WorkArea.Height;
            this.Left = 0;
            this.Top = 0;
            this.WindowState = WindowState.Normal;

            //foreach (var item in bl.GetItems(it => true))
            //{
            //    stockItems.Add(PO.Tools.POItem(item));
            //}
            //dgrdStock.DataContext = stockItems;

        }

        private void lvItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Item item = (sender as ListView).SelectedItem as PO.Item;
            if (item is PO.Item)
            {
                grdItemDetails.DataContext = item;
                tabControl.SelectedIndex = 3;
                //ItemInfo itemInfo = new ItemInfo(item);
                //itemInfo.Show();
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
                ord.OrderID.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.CustomerID.ToString().ToLower() == tboxSalesSearch.Text.ToLower() ||
                ord.Customer.Name.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Company.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Phone.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Mobile.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.PostCode.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Address.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Email.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.OrderID.ToString().ToLower().StartsWith(tboxSalesSearch.Text.ToLower())
                 )
                )
            {
                sales.Add(PO.Tools.POOrder(item));
            }
            lvSales.DataContext = sales;
        }

        private void btnCustSearch_Click(object sender, RoutedEventArgs e)
        {
            custs.Clear();

            foreach (var item in bl.GetCutomers(cust =>
                cust.CustomerID.ToString().ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Name.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Company.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Phone.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Mobile.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Address.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.PostCode.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Email.ToLower().Contains(tboxCustSearch.Text.ToLower())
                )
                )
            {
                custs.Add(PO.Tools.POCustomer(item));
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
                items.Add(PO.Tools.POItem(item));
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(Environment.ExitCode);
            }

        }

        private void lvSales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Order order = (sender as ListView).SelectedItem as PO.Order;
            if (order is PO.Order)
            {
                grdOrderDetails.DataContext = order;
                //lvOrderView.DataContext = order.Items;
                //lblOrderDate.DataContext = order.OrderTime;
                tabControl.SelectedIndex = 1;
            }
        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            PO.Order order = (PO.Order)(sender as Button).DataContext;
            if (order is PO.Order)
                new Thread(() =>
                {
                    Tools.CreateInvoice(order);
                }).Start();
        }

        private void lvCusts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Customer customer = (sender as ListView).SelectedItem as PO.Customer;
            custOrders.Clear();
            custOrders = new ObservableCollection<Order>();
            foreach (var item in bl.GetOrders(order => order.Customer.CustomerID == customer.CustomerID))
            {
                custOrders.Add(PO.Tools.POOrder(item));
            }
            lvCustSales.DataContext = custOrders;
            grdCustInfo.DataContext = customer;
        }

        private void btnSaveCustChanges_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (sender as Button).DataContext as Customer;
            if (customer is Customer)
            {
                bl.EditCustomer(PO.Tools.BOCustomer(customer));
            }
        }

        private void btnProdSave_Click(object sender, RoutedEventArgs e)
        {
            PO.Item item = (sender as Button).DataContext as PO.Item;
            if (item is Item)
                bl.EditItem(PO.Tools.BOItem(item));
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

        private void tboxSalesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            sales.Clear();

            foreach (var item in bl.GetOrders(ord =>
                ord.OrderID.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.CustomerID.ToString().ToLower() == tboxSalesSearch.Text.ToLower() ||
                ord.Customer.Name.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Company.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Phone.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Mobile.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.PostCode.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Address.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.Customer.Email.ToLower().Contains(tboxSalesSearch.Text.ToLower()) ||
                ord.OrderID.ToString().ToLower().StartsWith(tboxSalesSearch.Text.ToLower())
                 )
                )
            {
                sales.Add(PO.Tools.POOrder(item));
            }
            lvSales.DataContext = sales;
        }

        private void tboxCustSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            custs.Clear();

            foreach (var item in bl.GetCutomers(cust =>
                cust.CustomerID.ToString().ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Name.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Company.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Phone.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Mobile.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Address.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.PostCode.ToLower().Contains(tboxCustSearch.Text.ToLower()) ||
                cust.Email.ToLower().Contains(tboxCustSearch.Text.ToLower())
                )
                )
            {
                custs.Add(PO.Tools.POCustomer(item));
            }
            lvCusts.DataContext = custs;
        }

        private void btnCustSave_Click(object sender, RoutedEventArgs e)
        {
            PO.Customer customer = (sender as Button).DataContext as PO.Customer;
            if (customer is PO.Customer)
            {
                bl.EditCustomer(PO.Tools.BOCustomer(customer));
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void chkBoxPaid_Click(object sender, RoutedEventArgs e)
        {
            PO.Order order = (PO.Order)(sender as CheckBox).DataContext;
            if (order is PO.Order)
            {
                bl.PayOrder(order.OrderID, order.Paid);
            }
        }
    }
}
