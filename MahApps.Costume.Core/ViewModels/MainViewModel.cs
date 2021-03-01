using ControlzEx.Theming;
using MahApps.Costume.Models;
using MahApps.Costume.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MahApps.Costume.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Icon = BitmapFrame.Create(new Uri(@"pack://application:,,,/MahApps.Costume.Core;component/Resources/app-icon.png"));
            Themes = ThemeManager.Current.Themes.OrderBy(t => t.Name).ToList();
        }

        #region Public Properties

        private List<AccentColorMenuData> _accentColors;

        public List<AccentColorMenuData> AccentColors
        {
            get
            {
                if (_accentColors == null)
                {
                    _accentColors = ThemeManager.Current.Themes
                                            .GroupBy(x => x.ColorScheme)
                                            .OrderBy(a => a.Key)
                                            .Select(a => new AccentColorMenuData { Name = a.Key, ColorBrush = a.First().ShowcaseBrush })
                                            .ToList();
                }
                return _accentColors;
            }
        }


        private List<AppThemeMenuData> _appThemes;

        public List<AppThemeMenuData> AppThemes
        {
            get
            {
                if (_appThemes == null)
                {
                    _appThemes = ThemeManager.Current.Themes
                                         .GroupBy(x => x.BaseColorScheme)
                                         .Select(x => x.First())
                                         .Select(a => new AppThemeMenuData() { Name = a.BaseColorScheme, BorderColorBrush = a.Resources["MahApps.Brushes.ThemeForeground"] as Brush, ColorBrush = a.Resources["MahApps.Brushes.ThemeBackground"] as Brush })
                                         .ToList();
                }
                return _appThemes;
            }
        }

        public WindowState WindowState
        {
            get { return (WindowState)Enum.Parse(typeof(WindowState), Settings.Default.WindowState); }
            set { Properties.Settings.Default.WindowState = value.ToString(); Settings.Default.Save(); }
        }

        private ImageSource _icon;

        public ImageSource Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(); }
        }

        private string _title = "MahApps MainWindow";

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private List<Theme> _themes;

        public List<Theme> Themes
        {
            get { return _themes; }
            set { _themes = value; OnPropertyChanged(); }
        }

        #endregion Public Properties

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
