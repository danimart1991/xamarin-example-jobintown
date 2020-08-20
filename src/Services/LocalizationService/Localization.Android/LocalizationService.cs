using System.Globalization;
using System.Threading;
using Localization.Contracts;
using Localization.Models;

namespace Localization.Android
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
            var locale = Java.Util.Locale.Default;
            return locale.ToString().Replace("_", "-");
        }
    }
}