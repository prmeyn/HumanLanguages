namespace HumanLanguages
{
    public static class HumanHelper
    {
        /// <summary>
        /// Parses strings like "en", "en-US" or "en_US" (case-insensitive) into a <see cref="LanguageIsoCode"/>.
        /// Null, empty or unrecognized input falls back to <see cref="LanguageId.en"/> with
        /// <see cref="LanguageLocaleVariationCode.Default"/>. Use <see cref="TryCreateLanguageIsoCode"/>
        /// when you need to distinguish a genuine English request from invalid input.
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

        /// <summary>
        /// Strictly parses strings like "en", "en-US" or "en_US" (case-insensitive) into a <see cref="LanguageIsoCode"/>.
        /// Returns <c>true</c> only when the language part is a known <see cref="LanguageId"/> and, if a locale part is
        /// present, it is a known <see cref="LanguageLocaleVariationCode"/>. Otherwise returns <c>false</c> and
        /// <paramref name="result"/> is <c>null</c>. Unlike <see cref="CreateLanguageIsoCode"/>, this never falls back.
        /// </summary>
        public static bool TryCreateLanguageIsoCode(string languageIsoCodeString, out LanguageIsoCode? result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(languageIsoCodeString))
            {
                return false;
            }

            var parts = languageIsoCodeString.Split('-', '_');
            if (parts.Length > 2 || !TryParseName(parts[0], out LanguageId languageId))
            {
                return false;
            }

            var languageLocaleVariationCode = LanguageLocaleVariationCode.Default;
            if (parts.Length == 2 && !TryParseName(parts[1], out languageLocaleVariationCode))
            {
                return false;
            }

            result = new LanguageIsoCode(languageId, languageLocaleVariationCode);
            return true;
        }

        // Enum.TryParse also accepts numeric strings (e.g. "999") and yields undefined values; only accept member names.
        private static bool TryParseName<TEnum>(string value, out TEnum result) where TEnum : struct, Enum
        {
            if (value.Length == 0 || char.IsAsciiDigit(value[0]) || value[0] == '-' || value[0] == '+')
            {
                result = default;
                return false;
            }
            return Enum.TryParse(value, true, out result) && Enum.IsDefined(result);
        }
    }
}
