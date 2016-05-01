using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;
using MahAppsDemo.Models;
using MahAppsDemo.Services;

namespace MahAppsDemo.ViewModels
{
    [Export(typeof(ViewModel))]
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;

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

        private WindowState _WindowState;

        public WindowState WindowState
        {
            get { return (WindowState)Enum.Parse(typeof(WindowState), Properties.Settings.Default.WindowState); }
            set { Properties.Settings.Default.WindowState = value.ToString(); Properties.Settings.Default.Save(); }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
