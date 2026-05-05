using PetrolStationNetwork.Data;
using PetrolStationNetwork.ViewModels;
using System.Windows.Controls;

namespace PetrolStationNetwork.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeliveryItems.xaml
    /// </summary>
    public partial class DeliveryItems : Page
    {
        public DeliveryItems()
        {
            InitializeComponent();
            DataContext = new VMDeliveryItems();
            if (UserSession.Role == "Supplier") { bthDelete.IsEnabled = true; deliveryPicker.IsEnabled = true; productPicker.IsEnabled = true; count.IsEnabled = true; expDate.IsEnabled = true; }
        }
    }
}
