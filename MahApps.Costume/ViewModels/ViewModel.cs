using MahApps.Metro;
using MahAppsDemo.Models;
using MahAppsDemo.Mvvm;
using MahAppsDemo.Services;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MahAppsDemo.ViewModels
{
    [Export(typeof(ViewModel))]
    public class ViewModel : BindableBase
    {
        private readonly IDataService _dataService;
        private Window _window;

        [ImportingConstructor]
        public ViewModel(IDataService dataService)
        {
            this.AccentColors = ThemeManager.Accents.Select(a => new AccentColorMenuData()
            {
                Name = a.Name,
                ColorBrush = a.Resources["AccentColorBrush"] as Brush
            }).ToList();

            this.AppThemes = ThemeManager.AppThemes.Select(a => new AppThemeMenuData()
            {
                Name = a.Name,
                BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                ColorBrush = a.Resources["WhiteColorBrush"] as Brush
            }).ToList();

            AppTheme theme = ThemeManager.AppThemes.FirstOrDefault(t => t.Name.Equals(Properties.Settings.Default.ThemeName));
            Accent accent = ThemeManager.Accents.FirstOrDefault(a => a.Name.Equals(Properties.Settings.Default.AccentName));
            if ((theme != null) && (accent != null))
            {
                ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
            }

            _dataService = dataService;
            _dataService.LoadData();
            DataItems = _dataService.DataItems;
            SpecialFolders = _dataService.SpecialFolders;
        }

        public List<AccentColorMenuData> AccentColors { get; set; }

        public List<AppThemeMenuData> AppThemes { get; set; }

        public List<DataItem> DataItems { get; private set; }

        public List<SpecialFolder> SpecialFolders { get; private set; }

        private DataItem _selectedDataItem;

        public DataItem SelectedDataItem
        {
            get
            {
                return _selectedDataItem;
            }
            set
            {
                _selectedDataItem = value;
                OnPropertyChanged();
                IsEditingDataItem = false;
            }
        }

        private bool _isEditingDataItem;

        public bool IsEditingDataItem
        {
            get { return _isEditingDataItem; }
            set { _isEditingDataItem = value; OnPropertyChanged(); }
        }

        #region OpenWizardCommand

        public ICommand OpenWizardCommand
        {
            get { return new DelegateCommand<object>(OpenWizard, o => { return true; }); }
        }

        internal void OpenWizard(object parameter)
        {
            Xceed.Wpf.Toolkit.Wizard wizard = parameter as Xceed.Wpf.Toolkit.Wizard;
            if (wizard != null)
            {
                WizardViewModel viewModel = new WizardViewModel();
                wizard.DataContext = viewModel;
                wizard.CurrentPage = wizard.Items[0] as Xceed.Wpf.Toolkit.WizardPage;
                if (_window != null)
                {
                    _window.Content = null;
                    _window = null;
                }
                _window = new System.Windows.Window();
                _window.Owner = Application.Current.MainWindow;
                _window.Title = "Demo Wizard";
                _window.Content = wizard;
                _window.Width = 600;
                _window.Height = 400;
                _window.WindowStyle = WindowStyle.ToolWindow;
                _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                // Window will be closed by Wizard because FinishButtonClosesWindow = true and CancelButtonClosesWindow = true
                bool? ret = _window.ShowDialog();
                if ((ret.HasValue) && (ret.Value))
                {
                    // Do something here.
                }
            }
        }

        #endregion EditCommand       
    }
}
