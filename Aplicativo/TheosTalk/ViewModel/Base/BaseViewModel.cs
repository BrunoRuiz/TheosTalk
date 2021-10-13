using PropertyChanged;
using Xamarin.Forms;

namespace TheosTalk.ViewModel.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel
    {
        protected readonly INavigation Navigation;
        public bool IsBusy { get; set; }

        public BaseViewModel(INavigation navigation = null)
        {
            Navigation = navigation;
        }
    }
}
