using BLAPI;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        BL.IBL bl = BLFactory.GetBL();
        BO.Item item = new BO.Item();

        public AddItem()
        {
            InitializeComponent();
            DataContext = item;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddItem(item);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnChhoseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (fileDialog.ShowDialog() == true)
            {

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fileDialog.FileName);
                bitmap.EndInit();
                imgItemImage.Source = bitmap;
                item.Image = fileDialog.FileName;
            }
        }
    }
}
