namespace HumanLanguages.LanguageNames
{
    public sealed class EN
    {
        public static LanguageProperties LanguageProperties => new(
            LanguageNames:
            new Dictionary<LanguageIsoCode, string>()
                {
                    { LanguageIsoCode.en, "English" },
                    { LanguageIsoCode.da, "engelsk" },
                    { LanguageIsoCode.kok, "इंग्लीश" },
                    { LanguageIsoCode.@as, "ইংৰাজী" },
                    { LanguageIsoCode.ar, "الإنجليزية" },
                    { LanguageIsoCode.fil, "Ingles" },
                    { LanguageIsoCode.pl, "Angielski" },
                    { LanguageIsoCode.az, "İngilis" },
                    { LanguageIsoCode.vi, "Tiếng Anh" },
                    { LanguageIsoCode.ta, "ஆங்கிலம்" },
                    { LanguageIsoCode.ro, "Engleză" }
                },
            VariationNativeNames:
            new Dictionary<LanguageVariationIsoCode, string>()
            {
                { LanguageVariationIsoCode.US, "American English" },
                { LanguageVariationIsoCode.GB, "British English" }
            });
    }
}
