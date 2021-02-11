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

        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in bl.GetItems(it => true))
            {
                items.Add(item);
            }
            lvItems.DataContext = items;
        }

        //private void btnSale_Click(object sender, RoutedEventArgs e)
        //{
        //    OrderWindow orderWindow = new OrderWindow();
        //    orderWindow.Show();
        //}

        //private void btnFind_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void btnAdmin_Click(object sender, RoutedEventArgs e)
        //{
        //    ItemsWindow itemsWindow = new ItemsWindow();
        //    itemsWindow.Show();
        //}
    }
}
