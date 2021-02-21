using BLAPI;
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
    /// Interaction logic for AddStock.xaml
    /// </summary>
    public partial class AddStock : Window
    {

        BL.IBL bl = BLFactory.GetBL();
        PO.Item item = new PO.Item();

        public AddStock(PO.Item item)
        {
            InitializeComponent();
            this.item = item;
            DataContext = item;
            grdItem.DataContext = item;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddStock(new BO.Item 
                {
                    ItemID = item.ItemID,
                    Brand = item.Brand,
                    ModelNumber = item.ModelNumber,
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                },
                item.CostPrice);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
