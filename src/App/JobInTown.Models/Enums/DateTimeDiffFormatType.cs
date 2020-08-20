using Core.Attributes;

namespace JobInTown.Models.Enums
{
    public enum DateTimeDiffFormatType
    {
        [DisplayName("Enum_DateTimeDiffFormatType_Years")]
        Years,
        [DisplayName("Enum_DateTimeDiffFormatType_Year")]
        Year,
        [DisplayName("Enum_DateTimeDiffFormatType_Months")]
        Months,
        [DisplayName("Enum_DateTimeDiffFormatType_Month")]
        Month,
        [DisplayName("Enum_DateTimeDiffFormatType_Days")]
        Days,
        [DisplayName("Enum_DateTimeDiffFormatType_Day")]
        Day,
        [DisplayName("Enum_DateTimeDiffFormatType_Hours")]
        Hours,
        [DisplayName("Enum_DateTimeDiffFormatType_Hour")]
        Hour,
        [DisplayName("Enum_DateTimeDiffFormatType_Minutes")]
        Minutes,
        [DisplayName("Enum_DateTimeDiffFormatType_Minute")]
        Minute,
        [DisplayName("Enum_DateTimeDiffFormatType_Seconds")]
        Seconds,
        [DisplayName("Enum_DateTimeDiffFormatType_Second")]
        Second
    }
}
