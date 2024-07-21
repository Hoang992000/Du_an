using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TicketManagement.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Visibility _IsLoading;
        public Visibility IsLoading { get => _IsLoading; set { _IsLoading = value; OnPropertyChanged(); } }
        private bool _IsBoss;
        public bool IsBoss { get => _IsBoss; set { _IsBoss = value; OnPropertyChanged(); } }

        public bool _IsExecuting = false;
        public bool IsExecuting
        {
            get => _IsExecuting;
            set
            {
                _IsExecuting = value;
                IsLoading = value ? Visibility.Visible : Visibility.Hidden;
                CanExecute = !value;
                OnPropertyChanged();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CommandManager.InvalidateRequerySuggested();

                });
            }
        }

        private bool _CanExecute;
        public bool CanExecute { get => _CanExecute; set { _CanExecute = value; OnPropertyChanged(); } }


    }
    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
