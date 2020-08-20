using Autofac;
using Localization.Contracts;
using Localization.iOS;

namespace JobInTown.iOS
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