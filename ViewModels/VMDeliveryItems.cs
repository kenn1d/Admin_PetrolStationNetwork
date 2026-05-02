using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using PetrolStationNetwork.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PetrolStationNetwork.ViewModels
{
    public partial class VMDeliveryItems : ObservableObject
    {
        private DataContext dataBase = new DataContext();

        [ObservableProperty]
        private ObservableCollection<Models.Delivery> deliveries;

        [ObservableProperty]
        private ObservableCollection<Models.DeliveryItem> deliveryItems;

        [ObservableProperty]
        private ObservableCollection<Models.Product> products;

        // Поставки со статусом "В ожидании" "В обработке"
        [ObservableProperty]
        private ObservableCollection<Models.Delivery> deliveriesActive;

        [ObservableProperty]
        private string serialNumber;

        [ObservableProperty]
        private Models.DeliveryItem selectedItem;

        [ObservableProperty]
        private string selectedProduct;

        [ObservableProperty]
        private string bthAddContent;

        public ICommand Exit { get; }
        public ICommand Add { get; }
        public ICommand OnDelete { get; }

        public VMDeliveryItems()
        {
            dataBase.Deliveries.Load();
            dataBase.DeliveryItems.Load();

            this.deliveries = new ObservableCollection<Models.Delivery>(dataBase.Deliveries.ToList());
            this.deliveryItems = new ObservableCollection<Models.DeliveryItem>(dataBase.DeliveryItems.ToList());
            this.deliveriesActive = new ObservableCollection<Models.Delivery>(dataBase.Deliveries.Where(x => x.Status == "В ожидании" || x.Status == "В обработке").ToList());

            OnDelete = new RelayCommand(() => {
                if (Delete && SelectedItem != null)
                {
                    //dataBase.Deliveries.Remove(SelectedItem);
                    //deliveries.Remove(SelectedItem);
                    //dataBase.SaveChanges();
                    //SerialNumber = "";
                    //SelectedItem = null;
                    //BthAddContent = "Добавить";
                }
                else MessageBox.Show("Выберите запись для удаления", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
            });

            Exit = new RelayCommand(() => {
                MainWindow.init.frame.Navigate(new Views.Pages.Main(UserSession.Full_name));
            });
        }

        public bool Delete = false;

        //partial void OnSelectedItemChanged(Models.DeliveryItem item)
        //{
        //    if (item == null) return;

        //    SerialNumber = deliveries.FirstOrDefault(x => x.id == item.Delivery_id).Serial_number;
        //    SelectedProduct = products.FirstOrDefault(x => x.id == item.Product_id).;
        //    BthAddContent = "Изменить";
        //}
    }
}
