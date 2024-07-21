using RFIDApp.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.Vehicle;

namespace TicketManagement.ViewModel.Vehicle
{
    public class VehicleViewModel : BaseViewModel
    {
        private ObservableCollection<VehicleModel> _ListVehicle;
        public ObservableCollection<VehicleModel> ListVehicle { get => _ListVehicle; set { _ListVehicle = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private VehicleRepository vehicleRepository;
        public VehicleViewModel()
        {
            vehicleRepository = VehicleRepository.GetInstance();
            Task.Run(LoadData);
            AddCommand = new RelayCommand<object>((p) => true, (p) => ExecuteAdd());
            EditCommand = new RelayCommand<VehicleModel>((u) => true, (u) => ExecuteEdit(u));
            DeleteCommand = new RelayCommand<VehicleModel>((p) => true, (p) => ExecuteDelete(p));
        }
        private async void ExecuteDelete(VehicleModel p)
        {
            bool? result = new CustomMessageBox("Bạn muốn xoá à?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (result.Value)
            {
                string res = await vehicleRepository.DeleteVehicle(p);
                if (res == "Success")
                {
                    LoadData();
                }
                MessageBox.Show(res, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ExecuteAdd()
        {
            AddOrEditVehicle window = new AddOrEditVehicle();
            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadData();
            }
        }
        private void ExecuteEdit(VehicleModel u)
        {
            AddOrEditVehicle window = new AddOrEditVehicle(u);
            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadData();
            }
        }
        private async void LoadData()
        {
            List<VehicleModel> listVehicle = await vehicleRepository.getListvehicle();
            int i = 1;
            foreach (VehicleModel vehicle in listVehicle)
            {
                vehicle.STT = i;
                i++;
            }
            ListVehicle = new ObservableCollection<VehicleModel>(listVehicle);
        }
    }
}
