using HumanLanguages;

namespace AreYouHuman
{
    [TestClass]
    public class IntegrityChecks
    {
        [TestMethod]
        public void AllLangaugePropertiesExistInAllLangauges()
        {
            // Assert
            Assert.IsFalse(Enum.GetValues(typeof(LanguageIsoCode))
                                        .Cast<LanguageIsoCode>()
                                        .Any(code => Languages.LanguagePropertiesDictionary[code] == null));
        }

        [TestMethod]
        public void AllLangaugeNamesExistInAllLangauges()
        {
            // Arrange
            var allLangaugeProperties = Enum.GetValues(typeof(LanguageIsoCode))
                                            .Cast<LanguageIsoCode>()
                                            .Select(code => Languages.LanguagePropertiesDictionary[code])
                                            .ToList();

            // Assert
            var totalIsoCodes = Enum.GetValues(typeof(LanguageIsoCode)).Length;
            Assert.IsTrue(allLangaugeProperties.All(lp => lp.LanguageNames.Keys.Count == totalIsoCodes));
        }
    }
}