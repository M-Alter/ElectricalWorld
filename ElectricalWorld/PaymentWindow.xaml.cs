using System.Windows;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        public PaymentWindow(double totalPrice)
        {
            InitializeComponent();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }


    }
}
