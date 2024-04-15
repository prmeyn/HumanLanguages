namespace HumanLanguages
{
    public sealed record LanguageProperties(Dictionary<LanguageIsoCode, string> LanguageNames, Dictionary<LanguageVariationIsoCode, string> VariationNativeNames);
}
