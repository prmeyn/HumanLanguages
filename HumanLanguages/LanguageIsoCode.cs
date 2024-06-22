namespace HumanLanguages
{
    public record class LanguageIsoCode(LanguageId LanguageId = LanguageId.en, LanguageLocaleVariationCode LanguageLocaleVariationCode = LanguageLocaleVariationCode.Default)
    {
		public string ToIsoCodeString() => LanguageLocaleVariationCode == LanguageLocaleVariationCode.Default ? $"{LanguageId}" : $"{LanguageId}-{LanguageLocaleVariationCode}";
	}
}
