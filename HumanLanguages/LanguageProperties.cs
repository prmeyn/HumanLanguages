namespace HumanLanguages
{
    public sealed record LanguageProperties(IReadOnlyDictionary<LanguageId, string> LanguageNames, IReadOnlyDictionary<LanguageLocaleVariationCode, string> VariationNativeNames);
}
