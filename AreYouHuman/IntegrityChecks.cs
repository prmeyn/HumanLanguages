using HumanLanguages;

namespace AreYouHuman
{
    [TestClass]
    public sealed class IntegrityChecks
    {
        [TestMethod]
        public void AllLanguagePropertiesExistForAllLanguages()
        {
            foreach (var code in Enum.GetValues<LanguageId>())
            {
                Assert.IsTrue(
                    Languages.LanguagePropertiesDictionary.TryGetValue(code, out var properties) && properties != null,
                    $"Missing LanguageProperties for {code}");
            }
        }

        [TestMethod]
        public void AllLanguageNamesExistInAllLanguages()
        {
            var allIds = Enum.GetValues<LanguageId>().ToHashSet();

            foreach (var code in Enum.GetValues<LanguageId>())
            {
                var keys = Languages.LanguagePropertiesDictionary[code].LanguageNames.Keys.ToHashSet();
                Assert.IsTrue(keys.SetEquals(allIds),
                    $"{code}: missing [{string.Join(", ", allIds.Except(keys))}], unexpected [{string.Join(", ", keys.Except(allIds))}]");
            }
        }

        [TestMethod]
        public void NoLanguageNameIsEmpty()
        {
            var offenders = Enum.GetValues<LanguageId>()
                                .SelectMany(code => Languages.LanguagePropertiesDictionary[code].LanguageNames
                                    .Where(kvp => string.IsNullOrWhiteSpace(kvp.Value))
                                    .Select(kvp => $"{code} in {kvp.Key}"))
                                .ToList();

            Assert.AreEqual(0, offenders.Count, $"Empty language names: {string.Join(", ", offenders.Take(20))}");
        }
    }
}
