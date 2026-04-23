using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using PetrolStationNetwork.Data;
using PetrolStationNetwork.Views.Pages;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Delivery = PetrolStationNetwork.Models.Delivery;
using Supplier = PetrolStationNetwork.Models.Supplier;

namespace PetrolStationNetwork.ViewModels
{
    public partial class VMDelivery : ObservableObject
    {
        private DataContext dataBase = new DataContext();

        [ObservableProperty]
        private ObservableCollection<Delivery> deliveries;

        [ObservableProperty]
        private ObservableCollection<Supplier> suppliers;

        [ObservableProperty]
        private string serialNumber;

        [ObservableProperty]
        private Delivery selectedItem;

        [ObservableProperty]
        private string selectedStatus;

        [ObservableProperty]
        private string bthAddContent;

        public ICommand Exit { get; }
        public ICommand Add { get; }
        public ICommand OnDelete { get; }

        //TODO: Реализовать редактирование только своей записи
        //TODO: Реализовать проверку на дублирование
        public VMDelivery()
        {
            dataBase.Deliveries.Load();
            dataBase.Suppliers.Load();
            BthAddContent = "Добавить";

            this.deliveries = new ObservableCollection<Delivery>(dataBase.Deliveries.ToList());
            this.suppliers = new ObservableCollection<Supplier>(dataBase.Suppliers.ToList());

            if (UserSession.Role == "leader") Delete = true;
            Add = new RelayCommand(() => {
                if (selectedItem == null)
                {
                    Delivery newDelivery = new Delivery()
                    {
                        Supplier_id = UserSession.Id,
                        Serial_number = serialNumber,
                        Date = DateTime.Now,
                        Status = "В ожидании"
                    };
                    dataBase.Deliveries.Add(newDelivery);
                    deliveries.Add(newDelivery);
                }
                else
                {
                    selectedItem.Serial_number = serialNumber;
                    selectedItem.Status = selectedStatus;
                    SelectedItem = null;
                    BthAddContent = "Добавить";
                }
                dataBase.SaveChanges();
            });

            OnDelete = new RelayCommand(() => { 
                if (Delete && SelectedItem != null)
                {
                    dataBase.Deliveries.Remove(SelectedItem);
                    deliveries.Remove(SelectedItem);
                    dataBase.SaveChanges();
                    SerialNumber = "";
                    SelectedItem = null;
                    BthAddContent = "Добавить";
                }
                else MessageBox.Show("Выберите запись для удаления.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
            });

            Exit = new RelayCommand(() => {
                MainWindow.init.frame.Navigate(new Main(UserSession.Full_name));
            });
        }

        public bool Delete = false;

        partial void OnSelectedItemChanged(Delivery item)
        {
            if (item == null) return;

            SerialNumber = item.Serial_number;
            SelectedStatus = item.Status;
            BthAddContent = "Изменить";
        }
    }
}
