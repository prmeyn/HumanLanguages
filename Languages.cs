using HumanLanguages.LanguageNames;

namespace HumanLanguages
{
    public static class Languages
    {
        public static Dictionary<LanguageIsoCode, LanguageProperties> LanguagePropertiesDictionary = new()
        {
            {
                LanguageIsoCode.en,
                EN.LanguageProperties
            },
            {
                LanguageIsoCode.da,
                DA.LanguageProperties
            },
            {
                LanguageIsoCode.@as,
                AS.LanguageProperties
            }
        };
    }
}
