using HumanLanguages;

namespace AreYouHuman
{
    [TestClass]
    public sealed  class HumanHelperChecks
    {
		[TestMethod]
		public void CheckCustomSeparator()
		{
			// Arrange
			var input = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.US);
			var expected = "en_US";

            // Act
            var result = input.ToIsoCodeString('_');

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void CheckDashSeparator()
		{
			// Arrange
			var input = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.US);
			var expected = "en-US";

			// Act
			var result = input.ToIsoCodeString();

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
        public void CheckEnglishWithoutLocale()
        {
            // Arrange
            var input = "en";
            var expected = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default);

            // Act
            var result = HumanHelper.CreateLanguageIsoCode(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckEnglishWithLocale()
        {
            // Arrange
            var input = "en-US";
            var expected = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.US);

            // Act
            var result = HumanHelper.CreateLanguageIsoCode(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckDanishWithoutLocale()
        {
            // Arrange
            var input = "da";
            var expected = new LanguageIsoCode(LanguageId.da, LanguageLocaleVariationCode.Default);

            // Act
            var result = HumanHelper.CreateLanguageIsoCode(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckDanishWithLocaleDash()
        {
            // Arrange
            var input = "da-DK";
            var expected = new LanguageIsoCode(LanguageId.da, LanguageLocaleVariationCode.DK);

            // Act
            var result = HumanHelper.CreateLanguageIsoCode(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

		[TestMethod]
		public void CheckDanishWithLocaleUnderscore()
		{
			// Arrange
			var input = "da_DK";
			var expected = new LanguageIsoCode(LanguageId.da, LanguageLocaleVariationCode.DK);

			// Act
			var result = HumanHelper.CreateLanguageIsoCode(input);

			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
        public void CheckNullOrEmptyString()
        {
            // Arrange
            var input = string.Empty;
            var expected = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default);

            // Act
            var result = HumanHelper.CreateLanguageIsoCode(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckInvalidString()
        {
            // Arrange
            var input = "invalid";
            var expected = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default);

            // Act
            var result = HumanHelper.CreateLanguageIsoCode(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CheckNumericStringFallsBackInsteadOfUndefinedEnumValue()
        {
            // Arrange
            var expected = new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default);

            // Act & Assert
            Assert.AreEqual(expected, HumanHelper.CreateLanguageIsoCode("999"));
            Assert.AreEqual(expected, HumanHelper.CreateLanguageIsoCode("42-17"));
        }

        [TestMethod]
        public void CheckScriptVariationParsesToCanonicalMemberWithNativeName()
        {
            // Act
            var result = HumanHelper.CreateLanguageIsoCode("sr-Cyrl");

            // Assert
            Assert.AreEqual(new LanguageIsoCode(LanguageId.sr, LanguageLocaleVariationCode.Cyrl), result);
            Assert.IsTrue(Languages.LanguagePropertiesDictionary[result.LanguageId].VariationNativeNames
                .ContainsKey(result.LanguageLocaleVariationCode));
            Assert.AreEqual("sr-Cyrl", result.ToIsoCodeString());
        }

        [TestMethod]
        public void CheckUppercaseScriptCodeResolvesToCanonicalMember()
        {
            // After 11.0 removed the ALL-CAPS script members, any casing resolves to the mixed-case member.
            var result = HumanHelper.CreateLanguageIsoCode("sr-CYRL");

            Assert.AreEqual(new LanguageIsoCode(LanguageId.sr, LanguageLocaleVariationCode.Cyrl), result);
            Assert.IsTrue(Languages.LanguagePropertiesDictionary[result.LanguageId].VariationNativeNames
                .ContainsKey(result.LanguageLocaleVariationCode));
        }

        [TestMethod]
        public void TryCreateReturnsTrueForValidInput()
        {
            Assert.IsTrue(HumanHelper.TryCreateLanguageIsoCode("da-DK", out var result));
            Assert.AreEqual(new LanguageIsoCode(LanguageId.da, LanguageLocaleVariationCode.DK), result);

            Assert.IsTrue(HumanHelper.TryCreateLanguageIsoCode("en", out var noLocale));
            Assert.AreEqual(new LanguageIsoCode(LanguageId.en, LanguageLocaleVariationCode.Default), noLocale);
        }

        [TestMethod]
        public void TryCreateReturnsFalseForInvalidInput()
        {
            Assert.IsFalse(HumanHelper.TryCreateLanguageIsoCode("zzz", out var badLang));
            Assert.IsNull(badLang);

            Assert.IsFalse(HumanHelper.TryCreateLanguageIsoCode("en-ZZZ", out var badLocale));
            Assert.IsNull(badLocale);

            Assert.IsFalse(HumanHelper.TryCreateLanguageIsoCode("999", out var numeric));
            Assert.IsNull(numeric);

            Assert.IsFalse(HumanHelper.TryCreateLanguageIsoCode("", out var empty));
            Assert.IsNull(empty);
        }
    }
}
