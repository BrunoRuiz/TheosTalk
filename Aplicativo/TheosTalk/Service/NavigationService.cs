using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheosTalk.View;
using TheosTalk.ViewModel;
using TheosTalk.ViewModel.Base;
using Xamarin.Forms;

namespace TheosTalk.Service
{
    sealed class NavigationService
    {
        internal Task Navigate<TViewModel>(IEnumerable<object> parameters = null) where TViewModel : BaseViewModel => InternalNavigate(typeof(TViewModel), parameters);

        private static Lazy<NavigationService> _lazy = new Lazy<NavigationService>(() => new NavigationService());
        private readonly Dictionary<Type, Type> _mappings;
        private Application CurrentApplication => Application.Current;
        private INavigation Navigation { get => CurrentApplication.MainPage.Navigation; }

        public static NavigationService Current { get => _lazy.Value; }


        private NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreateViewModelMappings();
        }

        private void CreateViewModelMappings()
        {
            _mappings.Add(typeof(MainPageViewModel), typeof(MainPage));
            _mappings.Add(typeof(IgrejaPageViewModel), typeof(IgrejaPage));
        }

        private async Task InternalNavigate(Type viewModelType, IEnumerable<object> parameter)
        {
            try
            {
                Page page = null;

                // Verificar se estou indo para a página inicial...
                if (viewModelType == typeof(MainPageViewModel))
                {
                    // Se a página inicial não foi carregada...
                    if (!Navigation.NavigationStack.Any(lbda => lbda.BindingContext.GetType() == typeof(MainPageViewModel)))
                    {
                        page = CreateAndBindPage(viewModelType);

                        var pagesToRemove = Navigation.NavigationStack.ToList();

                        await Navigation.PushAsync(page);

                        foreach (var pageToRemove in pagesToRemove)
                            Navigation.RemovePage(pageToRemove);
                    }
                    else
                        await GoBack(toRoot: true);
                }
                else
                {
                    page = CreateAndBindPage(viewModelType);

                    if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
                        await Navigation.PushModalAsync(page, true);
                    else
                        await Navigation.PushAsync(page, true);
                }

                if (!(page is null))
                {
                    if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
                        await (page.BindingContext as BaseModalPageViewModel).InitializeAsync(parameter);
                    else
                        await (page.BindingContext as BasePageViewModel).InitializeAsync(parameter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task GoBack(bool toRoot = false, bool animated = true)
        {
            if (toRoot)
                return Navigation.PopToRootAsync(animated);

            if (Navigation.ModalStack.Count > 0)
                return Navigation.PopModalAsync(animated);

            return Navigation.PopAsync(animated);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            try
            {
                if (!_mappings.ContainsKey(viewModelType))
                    throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");

                return _mappings[viewModelType];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            try
            {
                Type pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType == null)
                    throw new Exception($"Mapping type for {viewModelType} is not a page");

                Page page = Activator.CreateInstance(pageType) as Page;
                page.BindingContext = Activator.CreateInstance(viewModelType) as BaseViewModel;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async void InitializeApp(bool useNavigation = false)
        {
            var mainPage = CreateAndBindPage(typeof(MainPageViewModel));
            await (mainPage.BindingContext as BasePageViewModel).InitializeAsync(new List<object> { ApiIgrejaService.Instance, ApiCustomVisionService.Instance });

            if (useNavigation)
                CurrentApplication.MainPage = new NavigationPage(mainPage);
            else
                CurrentApplication.MainPage = mainPage;
        }

        internal void InitializeNavigation(IEnumerable<object> args = null)
        {
            CurrentApplication.MainPage = new NavigationPage(new MainPage());
            var page = (MainPage)((NavigationPage)CurrentApplication.MainPage).CurrentPage;

            (page.BindingContext as BasePageViewModel).InitializeAsync(null);

            Device.BeginInvokeOnMainThread(async () => await Navigate<MainPageViewModel>(new List<Object> { ApiIgrejaService.Instance, ApiCustomVisionService.Instance }));
        }
    }
}