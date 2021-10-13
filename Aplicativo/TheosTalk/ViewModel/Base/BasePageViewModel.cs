using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheosTalk.ViewModel.Base
{
    abstract class BasePageViewModel : BaseViewModel
    {
        internal abstract Task InitializeAsync(IEnumerable<object> parameters);
    }
}
