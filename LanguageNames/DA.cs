namespace HumanLanguages.LanguageNames
{
    public sealed class DA
    {
        public static LanguageProperties LanguageProperties => new(
            LanguageNames:
            new Dictionary<LanguageIsoCode, string>()
                {
                    { LanguageIsoCode.en, "Danish" },
                    { LanguageIsoCode.da, "dansk" },
                    { LanguageIsoCode.kok, "डॅनीश" },
                    { LanguageIsoCode.@as , "ডেনিছ" },
                    { LanguageIsoCode.ar, "الدانماركية" },
                    { LanguageIsoCode.fil, "Danish" },
                    { LanguageIsoCode.pl, "Duński" },
                    { LanguageIsoCode.az, "Danimarka" },
                    { LanguageIsoCode.vi, "Tiếng Đan Mạch" },
                    { LanguageIsoCode.ta, "டேனிஷ்" },
                    { LanguageIsoCode.ro, "danez" }
                },
            VariationNativeNames:
            new Dictionary<LanguageVariationIsoCode, string>()
            {
                { LanguageVariationIsoCode.DK, "danmark dansk" }
            });
    }
}
