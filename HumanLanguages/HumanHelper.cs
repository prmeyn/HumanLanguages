namespace HumanLanguages
{
    public static class HumanHelper
    {
        /// <summary>
        /// Parses strings like "en", "en-US" or "en_US" (case-insensitive) into a <see cref="LanguageIsoCode"/>.
        /// Null, empty or unrecognized input falls back to <see cref="LanguageId.en"/> with
        /// <see cref="LanguageLocaleVariationCode.Default"/>.
        /// </summary>
        public static LanguageIsoCode CreateLanguageIsoCode(string languageIsoCodeString)
        {
            if (string.IsNullOrWhiteSpace(languageIsoCodeString))
            {
                return new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default);
            }

            var parts = languageIsoCodeString.Split('-', '_');
            var languageId = TryParseName(parts[0], out LanguageId langIdResult)
                ? langIdResult
                : LanguageId.en;

            var languageLocaleVariationCode = parts.Length > 1 && TryParseName(parts[1], out LanguageLocaleVariationCode localeResult)
                ? localeResult
                : LanguageLocaleVariationCode.Default;

            return new LanguageIsoCode(languageId, languageLocaleVariationCode);
        }

        // Enum.TryParse also accepts numeric strings (e.g. "999") and yields undefined values; only accept member names.
        private static bool TryParseName<TEnum>(string value, out TEnum result) where TEnum : struct, Enum
        {
            if (char.IsAsciiDigit(value[0]) || value[0] == '-' || value[0] == '+')
            {
                result = default;
                return false;
            }
            return Enum.TryParse(value, true, out result);
        }
    }
}
