using System;
using System.Globalization;
using Core.Extensions;
using JobInTown.Helpers;
using Localization.Contracts;
using Xamarin.Forms;

namespace JobInTown.Converters
{
    public class DateToPostedDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;

            if (value is DateTime dateTime)
            {
                var localizationService = AppContainer.Resolve<ILocalizationService>();
                if (localizationService != null)
                {
                    var postedDateText = localizationService.GetString("Job_PostedDate_Text");
                    if (!string.IsNullOrEmpty(postedDateText))
                    {
                        var dateTimeDiff = DateTimeDiffHelper.GetTimeDiffFromUtcNow(dateTime);

                        if (dateTimeDiff != null)
                        {
                            var dateTimeDiffFormat = localizationService.GetString($"{dateTimeDiff.Format.GetDisplayName()}");

                            var timeFromPosted = $"{dateTimeDiff?.Number.ToString()} {dateTimeDiffFormat}";

                            result = string.Format(postedDateText, dateTime.ToLocalTime(), timeFromPosted);
                        }
                    }
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
