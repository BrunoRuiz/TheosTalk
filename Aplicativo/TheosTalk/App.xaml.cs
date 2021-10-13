using TheosTalk.Service;
using Xamarin.Forms;

namespace TheosTalk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NavigationService.Current.InitializeApp(useNavigation: true);
        }

        protected override void OnStart() {}

        protected override void OnSleep() {}

        protected override void OnResume() {}
    }
}
