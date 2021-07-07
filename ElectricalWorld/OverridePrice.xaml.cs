using System.Windows;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for OverridePrice.xaml
    /// </summary>
    public partial class OverridePrice : Window
    {
        PO.Item item;
        public OverridePrice(PO.Item item)
        {
            InitializeComponent();
            this.item = item;
            this.DataContext = item;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
