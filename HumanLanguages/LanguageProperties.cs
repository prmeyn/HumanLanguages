namespace HumanLanguages
{
    public sealed record LanguageProperties(Dictionary<LanguageId, string> LanguageNames, Dictionary<LanguageLocaleVariationCode, string> VariationNativeNames);
}
