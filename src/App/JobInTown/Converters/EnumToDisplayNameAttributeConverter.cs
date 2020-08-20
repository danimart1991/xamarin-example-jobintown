using System;
using System.Globalization;
using Core.Extensions;
using Localization.Contracts;
using Xamarin.Forms;

namespace JobInTown.Converters
{
    public class EnumToDisplayNameAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                var localizationService = AppContainer.Resolve<ILocalizationService>();
                if (localizationService != null)
                {
                    var enumDisplayValue = localizationService.GetString(enumValue.GetDisplayName());
                    return enumDisplayValue;
                }

                return string.Empty;
            }
            else
            {
#if DEBUG
                throw new ArgumentException("Value must be an Enumeration type");
#else
                return string.Empty;
#endif
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
