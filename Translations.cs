namespace HumanLanguages
{
    public static class Translations
    {
        public static Dictionary<TranslationKey, Dictionary<LanguageIsoCode, Dictionary<LanguageVariationIsoCode, string>>> TranslationsDictionary = new()
        {
            {
                TranslationKey.SortLanguagesPrompt,
                new Dictionary<LanguageIsoCode, Dictionary<LanguageVariationIsoCode, string>>()
                {
                    {
                        LanguageIsoCode.en,
                        new Dictionary<LanguageVariationIsoCode, string>()
                        {
                            { LanguageVariationIsoCode.Default, "Sort and add the languages you can read, in order of preference:" }
                        }
                    },
                    {
                        LanguageIsoCode.da,
                        new Dictionary<LanguageVariationIsoCode, string>()
                        {
                            { LanguageVariationIsoCode.Default, "Sorter og tilføj de sprog, du kan læse, i præferencerækkefølge:" }
                        }
                    }
                }
            },
            {
                TranslationKey.ChoosePreferredLanguagesPrompt,
                new Dictionary<LanguageIsoCode, Dictionary<LanguageVariationIsoCode, string>>()
                {
                    {
                        LanguageIsoCode.en,
                        new Dictionary<LanguageVariationIsoCode, string>()
                        {
                            { LanguageVariationIsoCode.Default, "Add more?" }
                        }
                    },
                    {
                        LanguageIsoCode.da,
                        new Dictionary<LanguageVariationIsoCode, string>()
                        {
                            { LanguageVariationIsoCode.Default, "Tilføj mere?" }
                        }
                    }
                }
            }
        };
    }
}
