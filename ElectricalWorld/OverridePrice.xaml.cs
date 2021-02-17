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
