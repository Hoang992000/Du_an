using RFIDApp.Components;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel.Location
{
    public class AddOrEditLocationViewModel : BaseViewModel
    {
        private LocationModelPage _CurrentLocation;
        public LocationModelPage CurrentLocation { get => _CurrentLocation; set { _CurrentLocation = value; OnPropertyChanged(); } }
        private string _LocationName;
        public string LocationName { get => _LocationName; set { _LocationName = value; OnPropertyChanged(); } }
        public string Title { get; set; }
        public bool IsNew { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private LocationRepository Repository;
        public AddOrEditLocationViewModel(LocationModelPage loca)
        {
            Repository = LocationRepository.GetInstance();
            CurrentLocation = loca;
            Task.Run(Initialize);
            SaveCommand = new RelayCommand<Window>((w) => CanSave(), (w) => ExecuteSave(w));
            CancelCommand = new RelayCommand<Window>((w) => CanClose(), (w) => ExecuteClose(w));
        }
        private void Initialize()
        {
            if (CurrentLocation != null)
            {
                Title = "SỬA ĐỊA ĐIỂM";
                LocationName = CurrentLocation.Locationname;
                IsNew = false;
            }
            else
            {
                Title = "THÊM MỚI ĐỊA ĐIỂM";
                IsNew = true;
            }
        }
        private void ExecuteClose(Window w)
        {
            w.Close();
        }

        private bool CanClose()
        {
            return true;
        }
        private bool CanSave()
        {
            if (string.IsNullOrEmpty(LocationName))
                return false;
            return true;
        }

        private void ExecuteSave(Window w)
        {
            if (IsNew)
            {
                SaveNewUser(w);
            }
            else
            {
                UpdateUser(w);
            }

        }

        private async void UpdateUser(Window w)
        {
            try
            {
                LocationModel locationModel = new LocationModel() { LocationId = CurrentLocation.LocationId, Locationname = LocationName, IsActive = false };
                LocationModel result = await Repository.UpdateLocation(locationModel);
                if (result.LocationId != 0)
                {
                    w.DialogResult = true;
                    w.Close();
                }
                else new CustomMessageBox(result.Locationname, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }

        private async void SaveNewUser(Window w)
        {
            try
            {
                LocationModel locationModel = new LocationModel() { Locationname = LocationName, IsActive = false };
                LocationModel result = await Repository.AddLocation(locationModel);
                if (result.LocationId != 0)
                {
                    w.DialogResult = true;
                    w.Close();
                }
                else new CustomMessageBox(result.Locationname, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
