using System.Threading.Tasks;
using Plugin.Media.Abstractions;

using Refit;

namespace TheosTalk.Service.Interface
{
    public interface IApiCustomVision
    {
        [Post("/url")]
        Task<string> GetPrediction(string url);

        [Post("/image")]
        Task<string> GetPrediction(byte[] image);

        Task<string> GetPrediction(MediaFile photo);
    }
}
