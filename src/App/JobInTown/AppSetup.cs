using Acr.UserDialogs;
using Autofac;
using AutoMapper;
using AzureStorage;
using AzureStorage.Contracts;
using FilePicker;
using FilePicker.Contracts;
using JobInTown.Azure.Client;
using JobInTown.Azure.Client.Contracts;
using JobInTown.Azure.Client.Models;
using JobInTown.Models;
using JobInTown.ViewModels;
using Navigation;
using Navigation.Contracts;
using Network;
using Network.Adapters;
using Network.Contracts;
using Network.Contracts.Models.Adapters;
using Settings;
using Settings.Contracts;

namespace JobInTown
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);

            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterType<MainViewModel>().SingleInstance();
            cb.RegisterType<ItemDetailViewModel>().SingleInstance();
            cb.RegisterType<AddItemViewModel>().SingleInstance();
            cb.RegisterType<LoginViewModel>().SingleInstance();
            cb.RegisterType<RegisterViewModel>().SingleInstance();
            cb.RegisterType<ImageDetailViewModel>().SingleInstance();

            cb.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            cb.RegisterType<HttpClientAdapter>().As<IHttpClientAdapter>().SingleInstance();
            cb.RegisterType<FilePickerService>().As<IFilePickerService>().InstancePerDependency();
            cb.RegisterType<AzureStorageService>().As<IAzureStorageService>().SingleInstance();
            cb.Register(ctx => new NetworkService(new HttpClientAdapter())).As<INetworkService>();
            cb.RegisterType<ApiClient>().As<IApiClient>().SingleInstance();
            cb.Register(ctx => UserDialogs.Instance).As<IUserDialogs>();
            cb.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<GalleryItem, Job>();
                cfg.CreateMap<Job, GalleryItem>();
                cfg.CreateMap<Models.User, Azure.Client.Models.User>();
                cfg.CreateMap<Azure.Client.Models.User, Models.User>();
            });
        }
    }
}
