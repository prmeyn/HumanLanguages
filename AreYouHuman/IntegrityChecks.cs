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

            //// Act
            //bool allNamesExist = allLanguageNames.All(name => allLanguageNames.Contains(name));

            //// Assert
            //Assert.IsTrue(allNamesExist);
        }
    }
}