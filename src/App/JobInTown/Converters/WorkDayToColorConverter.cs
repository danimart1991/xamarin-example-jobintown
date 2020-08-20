using System;
using System.Globalization;
using JobInTown.Models.Enums;
using Xamarin.Forms;

namespace JobInTown.Converters
{
    public class WorkDayToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum workday)
            {
                switch (workday)
                {
                    case WorkdayType.FullTime:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("33B5E5");
                            case Device.iOS:
                                return Color.FromHex("34A9DA");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("0078D7");
                        }

                    case WorkdayType.Indifferent:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("AA66CC");
                            case Device.iOS:
                                return Color.FromHex("B9529E");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("BF0077");
                        }

                    case WorkdayType.PartialIndifferent:
                    case WorkdayType.PartialMorning:
                    case WorkdayType.PartialAfternoon:
                    case WorkdayType.PartialNight:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("99CC00");
                            case Device.iOS:
                                return Color.FromHex("69BD45");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("10893E");
                        }

                    case WorkdayType.IntensiveIndifferent:
                    case WorkdayType.IntensiveMorning:
                    case WorkdayType.IntensiveAfternoon:
                    case WorkdayType.IntensiveNight:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("FFBB33");
                            case Device.iOS:
                                return Color.FromHex("FFCC04");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("FFB900");
                        }

                    case WorkdayType.Other:
                    default:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("FF4444");
                            case Device.iOS:
                                return Color.FromHex("EE3658");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("D13438");
                        }
                }
            }

            return WorkdayType.Other;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}