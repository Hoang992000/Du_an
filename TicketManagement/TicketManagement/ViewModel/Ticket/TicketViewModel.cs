using RFIDApp.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel.Ticket
{
    public class TicketViewModel : BaseViewModel
    {
        private ObservableCollection<TicketModelSTT> _ListTicket;
        private PaginationModel _Pagination;
        private string _SearchNum;
        public string SearchNum { get => _SearchNum; set { _SearchNum = value; OnPropertyChanged(); } }
        public ObservableCollection<TicketModelSTT> ListTicket { get => _ListTicket; set { _ListTicket = value; OnPropertyChanged(); } }
        public PaginationModel Pagination { get => _Pagination; set { _Pagination = value; OnPropertyChanged(); } }

        private Ticketrepository ticketRepository;
        public ICommand SearchCommand { get; set; }
        public TicketViewModel()
        {
            Task.Run(Initialize);
            SearchCommand = new RelayCommand<object>((p) => CanSearch(), (p) => ExecuteSearch(p));
        }
        private async void Initialize()
        {
            try
            {
                IsExecuting = true;
                ticketRepository = Ticketrepository.GetInstance();
                Pagination = new PaginationModel();
                Pagination.PageChanged += () =>
                {
                    LoadPageData();
                };
                LoadPageData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsExecuting = false;
            }
        }
        private bool CanSearch()
        {
            return true;
        }

        private void ExecuteSearch(object p)
        {
            Pagination.CurrentPage = 1;
            LoadPageData(SearchNum);
            SearchNum = string.Empty;
        }
        private async void LoadPageData(string userName = null)
        {
            try
            {
                IsExecuting = true;
                if (string.IsNullOrEmpty(userName))
                {
                    ListTicket = new ObservableCollection<TicketModelSTT>(await ticketRepository.GetAllData(Pagination.PageSize, Pagination.CurrentPage));
                }
                else
                {
                    ListTicket = new ObservableCollection<TicketModelSTT>(await ticketRepository.SearchUserByUserName(userName, Pagination.PageSize, Pagination.CurrentPage));

                }

                if (ListTicket.Count > 0)
                {
                    int Total = await ticketRepository.GetTotalRow();
                    Pagination.InitPagination(Total);
                }
                else
                {
                    Pagination.InitPagination(0);
                }
            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                IsExecuting = false;

            }

        }
    }
}
