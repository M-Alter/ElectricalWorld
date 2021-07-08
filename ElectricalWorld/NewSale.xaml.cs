using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        BackgroundWorker backgroundWorkerCust;

        public NewSale()
        {
            InitializeComponent();
            lvBasket.DataContext = basket;
            backgroundWorkerCust = new BackgroundWorker();
            backgroundWorkerCust.WorkerSupportsCancellation = true;
            backgroundWorkerCust.DoWork += backgroundWorkerCust_DoWork;
            backgroundWorkerCust.RunWorkerCompleted += backgroundWorkerCust_RunWorkerCompleted;
            
            
        }

        private void backgroundWorkerCust_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmbCusts.ItemsSource = (List<PO.Customer>)e.Result;
        }


        private void backgroundWorkerCust_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<PO.Customer> bgListCust = new List<PO.Customer>();

            

            foreach (var item in bl.GetCutomers(cust =>
                cust.CustomerID.ToString().ToLower().Contains(e.Argument.ToString()) ||
                cust.Name.ToLower().Contains(e.Argument.ToString()) ||
                cust.Company.ToLower().Contains(e.Argument.ToString()) ||
                cust.Phone.ToLower().Contains(e.Argument.ToString()) ||
                cust.Mobile.ToLower().Contains(e.Argument.ToString()) ||
                cust.Address.ToLower().Contains(e.Argument.ToString()) ||
                cust.PostCode.ToLower().Contains(e.Argument.ToString()) ||
                cust.Email.ToLower().Contains(e.Argument.ToString())
                )
                )
            {
                bgListCust.Add(PO.Tools.POCustomer(item));
            }
            e.Result = bgListCust;

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
                basket.Add(new PO.Item
                {
                    ItemID = item.ItemID,
                    Brand = item.Brand,
                    ModelNumber = item.ModelNumber,
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Categories = from cat in item.Categories
                                 select cat
                });
            lblTotal.Content = basket.Sum(it => it.Price);
        }




        private void cmbCusts_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (backgroundWorkerCust.IsBusy == true)
                backgroundWorkerCust.CancelAsync();
            string input = (cmbCusts.Text + e.Text).ToLower();
            while (backgroundWorkerCust.IsBusy) ;
            backgroundWorkerCust.RunWorkerAsync(input);
            
           // custs.Clear();
            cmbCusts.IsDropDownOpen = true;

            

            //foreach (var item in bl.GetCutomers(cust =>
            //    cust.CustomerID.ToString().ToLower().Contains(input) /*||
            //    cust.Name.ToLower().Contains(input) ||
            //    cust.Company.ToLower().Contains(input) ||
            //    cust.Phone.ToLower().Contains(input) ||
            //    cust.Mobile.ToLower().Contains(input) ||
            //    cust.Address.ToLower().Contains(input) ||
            //    cust.PostCode.ToLower().Contains(input) ||
            //    cust.Email.ToLower().Contains(input)*/
            //    )
            //    )
            //{
            //    custs.Add(PO.Tools.POCustomer(item));
            //}
            //cmbCusts.ItemsSource = custs;
        }


        private void cmbCusts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            order.CustomerID = ((sender as ComboBox).SelectedItem as PO.Customer).CustomerID;
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


        private void btnEndSale_Click(object sender, RoutedEventArgs e)
        {
            order.Items = from item in basket
                          select item;
            order.OrderTime = DateTime.Now;
            order.TotalPrice = basket.Sum(it => it.Price);
            order.OrderID = bl.AddOrder(PO.Tools.BOOrder(order));
            var cust = bl.GetCutomers(c => c.CustomerID == order.CustomerID).FirstOrDefault();
            new Thread(() =>
            {
                Tools.CreateInvoice(order, PO.Tools.POCustomer(cust), false);
            }).Start();
            btnSend.IsEnabled = true;
            //Close();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {

            var cust = bl.GetCutomers(c => c.CustomerID == order.CustomerID).FirstOrDefault();
            PaymentWindow paymentWindow = new PaymentWindow(PO.Tools.POCustomer(cust), order.TotalPrice);
            paymentWindow.ShowDialog();

            if (paymentWindow.DialogResult == true)
            {
                order.Items = new List<PO.InvoiceItem>(basket.ToList().Concat(new List<PO.InvoiceItem> { new PO.Payment { Brand = "Paid", Price = -basket.Sum(it => it.Price) } }));
                order.Paid = true;
            }
            else
            {
                order.Items = from item in basket
                              select item;
            }
            order.OrderTime = DateTime.Now;
            order.TotalPrice = order.Items.Sum(it => it.Price);
            order.OrderID = bl.AddOrder(PO.Tools.BOOrder(order));
            new Thread(() =>
            {
                Tools.CreateInvoice(order, PO.Tools.POCustomer(cust), false);
            }).Start();
            btnSend.IsEnabled = true;
            //Close();
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
            order.Items = from item in basket
                          select item;
            order.OrderTime = DateTime.Now;
            order.TotalPrice = basket.Sum(it => it.Price);
            order.OrderID = bl.AddOrder(PO.Tools.BOOrder(order));
            var cust = bl.GetCutomers(c => c.CustomerID == order.CustomerID).FirstOrDefault();
            new Thread(() =>
            {
                Tools.CreateInvoice(order, PO.Tools.POCustomer(cust), true);
            }).Start();

            Close();
        }

        private void btnPayAndSendEmail_Click(object sender, RoutedEventArgs e)
        {
            var cust = bl.GetCutomers(c => c.CustomerID == order.CustomerID).FirstOrDefault();
            PaymentWindow paymentWindow = new PaymentWindow(PO.Tools.POCustomer(cust), order.TotalPrice);
            paymentWindow.ShowDialog();

            if (paymentWindow.DialogResult == true)
            {
                order.Items = new List<PO.InvoiceItem>(basket.ToList().Concat(new List<PO.InvoiceItem> { new PO.Payment { Brand = "Paid", Price = -basket.Sum(it => it.Price) } }));
                order.Paid = true;
            }
            else
            {
                order.Items = from item in basket
                              select item;
            }
            order.OrderTime = DateTime.Now;
            order.TotalPrice = order.Items.Sum(it => it.Price);
            order.OrderID = bl.AddOrder(PO.Tools.BOOrder(order));
            new Thread(() =>
            {
                Tools.CreateInvoice(order, PO.Tools.POCustomer(cust), true);
            }).Start();

            Close();
        }

        private void btnOverride_Click(object sender, RoutedEventArgs e)
        {
            PO.Item item = (sender as Button).DataContext as PO.Item;
            if (item is PO.Item)
            {
                OverridePrice overridePrice = new OverridePrice(item);
                overridePrice.ShowDialog();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var cust = bl.GetCutomers(c => c.CustomerID == order.CustomerID).FirstOrDefault();
            if (cust.Email != "")
                new Task(() =>
                {
                    Tools.EmailInvoice(order, PO.Tools.POCustomer(cust));
                }).Start();
        }
    }
}
