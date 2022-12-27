using ControlzEx.Theming;
using IdeaForge.Service.GenericServices;
using IdeaForge.Service.IGenericServices;
using IdeaForge.Services;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(App));
        public App()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            Barrel.ApplicationId = "ideaForgeWPF";
            //ThemeManager.Current.ChangeTheme(this, "Gr");
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            
        }

        private void ConfigureServices(ServiceCollection services)
        {
            DataServices(services);
            services.AddSingleton<Login>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Info("        =============  Started Logging  =============        ");
                var mainWindow = serviceProvider.GetService<Login>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                
               
            }
            
                
            
          
        }

        public void DataServices(ServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IProfileSerevice, ProfileService>();
            services.AddScoped<IPilotRequestServices, PilotRequest>();
            services.AddScoped<IGoogleMapsApiService, GoogleMapsApiService>();
            services.AddScoped<IAdminRequestServices, AdminRequest>();
            services.AddScoped<IAdminRequestPage, AdminRequestService>();

        }
    }
}
