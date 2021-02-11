using BL;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        IBL bl = BLAPI.BLFactory.GetBL();
        BO.Customer cust = new BO.Customer();
        public AddCustomer()
        {
            InitializeComponent();
            this.DataContext = cust;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bl.AddCustomer(cust);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
