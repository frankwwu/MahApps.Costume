using Common.Mvvm;
using Xceed.Wpf.Toolkit.Core;

namespace MahAppsDemo.ViewModels
{
    public class WizardViewModel : BindableBase
    {
        private static int _count = 0;       

        public WizardViewModel()
        {
            Number = _count++;

            NextCommand = new DelegateCommand<object>(Next);
            FinishCommand = new DelegateCommand<object>(Finish);
        }

        private int _number;

        public int Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(); }
        }

        public bool CanNextFromPage1
        {
            get
            {
                return true;
            }
        }

        public bool CanFinish
        {
            get
            {
                return true;
            }
        }

        #region NextCommand

        public DelegateCommand<object> NextCommand { get; private set; }

        public void Next(object parameter)
        {
            CancelRoutedEventArgs args = parameter as CancelRoutedEventArgs;
        }

        #endregion NextCommand

        #region PreviousCommand

        public DelegateCommand<object> PreviousCommand { get; private set; }

        public void Previous(object parameter)
        {
            CancelRoutedEventArgs args = parameter as CancelRoutedEventArgs;
        }

        #endregion PreviousCommand

        #region FinishCommand

        public DelegateCommand<object> FinishCommand { get; private set; }

        public void Finish(object parameter)
        {
            CancelRoutedEventArgs args = parameter as CancelRoutedEventArgs;
        }

        #endregion FinishCommand
    }
}
