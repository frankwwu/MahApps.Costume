using Common.Mvvm;

namespace MahApps.AvalonDock.ViewModels
{
    public abstract class DockWindowViewModel : BindableBase
    {
        public DockWindowViewModel()
        {
            this.CanClose = true;
            this.IsClosed = false;

            // Commands
            this.CloseCommand = new DelegateCommand<object>(this.Close);
        }

        #region Public Properties

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private bool _isClosed;

        public bool IsClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; OnPropertyChanged(); }
        }

        private bool _canClose;

        public bool CanClose
        {
            get { return _canClose; }
            set { _canClose = value; OnPropertyChanged(); }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }

        #endregion Public Properties

        #region CloseCommand

        public DelegateCommand<object> CloseCommand { get; private set; }

        public void Close(object parameter)
        {
            if (CanClose)
            {
                this.IsClosed = true;
            }
        }

        #endregion
    }
}
