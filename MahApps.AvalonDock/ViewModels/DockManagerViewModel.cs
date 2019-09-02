using Common.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MahApps.AvalonDock.ViewModels
{
    public class DockManagerViewModel : BindableBase
    {
        public DockManagerViewModel(IEnumerable<DockWindowViewModel> dockWindowViewModels)
        {
            this.Documents = new ObservableCollection<DockWindowViewModel>();
            this.Anchorables = new ObservableCollection<object>();

            foreach (DockWindowViewModel vm in dockWindowViewModels)
            {
                AddDocument(vm);
            }
        }

        #region Public Properties

        private ObservableCollection<DockWindowViewModel> _documents;

        public ObservableCollection<DockWindowViewModel> Documents
        {
            get { return _documents; }
            private set { _documents = value; }
        }

        private DockWindowViewModel _activeDocument;

        public DockWindowViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set { _activeDocument = value; OnPropertyChanged(); }
        }

        public ObservableCollection<object> Anchorables { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void AddDocument(DockWindowViewModel document)
        {
            document.PropertyChanged += OnPropertyChanged;
            if (!document.IsClosed)
                this.Documents.Add(document);
        }

        #endregion Public Methods

        #region Private Methods

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DockWindowViewModel document = sender as DockWindowViewModel;

            if (e.PropertyName == nameof(DockWindowViewModel.IsClosed))
            {
                if (!document.IsClosed)
                    this.Documents.Add(document);
                else
                    this.Documents.Remove(document);
            }
        }

        #endregion Private Methods
    }
}
