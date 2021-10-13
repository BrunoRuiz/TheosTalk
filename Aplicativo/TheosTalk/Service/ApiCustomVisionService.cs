using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using TheosTalk.Helpers;
using TheosTalk.Service.Interface;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using System.Linq;
using HttpExtension;
using TheosTalk.Model;
using Refit;

namespace TheosTalk.Service
{
    public class ApiCustomVisionService : IApiCustomVision
    {

        private static ApiCustomVisionService _instance;
        public static ApiCustomVisionService Instance = _instance ?? (_instance = new ApiCustomVisionService());
        private readonly IApiCustomVision _api;

        public ApiCustomVisionService()
        {
            _api = RestService.For<IApiCustomVision>(CustomVisionHelper.CustomVisionEndPoint);
        }

        public async Task<string> GetPrediction(string url) 
        {
            try
            {
                var httpClient = new HttpClient();
               
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Prediction-Key", CustomVisionHelper.PredictionKey);

                Uri urlImage = new Uri(url);

                var response = await httpClient.PostAsync<CustomVisionPredictionModel>($"{CustomVisionHelper.CustomVisionApiUrl}", urlImage);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Value.Predictions[0]?.TagName;
                }
                else
                {
                    Debug.WriteLine(response.Error.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }

        }

        public async Task<string> GetPrediction(byte[] image)
        {
            try
            {
                var httpClient = new HttpClient();                
                httpClient.DefaultRequestHeaders.Add("Prediction-Key", CustomVisionHelper.PredictionKey);

                var response = await httpClient.PostAsync<CustomVisionPredictionModel>($"{CustomVisionHelper.CustomVisionApiImage}", image, "application/octet-stream");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Value.Predictions[0]?.TagName;
                }
                else
                {
                    Debug.WriteLine(response.Error.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public async Task<string> GetPrediction(MediaFile photo)
        {
            try
            {
                var endpoint = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(CustomVisionHelper.PredictionKey))
                {
                    Endpoint = CustomVisionHelper.CustomVisionEndPoint
                };

                var results = await endpoint.ClassifyImageAsync(Guid.Parse(CustomVisionHelper.ProjectId),
                                                              CustomVisionHelper.PublishNameIteration,
                                                              photo.GetStream());
                  
                return results.Predictions?.Where(x => (x.Probability >= 0.8)).FirstOrDefault().TagName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }

        }

    }
}