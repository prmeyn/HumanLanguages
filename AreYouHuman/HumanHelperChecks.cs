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
			Assert.AreEqual(result, expected);
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
			Assert.AreEqual(result, expected);
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
    }
}
