using Autofac;
using JobInTown.UWP.Services;
using Localization.Contracts;

namespace JobInTown.UWP
{
    public class Setup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder cb)
        {
            base.RegisterDependencies(cb);

            cb.RegisterType<LocalizationService>().As<ILocalizationService>().SingleInstance();
        }
    }
}