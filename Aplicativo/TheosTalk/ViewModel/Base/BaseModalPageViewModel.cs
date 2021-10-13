using System.Threading.Tasks;

namespace TheosTalk.ViewModel.Base
{
    abstract class BaseModalPageViewModel : BaseViewModel
    {
        internal abstract Task InitializeAsync(object parameter);
    }
}
