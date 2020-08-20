using Autofac;
using JobInTown.Droid.Services;
using Localization.Contracts;

namespace JobInTown.Droid
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