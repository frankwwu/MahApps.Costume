using ControlzEx.Theming;
using MahApps.Costume.Mvvm;
using MahApps.Costume.Properties;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MahApps.Costume.Models
{
    public class AccentColorMenuData
    {
        #region Public Properties

        public string Name { get; set; }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public AccentColorMenuData()
        {
            this.ChangeAccentCommand = new DelegateCommand<object>(this.ChangeTheme);
            if (!string.IsNullOrEmpty(Settings.Default.Accent))
            {
                ThemeManager.Current.ChangeThemeColorScheme(Application.Current, Settings.Default.Accent);
            }
        }

        public ICommand ChangeAccentCommand { get; private set; }


        protected virtual void ChangeTheme(object sender)
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, this.Name);
            Settings.Default.Accent = this.Name;
            Settings.Default.Save();
        }

        #endregion Public Properties
    }
}
