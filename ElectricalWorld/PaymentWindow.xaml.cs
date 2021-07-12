
using System.Windows;
using System.Windows.Input;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {

        PO.Customer cust;
        public PaymentWindow(PO.Order order)
        {
            cust = order.Customer;
            InitializeComponent();
            lblAmountDue.Content = order.TotalPrice - order.Paid;
        }

        public double AmountPaid { get; set; }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {

            //// configure client, request and HPP settings
            //var service = new HostedService(new GpEcomConfig
            //{
            //    MerchantId = "2101717993",
            //    AccountId = "internet",
            //    SharedSecret = "secret",
            //    ServiceUrl = "https://pay.sandbox.realexpayments.com/pay",
            //    HostedPaymentConfig = new HostedPaymentConfig
            //    {
            //        Version = "2"
            //    }
            //});

            //// Add 3D Secure 2 Mandatory and Recommended Fields
            //var hostedPaymentData = new HostedPaymentData
            //{
            //    CustomerEmail = cust.Email,
            //    CustomerPhoneMobile = "44|"+cust.Mobile,
            //    AddressesMatch = false
            //};

            //var billingAddress = new Address1
            //{
            //    StreetAddress1 = cust.Address1,
            //    StreetAddress2 = "",
            //    StreetAddress3 = "",
            //    City = cust.City,
            //    PostalCode = cust.City,
            //    Country = "826"
            //};

            //var shippingAddress = new Address1
            //{
            //    StreetAddress1 = cust.Address1,
            //    StreetAddress2 = "",
            //    StreetAddress3 = "",
            //    City = cust.City,
            //    PostalCode = cust.City,
            //    Country = "840",
            //};

            //try
            //{
            //    var hppJson = service.Charge(19.99m)
            //       .WithCurrency("GBP")
            //       .WithHostedPaymentData(hostedPaymentData)
            //       .WithAddress(billingAddress, AddressType.Billing)
            //       .WithAddress(shippingAddress, AddressType.Shipping)
            //       .Serialize();

            //    // TODO: pass the HPP request JSON to the JavaScript, iOS or Android Library
            //}

            //catch (ApiException exce)
            //{
            //    // TODO: Add your error handling here
            //}
            DialogResult = true;
            AmountPaid = double.Parse(tboxAmountPaid.Text);
            Close();
        }

        private void tboxAmountPaid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key ==  Key.Return)
            {
                DialogResult = true;
                AmountPaid = double.Parse(tboxAmountPaid.Text);
                Close();
            }

            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Decimal)
            {
                e.Handled = true;
            }
        }
    }
}
