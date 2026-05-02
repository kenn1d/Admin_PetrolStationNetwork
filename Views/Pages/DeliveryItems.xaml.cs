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
        }
    }
}
