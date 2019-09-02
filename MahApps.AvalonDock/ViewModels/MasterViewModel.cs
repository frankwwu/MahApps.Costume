using Common.Mvvm;
using System;
using System.Collections;
using System.Windows.Controls;

namespace MahApps.AvalonDock.ViewModels
{
    public class MasterViewModel : DockWindowViewModel
    {
        public event EventHandler<string> OpenDetail;

        public MasterViewModel()
        {
            DayOfWeek = Enum.GetValues(typeof(DayOfWeek));

            OpenCommand = new DelegateCommand<object>(Open);
        }

        public IList DayOfWeek { get; set; }

        #region OpenCommand

        public DelegateCommand<object> OpenCommand { get; private set; }

        public void Open(object parameter)
        {
            Button btn = parameter as Button;
            string day = btn.Content.ToString();

            if ((OpenDetail != null) && (OpenDetail != null))
            {
                OpenDetail(this, day);
            }
        }

        #endregion OpenCommand
    }
}
