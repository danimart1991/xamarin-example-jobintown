using System.Globalization;
using System.Resources;

namespace Localization.Contracts
{
    public interface ILocalizationService
    {
        ResourceManager ResourceManager { get; set; }

        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo cultureInfo);

        string GetString(string key);
    }
}
