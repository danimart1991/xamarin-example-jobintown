using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace Localization.Models
{
    public abstract class Localize
    {
        protected const string DefaultLanguage = "en";

        public ResourceManager ResourceManager { get; set; }

        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = GetPlatformLanguage();

            CultureInfo cultureInfo = new CultureInfo(DefaultLanguage);
            try
            {
                cultureInfo = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                try
                {
                    var fallback = new PlatformCulture(netLanguage).LanguageCode;
                    cultureInfo = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
#if DEBUG
                    Debug.WriteLine("Culture Info not found");
#endif
                }
            }

            return cultureInfo;
        }

        public string GetString(string key)
        {
            return ResourceManager?.GetString(key);
        }

        protected virtual string GetPlatformLanguage()
        {
            return string.Empty;
        }
    }
}
