using System.Threading.Tasks;
using Refit;
using TheosTalk.Dto.Model;
using TheosTalk.Helpers;
using TheosTalk.Service.Interface;

namespace TheosTalk.Service
{
    public class ApiIgrejaService : IApiIgrejaService
    {
        private static ApiIgrejaService _instance;
        public static ApiIgrejaService Instance = _instance ?? (_instance = new ApiIgrejaService());
        private readonly IApiIgrejaService _api;

        public ApiIgrejaService()
        {
            _api = RestService.For<IApiIgrejaService>("https://google.com.br/testeapi");
        }

        public Task<IgrejaDto> GetIgrejaByTagName(string tagName)
        {
            return Task.FromResult(IgrejaHelper.RetornaIgreja(tagName));
        }
    }
}
