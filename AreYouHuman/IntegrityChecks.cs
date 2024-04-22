using HumanLanguages;

namespace AreYouHuman
{
    [TestClass]
    public sealed class IntegrityChecks
    {
        [TestMethod]
        public void AllLangaugePropertiesExistInAllLangauges()
        {
            // Assert
            Assert.IsFalse(Enum.GetValues(typeof(LanguageId))
                                        .Cast<LanguageId>()
                                        .Any(code => Languages.LanguagePropertiesDictionary[code] == null));
        }

        [TestMethod]
        public void AllLangaugeNamesExistInAllLangauges()
        {
            // Arrange
            var allLangaugeProperties = Enum.GetValues(typeof(LanguageId))
                                            .Cast<LanguageId>()
                                            .Select(code => Languages.LanguagePropertiesDictionary[code])
                                            .ToList();

            // Assert
            var totalIsoCodes = Enum.GetValues(typeof(LanguageId)).Length;
            Assert.IsTrue(allLangaugeProperties.All(lp => lp.LanguageNames.Keys.Count == totalIsoCodes));
        }
    }
}