using BLAPI;
using System;
using System.Windows;

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
