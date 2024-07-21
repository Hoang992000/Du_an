using System.Windows.Input;
using TicketManagement.ViewModel;

namespace TicketManagement.Models
{
    public class PaginationModel : BaseViewModel
    {
        public delegate void PageChangedEvent();
        public event PageChangedEvent PageChanged;

        private int _CurrentPage = 1;
        private int _StartIndex = 0;
        private int _EndIndex = 0;
        private int _TotalItems = 0;
        private int _TotalPages = 0;
        private int _PageSize = 20;
        public int CurrentPage
        {
            get => _CurrentPage; set
            {
                _CurrentPage = value;
                if (CurrentPage < 1)
                {
                    CurrentPage = 1;
                }
                else if (CurrentPage > TotalPages)
                {
                    CurrentPage = TotalPages;
                }
                OnPropertyChanged();
            }
        }
        public int StartIndex { get => _StartIndex; set { _StartIndex = value; OnPropertyChanged(); } }
        public int EndIndex { get => _EndIndex; set { _EndIndex = value; OnPropertyChanged(); } }
        public int TotalItems { get => _TotalItems; set { _TotalItems = value; OnPropertyChanged(); } }
        public int TotalPages { get => _TotalPages; set { _TotalPages = value; OnPropertyChanged(); } }
        public int PageSize { get => _PageSize; set { if (TotalItems == 0) return; CurrentPage = 1; _PageSize = value; PageChanged?.Invoke(); OnPropertyChanged(); } }
        public ICommand NextPageCommand { get; private set; }
        public ICommand PreviousPageCommand { get; private set; }
        public ICommand LastPageCommand { get; private set; }
        public ICommand FirstPageCommand { get; private set; }
        public ICommand InputCommand { get; private set; }
        public PaginationModel()
        {
            FirstPageCommand = new RelayCommand<object>((p) => CanFirstPage(), (p) => ExecuteFirstPage());
            LastPageCommand = new RelayCommand<object>((p) => CanLastPage(), (p) => ExecuteLastPage());
            NextPageCommand = new RelayCommand<object>((p) => CanNextPage(), (p) => ExecuteNextPage());
            PreviousPageCommand = new RelayCommand<object>((p) => CanPreviousPage(), (p) => ExecutePreviousPage());
            InputCommand = new RelayCommand<object>((p) => CanInputPage(), (p) => ExecuteInputPage());
        }

        private void ExecuteFirstPage()
        {
            CurrentPage = 1;
            CalculateIndexPage();
            PageChanged?.Invoke();
        }
        private void ExecuteLastPage()
        {
            CurrentPage = TotalPages;
            CalculateIndexPage();
            PageChanged?.Invoke();
        }
        private void ExecuteNextPage()
        {
            CurrentPage++;
            CalculateIndexPage();
            PageChanged?.Invoke();
        }
        private void ExecutePreviousPage()
        {
            CurrentPage--;
            CalculateIndexPage();
            PageChanged?.Invoke();
        }
        private void ExecuteInputPage()
        {
            CalculateIndexPage();
            PageChanged?.Invoke();
        }
        private bool CanPreviousPage()
        {
            if (CurrentPage <= 1 || TotalItems == 0)
                return false;
            return true;
        }
        private bool CanNextPage()
        {
            if (CurrentPage == TotalPages || TotalItems == 0)
                return false;
            return true;
        }
        private bool CanLastPage()
        {
            if (CurrentPage == TotalPages || TotalItems == 0)
                return false;
            return true;
        }
        private bool CanFirstPage()
        {
            if (CurrentPage <= 1 || TotalItems == 0)
                return false;
            return true;
        }
        private bool CanInputPage()
        {
            if (string.IsNullOrEmpty(CurrentPage.ToString()) || TotalItems == 0)
                return false;
            return true;
        }
        private void CalculateIndexPage()
        {
            StartIndex = (CurrentPage * PageSize) - (PageSize - 1);
            EndIndex = CurrentPage * PageSize;
            if (EndIndex > TotalItems)
            {
                EndIndex = TotalItems;
            }
        }
        public void InitPagination(int totalRow)
        {
            TotalItems = totalRow;
            if (totalRow > 0)
            {
                TotalPages = TotalItems / PageSize;
                if (TotalItems % PageSize != 0)
                {
                    TotalPages += 1;
                }
                StartIndex = (CurrentPage * PageSize) - (PageSize - 1);
                EndIndex = (CurrentPage * PageSize) > TotalItems ? TotalItems : CurrentPage * PageSize;
            }

        }
    }
}
