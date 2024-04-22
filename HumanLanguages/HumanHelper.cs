namespace HumanLanguages
{
    public static  class HumanHelper
    {
        public static LanguageIsoCode CreateLanguageIsoCode(string languageIsoCodeString)
        {
            if (string.IsNullOrEmpty(languageIsoCodeString))
            {
                return new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default);
            }

            var parts = languageIsoCodeString.Split('-');
            var languageId = Enum.TryParse(typeof(LanguageId), parts[0], true, out var langIdResult)
                ? (LanguageId)langIdResult
                : LanguageId.en;

            var languageLocaleVariationCode = parts.Length > 1 && Enum.TryParse(typeof(LanguageLocaleVariationCode), parts[1], true, out var localeResult)
                ? (LanguageLocaleVariationCode)localeResult
                : LanguageLocaleVariationCode.Default;

            return new LanguageIsoCode(languageId, languageLocaleVariationCode);
        }

    }
}
