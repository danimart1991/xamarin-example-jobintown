using System.Globalization;
using System.Threading;
using Foundation;
using Localization.Contracts;
using Localization.Models;

namespace Localization.iOS
{
    public class LocalizationService : Localize, ILocalizationService
    {
        public void SetLocale(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        protected override string GetPlatformLanguage()
        {
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var preferredLanguage = NSLocale.PreferredLanguages[0];
                return preferredLanguage.ToString();
            }

            return string.Empty;
        }
    }
}
