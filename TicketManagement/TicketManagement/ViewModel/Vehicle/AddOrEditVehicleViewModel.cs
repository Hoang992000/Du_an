using RFIDApp.Components;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel.Vehicle
{
    public class AddOrEditVehicleViewModel : BaseViewModel
    {
        private VehicleModel _CurrentVehicle { get; set; }
        public VehicleModel CurrentVehicle { get { return _CurrentVehicle; } set { _CurrentVehicle = value; OnPropertyChanged(); } }
        private string _VehicleName { get; set; }

        public string VehicleName
        {
            get { return _VehicleName; }
            set { _VehicleName = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }
        private int _Cost { get; set; }
        bool check;
        public int Cost
        {
            get { return _Cost; }
            set
            {
                _Cost = value; OnPropertyChanged();
                if (string.IsNullOrEmpty(_Cost.ToString())) check = false; else check = true;
                CommandManager.InvalidateRequerySuggested();
            }
        }
        public string Title { get; set; }
        public bool IsNew { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private VehicleRepository vehicleRepository { get; set; }
        public AddOrEditVehicleViewModel(VehicleModel vehi)
        {
            vehicleRepository = VehicleRepository.GetInstance();
            CurrentVehicle = vehi;
            Task.Run(Initialize);
            CancelCommand = new RelayCommand<Window>((w) => true, (w) => w.Close());
            SaveCommand = new RelayCommand<Window>((w) => CanSave(), (w) => ExecuteSave(w));
        }
        private void Initialize()
        {
            if (CurrentVehicle != null)
            {
                Title = "SỬA PHƯƠNG TIỆN";
                VehicleName = CurrentVehicle.VehicleName;
                Cost = CurrentVehicle.Cost;
                IsNew = false;
            }
            else
            {
                Title = "THÊM PHƯƠNG TIỆN";
                IsNew = true;
            }
        }
        private bool CanSave()
        {
            if (string.IsNullOrEmpty(VehicleName) || !check)
            {
                return false;
            }
            return true;
        }
        private void ExecuteSave(Window w)
        {
            if (IsNew)
            {
                SaveNewVehicle(w);
            }
            else
            {
                UpdateVehicle(w);
            }

        }
        private async void SaveNewVehicle(Window w)
        {
            var result = await vehicleRepository.AddVehicle(new VehicleModel() { VehicleName = VehicleName, Cost = Cost });
            if (result.VehicleId != null)
            {
                w.DialogResult = true;
                w.Close();
            }
            else new CustomMessageBox("Thêm thất bại", MessageType.Error, MessageButtons.Ok).ShowDialog();
        }
        private async void UpdateVehicle(Window w)
        {
            var result = await vehicleRepository.UpdateVehicle(new VehicleModel() { VehicleId = CurrentVehicle.VehicleId, VehicleName = VehicleName, Cost = Cost });
            if (result.VehicleId != null)
            {
                w.DialogResult = true;
                w.Close();
            }
            else new CustomMessageBox("Cập nhật thất bại", MessageType.Error, MessageButtons.Ok).ShowDialog();
        }
    }
}
