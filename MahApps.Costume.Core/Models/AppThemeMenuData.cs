using ControlzEx.Theming;
using MahApps.Costume.Properties;
using System.Windows;

namespace MahApps.Costume.Models
{
    public class AppThemeMenuData : AccentColorMenuData
    {
        public AppThemeMenuData()
        {
            if (!string.IsNullOrEmpty(Settings.Default.Theme))
            {
                ThemeManager.Current.ChangeThemeBaseColor(Application.Current, Settings.Default.Theme);
            }
        }

        protected override void ChangeTheme(object sender)
        {
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, this.Name);
            Settings.Default.Theme = this.Name;
            Settings.Default.Save();
        }
    }
}
