using System;
using System.Globalization;
using JobInTown.Models.Enums;
using Xamarin.Forms;

namespace JobInTown.Converters
{
    public class ContractToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum contract)
            {
                switch (contract)
                {
                    case ContractType.Indefinite:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("7E735F");
                            case Device.iOS:
                                return Color.FromHex("7E735F");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("7E735F");
                        }

                    case ContractType.FixedTerm:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("00B7C3");
                            case Device.iOS:
                                return Color.FromHex("00B7C3");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("00B7C3");
                        }

                    case ContractType.PartTime:
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                return Color.FromHex("C6C6C6");
                            case Device.iOS:
                                return Color.FromHex("C6C6CB");
                            case Device.WinPhone:
                            case Device.Windows:
                            default:
                                return Color.FromHex("C6C6C6");
                        }

                    case ContractType.Freelance:
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

                    case ContractType.Formative:
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

                    case ContractType.FixedDiscontinuous:
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

                    case ContractType.Relief:
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

                    case ContractType.Other:
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

            return ContractType.Other;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}