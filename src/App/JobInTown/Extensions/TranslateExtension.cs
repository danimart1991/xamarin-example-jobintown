using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Localization.Contracts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobInTown.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "JobInTown.Resx.AppResources";

        private readonly CultureInfo _cultureInfo;

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == "iOS" || Device.RuntimePlatform == "Android")
            {
                var localizationService = AppContainer.Resolve<ILocalizationService>();
                _cultureInfo = localizationService?.GetCurrentCultureInfo();
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resourceManager.GetString(Text, _cultureInfo);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException($"Key '{Text}' was not found in resources '{ResourceId}' for culture '{_cultureInfo.Name}'.", "Text");
#else
				translation = Text;
#endif
            }

            return translation;
        }
    }
}
