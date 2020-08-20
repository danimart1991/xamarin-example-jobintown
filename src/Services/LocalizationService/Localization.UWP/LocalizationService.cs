using System.Globalization;
using Localization.Contracts;
using Localization.Models;
using Windows.Globalization;

namespace Localization.UWP
{
    public class LocalizationService : Localize, ILocalizationService
    {
        public void SetLocale(CultureInfo cultureInfo)
        {
            ApplicationLanguages.PrimaryLanguageOverride = cultureInfo.TwoLetterISOLanguageName;
        }

        protected override string GetPlatformLanguage()
        {
            return CultureInfo.CurrentUICulture.ToString();
        }
    }
}
