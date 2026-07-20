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

        [TestMethod]
        public void NoLegacyUppercaseScriptMembersRemain()
        {
            string[] removed = ["DEVA", "ARAB", "CYRL", "LATN", "TFNG", "OLCK", "JAVA", "MONG", "BENG", "GURU", "VAII", "HANT", "HANS", "ADLM"];
            var present = Enum.GetNames<LanguageLocaleVariationCode>().Intersect(removed).ToList();
            Assert.AreEqual(0, present.Count, $"Legacy ALL-CAPS script members must stay removed: {string.Join(", ", present)}");
        }

        [TestMethod]
        public void EnumNumericValuesAreStable()
        {
            // Locks the explicit-value contract: these must never change (persisted data depends on them).
            // Values live in a dictionary so the cast below is a runtime read, not a constant-folded compare.
            var variations = new Dictionary<LanguageLocaleVariationCode, int>
            {
                [LanguageLocaleVariationCode.Default] = 0,
                [LanguageLocaleVariationCode.Cyrl] = 36,
                [LanguageLocaleVariationCode.Latn] = 37,
                [LanguageLocaleVariationCode.ME] = 267,
                [LanguageLocaleVariationCode.AZ] = 271,
            };
            foreach (var (member, expected) in variations)
            {
                Assert.AreEqual(expected, (int)member, $"{member} value changed");
            }

            var languages = new Dictionary<LanguageId, int>
            {
                [LanguageId.aa] = 0,
                [LanguageId.@as] = 7,
                [LanguageId.@is] = 83,
                [LanguageId.zu] = 239,
            };
            foreach (var (member, expected) in languages)
            {
                Assert.AreEqual(expected, (int)member, $"{member} value changed");
            }
        }
    }
}
