using MeApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        Dictionary<string, Type> routes = new Dictionary<string, Type>();

        public ICommand NavigateCommand => new Command(Navigate);
        public ICommand SettingsCommand => new Command( async () => await PushPage(new SettingsPage()));

        private async Task PushPage(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page);
            Shell.Current.FlyoutIsPresented = false;
        }

        private async void Navigate(object route)
        {
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{route.ToString()}");
            Shell.Current.FlyoutIsPresented = false;
        }

        void RegisterRoutes()
        {
            routes.Add("articles", typeof(ArticlesPage));
            routes.Add("photos", typeof(PhotosPage));
            routes.Add("projects", typeof(ProjectsPage));
            routes.Add("settings", typeof(SettingsPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}