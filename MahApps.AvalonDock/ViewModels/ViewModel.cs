using Common.Mvvm;
using MahApps.AvalonDock.Models;
using MahApps.Metro;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MahApps.AvalonDock.ViewModels
{
    [Export(typeof(ViewModel))]
    public class ViewModel : BindableBase
    {
        [ImportingConstructor]
        public ViewModel()
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

            List<DockWindowViewModel> documents = new List<DockWindowViewModel>();
            MasterViewModel master = new MasterViewModel() { Title = "Master", CanClose = false };
            documents.Add(master);
            this.DockManagerViewModel = new DockManagerViewModel(documents);

            master.OpenDetail += (s, e) =>
            {
                DockWindowViewModel vm = this.DockManagerViewModel.Documents.FirstOrDefault(d => (d is DetailViewModel) && (d as DetailViewModel).Name.Equals(e));
                if (vm != null)
                {
                    this.DockManagerViewModel.ActiveDocument = vm;
                }
                else
                {
                    DetailViewModel doc = new DetailViewModel() { Title = e, Name = e };
                    this.DockManagerViewModel.AddDocument(doc);
                    this.DockManagerViewModel.ActiveDocument = doc;
                }
            };
        }

        public List<AccentColorMenuData> AccentColors { get; set; }

        public List<AppThemeMenuData> AppThemes { get; set; }

        private DockManagerViewModel _dockManagerViewModel;

        public DockManagerViewModel DockManagerViewModel
        {
            get { return _dockManagerViewModel; }
            private set { _dockManagerViewModel = value; }
        }
    }
}
