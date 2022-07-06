using ControlzEx.Theming;
using IdeaForge.Service.GenericServices;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider serviceProvider { get; private set; }
        public App()
        {
//ThemeManager.Current.ChangeTheme(this, "Gr");
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            Barrel.ApplicationId = "ideaForgeWPF";
        }

        private void ConfigureServices(ServiceCollection services)
        {
            DataServices(services);
            services.AddSingleton<Login>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            
            
                var mainWindow = serviceProvider.GetService<Login>();
                mainWindow.Show();
            
          
        }

        public void DataServices(ServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IProfileSerevice, ProfileService>();


        }
    }
}
