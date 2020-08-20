using Core.Attributes;

namespace JobInTown.Models.Enums
{
    public enum WorkdayType
    {
        [DisplayName("Enum_WorkdayType_Other")]
        Other,
        [DisplayName("Enum_WorkdayType_FullTime")]
        FullTime,
        [DisplayName("Enum_WorkdayType_Indifferent")]
        Indifferent,
        [DisplayName("Enum_WorkdayType_PartialIndifferent")]
        PartialIndifferent,
        [DisplayName("Enum_WorkdayType_PartialMorning")]
        PartialMorning,
        [DisplayName("Enum_WorkdayType_PartialAfternoon")]
        PartialAfternoon,
        [DisplayName("Enum_WorkdayType_PartialNight")]
        PartialNight,
        [DisplayName("Enum_WorkdayType_IntensiveIndifferent")]
        IntensiveIndifferent,
        [DisplayName("Enum_WorkdayType_IntensiveMorning")]
        IntensiveMorning,
        [DisplayName("Enum_WorkdayType_IntensiveAfternoon")]
        IntensiveAfternoon,
        [DisplayName("Enum_WorkdayType_IntensiveNight")]
        IntensiveNight
    }
}