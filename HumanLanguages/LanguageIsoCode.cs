namespace HumanLanguages
{
    public record class LanguageIsoCode(LanguageId LanguageId = LanguageId.en, LanguageLocaleVariationCode LanguageLocaleVariationCode = LanguageLocaleVariationCode.Default)
    {
		public string ToIsoCodeString(char separator = '-') => LanguageLocaleVariationCode == LanguageLocaleVariationCode.Default ? $"{LanguageId}" : $"{LanguageId}{separator}{LanguageLocaleVariationCode}";
	}
}
