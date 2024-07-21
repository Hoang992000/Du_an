using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.Report;

namespace TicketManagement.ViewModel.Report
{
    public class ReportByLocationViewModel : BaseViewModel
    {
        private ObservableCollection<LocationModel> _Location { get; set; }
        private ObservableCollection<VehicleModel> _Vehicle { get; set; }
        private LocationModel _SelectedItem { get; set; }
        private VehicleModel _SelectedItem1 { get; set; }
        private DateTime _DateStart;

        public DateTime DateStart
        {
            get { return _DateStart; }
            set { _DateStart = value; OnPropertyChanged(); }
        }
        private DateTime _DateEnd;

        public DateTime DateEnd
        {
            get { return _DateEnd; }
            set { _DateEnd = value; OnPropertyChanged(); }
        }
        public LocationModel SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }
        public VehicleModel SelectedItem1
        {
            get { return _SelectedItem1; }
            set { _SelectedItem1 = value; OnPropertyChanged(); }
        }

        public ObservableCollection<LocationModel> Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged(); }
        }
        public ObservableCollection<VehicleModel> Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; OnPropertyChanged(); }
        }
        public LocationRepository locationRepository { get; set; }
        public Ticketrepository TicketRepository { get; set; }
        public VehicleRepository VehicleRepository { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ReportByLocationViewModel()
        {
            locationRepository = LocationRepository.GetInstance();
            TicketRepository = Ticketrepository.GetInstance();
            VehicleRepository = VehicleRepository.GetInstance();
            Task.Run(Innit);
            ExportCommand = new RelayCommand<Window>((w) => CanExport(), (w) => ExecuteExport(w));
            CancelCommand = new RelayCommand<Window>((w) => true, (w) => w.Close());
        }
        private async void Innit()
        {
            List<VehicleModel> vehicles = new List<VehicleModel> { new VehicleModel() { VehicleId = 0, VehicleName = "All" } };
            vehicles.AddRange(await VehicleRepository.getListvehicle());
            List<LocationModel> locations = new List<LocationModel> { new LocationModel() { LocationId = 0, Locationname = "All" } };
            locations.AddRange(await locationRepository.getListLocation());
            Location = new ObservableCollection<LocationModel>(locations);
            Vehicle = new ObservableCollection<VehicleModel>(vehicles);
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now;
        }

        private void ExecuteExport(Window w)
        {
            loadReportData();
            w.Close();
        }
        private async void loadReportData()
        {
            ReportWindowView report = new ReportWindowView();
            List<TicketModelSTT> ticketModelSTTs = await TicketRepository.reportdetail(SelectedItem.LocationId, SelectedItem1.VehicleId, DateStart, DateEnd);
            int Total = await TicketRepository.getRevenue(SelectedItem.LocationId, SelectedItem1.VehicleId, DateStart, DateEnd);
            report.ReportDetail(ticketModelSTTs, Total, DateStart.ToString("dd/MM/yyyy"), DateEnd.ToString("dd/MM/yyyy"));
            report.Show();
        }
        private bool CanExport()
        {
            if (SelectedItem != null && SelectedItem1 != null) { return true; }
            return false;
        }
    }
}
