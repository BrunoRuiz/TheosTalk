using System;
using System.Windows.Input;
using Plugin.Media;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using TheosTalk.Model;
using System.IO;
using TheosTalk.Service.Interface;
using TheosTalk.ViewModel.Base;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheosTalk.Service;
using AutoMapper;
using TheosTalk.AutoMapper;

namespace TheosTalk.ViewModel
{
    class MainPageViewModel: BasePageViewModel
    {
        private IApiCustomVision _apiCustomVisionService;
        private IApiIgrejaService _apiIgrejaService;
        private IMapper _mapper;

        public ICommand VerificarIgrejaCommand { get; private set; }

        public void Initialize()
        {
            _mapper = IgrejaDomainDtoMapper.CreateMapper();
            VerificarIgrejaCommand = new Command(ExecuteVerificarIgrejaCommand);            
        }
        
        internal override Task InitializeAsync(IEnumerable<object> parameters)
        {
            Initialize();

            if (parameters != null)
                foreach (var parameter in parameters)
                {
                    if (parameter is IApiIgrejaService apiIgrejaService)
                        _apiIgrejaService = apiIgrejaService;

                    if (parameter is IApiCustomVision apiCustomVisionService)
                        _apiCustomVisionService = apiCustomVisionService;
                }

            return Task.CompletedTask;
        }

        private async void ExecuteVerificarIgrejaCommand()
        {
            #region CONTROLES CROSSMEDIA PARA ACESSO A CAMERA
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Erro na Camera", "Camera indisponivel.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Igrejas",
                Name = $"igreja{DateTime.Now:ddMMyyyyHHmmss}.jpg",
                PhotoSize = PhotoSize.Small
            });

            if (file == null) return;

            #endregion

            #region PROCESSANDO RESPOSTA DO CUSTOMVISION
            string result = string.Empty;
            IgrejaModel Igreja = null;
            using (var Dialog = UserDialogs.Instance.Loading("Processando imagem...", null, null, true, MaskType.Gradient))
            {
                var fileInfo = new FileInfo(file.Path);

                result = await _apiCustomVisionService.GetPrediction(file);

                if (string.IsNullOrEmpty(result))
                {
                    await App.Current.MainPage.DisplayAlert("TheòsTalk", "Igreja não encontrada.", "OK");
                    return;
                }

                var igrejaDto = await _apiIgrejaService.GetIgrejaByTagName(result);
                Igreja = _mapper.Map<IgrejaModel>(igrejaDto);
            }
            #endregion

            //SE ENCONTRAR ENTAO VAMOS MOSTRAR EM UMA NOVA PAGE OS DETALHES DA IGREJA ENCONTRADA.
            if (Igreja != null)
                await NavigationService.Current.Navigate<IgrejaPageViewModel>(new List<Object> { Igreja });
        }
    }
}
