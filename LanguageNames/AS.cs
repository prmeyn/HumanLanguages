namespace HumanLanguages.LanguageNames
{
    public sealed class AS
    {
        public static LanguageProperties LanguageProperties => new(
            LanguageNames:
            new Dictionary<LanguageIsoCode, string>()
                {
                    { LanguageIsoCode.en, "Assamese" },
                    { LanguageIsoCode.da, "assamisk" },
                    { LanguageIsoCode.kok, "असमिया" },
                    { LanguageIsoCode.@as , "অসমীয়া" },
                    { LanguageIsoCode.ar, "الأسامية" },
                    { LanguageIsoCode.fil, "Assamese" },
                    { LanguageIsoCode.pl, "Asamski" },
                    { LanguageIsoCode.az, "Asam" },
                    { LanguageIsoCode.vi, "Assamese" },
                    { LanguageIsoCode.ta, "அசாமி" },
                    { LanguageIsoCode.ro, "assameză" }
                },
            VariationNativeNames:
            new Dictionary<LanguageVariationIsoCode, string>()
            {
                { LanguageVariationIsoCode.IN, "অসমীয়া" }
            });
    }
}
