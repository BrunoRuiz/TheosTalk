using System.Collections.Generic;
using System.Threading.Tasks;
using TheosTalk.Model;
using TheosTalk.ViewModel.Base;

namespace TheosTalk.ViewModel
{
    class IgrejaPageViewModel: BasePageViewModel
    {

        public IgrejaModel Igreja { get; private set; }

        internal override Task InitializeAsync(IEnumerable<object> parameters)
        {
            if (parameters != null)
                foreach (var parameter in parameters)
                    if (parameter is IgrejaModel igrejaModel)
                        Igreja = igrejaModel;

             return Task.CompletedTask;
        }
    }
}
