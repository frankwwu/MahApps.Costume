using MahAppsDemo.Mvvm;

namespace MahAppsDemo.ViewModels
{
    public class WizardViewModel : BindableBase
    {
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
    }
}
