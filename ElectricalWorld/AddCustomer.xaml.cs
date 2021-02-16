using BL;
using System.Windows;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        IBL bl = BLAPI.BLFactory.GetBL();
        internal BO.Customer cust = new BO.Customer();
        public AddCustomer()
        {
            InitializeComponent();
            this.DataContext = cust;
            cust.City = "London";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bl.AddCustomer(cust);
            //DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
