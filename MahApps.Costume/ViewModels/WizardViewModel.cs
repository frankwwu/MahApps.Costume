using MahAppsDemo.Mvvm;
using Xceed.Wpf.Toolkit.Core;

namespace MahAppsDemo.ViewModels
{
    public class WizardViewModel : BindableBase
    {

        public WizardViewModel()
        {
            NextCommand = new DelegateCommand<object>(Next);
            FinishCommand = new DelegateCommand<object>(Finish);
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

        #region FinishCommand

        public DelegateCommand<object> FinishCommand { get; private set; }

        public void Finish(object parameter)
        {
            CancelRoutedEventArgs args = parameter as CancelRoutedEventArgs;
        }

        #endregion FinishCommand
    }
}
