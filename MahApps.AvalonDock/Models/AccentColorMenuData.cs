using Common.Mvvm;
using MahApps.Metro;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace MahApps.AvalonDock.Models
{
    public class AccentColorMenuData
    {
        public AccentColorMenuData()
        {
            ChangeAccentCommand = new DelegateCommand<object>(this.ChangeAccent);
        }

        public string Name { get; set; }

        public string NameDisplay { get { return Regex.Replace(this.Name, "([a-z])([A-Z][a-z])", "$1 $2"); } }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public DelegateCommand<object> ChangeAccentCommand { get; private set; }

        private void ChangeAccent(object parameter)
        {
            ChangeTheme(parameter);
        }

        protected virtual void ChangeTheme(object sender)
        {
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            Accent accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
            SaveTheme(accent, theme.Item1);
        }

        protected void SaveTheme(Accent accent, AppTheme theme)
        {
            Properties.Settings.Default.ThemeName = theme.Name;
            Properties.Settings.Default.AccentName = accent.Name;
            Properties.Settings.Default.Save();
        }
    }
}
