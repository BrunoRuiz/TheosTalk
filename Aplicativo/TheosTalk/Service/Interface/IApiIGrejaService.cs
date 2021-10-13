using System.Threading.Tasks;
using Refit;
using TheosTalk.Dto.Model;

namespace TheosTalk.Service.Interface
{
    public interface IApiIgrejaService
    {
        [Get("/api/v1/GetByTagName")]
        Task<IgrejaDto> GetIgrejaByTagName(string tagName);
    }
}
