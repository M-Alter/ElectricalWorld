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
using BLAPI;
using Microsoft.Win32;

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
            bl.AddItem(item);
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
            fileDialog.ShowDialog();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fileDialog.FileName);
            bitmap.EndInit();
            imgItemImage.Source = bitmap;
            item.Image = fileDialog.FileName;
        }
    }
}
