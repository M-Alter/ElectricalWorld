using BLAPI;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ElectricalWorld
{
    /// <summary>
    /// Interaction logic for ItemInfo.xaml
    /// </summary>
    public partial class ItemInfo : Window
    {
        BL.IBL bl = BLFactory.GetBL();
        PO.Item item;
        public static readonly DependencyProperty ItemIsEditable = DependencyProperty.Register("isInEditMode", typeof(bool), typeof(ItemInfo));


        private bool isInEditMode
        {
            get { return (bool)GetValue(ItemIsEditable); }
            set { SetValue(ItemIsEditable, value); }
        }

        public ItemInfo(PO.Item item)
        {
            InitializeComponent();
            this.item = item;
            this.DataContext = item;
            isInEditMode = false;
            if (item.Image != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(item.Image);
                bitmap.EndInit();

                imgItemImage.Source = bitmap;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bl.AddItem(PO.Tools.BOItem(item));
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
