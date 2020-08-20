using Core.Attributes;

namespace JobInTown.Models.Enums
{
    public enum ContractType
    {
        [DisplayName("Enum_ContractType_Other")]
        Other,
        [DisplayName("Enum_ContractType_Indefinite")]
        Indefinite,
        [DisplayName("Enum_ContractType_FixedTerm")]
        FixedTerm,
        [DisplayName("Enum_ContractType_PartTime")]
        PartTime,
        [DisplayName("Enum_ContractType_Freelance")]
        Freelance,
        [DisplayName("Enum_ContractType_Formative")]
        Formative,
        [DisplayName("Enum_ContractType_FixedDiscontinuous")]
        FixedDiscontinuous,
        [DisplayName("Enum_ContractType_Relief")]
        Relief
    }
}
