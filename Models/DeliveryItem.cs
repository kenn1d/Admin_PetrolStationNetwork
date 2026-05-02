using CommunityToolkit.Mvvm.ComponentModel;

namespace PetrolStationNetwork.Models
{
    public partial class DeliveryItem : ObservableObject
    {
        public int id { get; set; }

        [ObservableProperty]
        private int delivery_id;

        [ObservableProperty]
        private int product_id;

        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private DateTime exp_date;
    }
}
