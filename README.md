# [HumanLanguages](https://www.nuget.org/packages/HumanLanguages)

**HumanLanguages** is an open-source C# class library that provides a comprehensive database of human language names. It knows the name of every supported language *in* every supported language — 240 languages, fully cross-translated — plus locale/script variations and helpers for parsing ISO-style codes like `en-US` or `da_DK`.

## Features

- **Language names**: the name of any language, written in any other language.
- **Native names (endonyms)**: every language's own name for itself.
- **Locale variations**: per-language region and script variations (e.g. `en-GB`, `sr-Cyrl`) with native display names.
- **Parsing**: turn strings like `"da-DK"` or `"da_DK"` into strongly typed codes.
- **No dependencies**: plain data and enums, nothing else.

## Getting Started

Install the [NuGet package](https://www.nuget.org/packages/HumanLanguages):

```bash
dotnet add package HumanLanguages
```

## Usage

### Parse a language code

```csharp
using HumanLanguages;

var isoCode = HumanHelper.CreateLanguageIsoCode("da-DK"); // '-' and '_' both work, case-insensitive

Console.WriteLine(isoCode.LanguageId);                  // da
Console.WriteLine(isoCode.LanguageLocaleVariationCode); // DK
Console.WriteLine(isoCode.ToIsoCodeString());           // "da-DK"
Console.WriteLine(isoCode.ToIsoCodeString('_'));        // "da_DK"
```

Null, empty or unrecognized input falls back to English (`en`, `Default`).

### Get a language's name in another language

```csharp
var danish = Languages.LanguagePropertiesDictionary[LanguageId.da];

Console.WriteLine(danish.LanguageNames[LanguageId.en]); // "Danish"
Console.WriteLine(danish.LanguageNames[LanguageId.fr]); // "danois"
Console.WriteLine(danish.LanguageNames[LanguageId.ja]); // "デンマーク語"
Console.WriteLine(danish.LanguageNames[LanguageId.da]); // "dansk" (its own name)
```

### List all languages with their native names

```csharp
foreach (var languageId in Enum.GetValues<LanguageId>())
{
    var properties = Languages.LanguagePropertiesDictionary[languageId];
    Console.WriteLine($"{languageId}: {properties.LanguageNames[languageId]}");
}
// aa: Afar
// af: Afrikaans
// ...
// zu: isiZulu
```

### Locale and script variations

```csharp
var english = Languages.LanguagePropertiesDictionary[LanguageId.en];
Console.WriteLine(english.VariationNativeNames[LanguageLocaleVariationCode.GB]); // "United Kingdom"

var serbian = HumanHelper.CreateLanguageIsoCode("sr-Cyrl");
var serbianProperties = Languages.LanguagePropertiesDictionary[serbian.LanguageId];
Console.WriteLine(serbianProperties.VariationNativeNames[serbian.LanguageLocaleVariationCode]); // "ћирилица (Cyrillic)"
```

## Contributing

We welcome contributions! If you find a bug, have an idea for improvement, or want to add support for additional languages, please submit an issue or a pull request on GitHub: https://github.com/prmeyn/HumanLanguages

## License

This project is licensed under the GNU GENERAL PUBLIC LICENSE.

Happy coding! 🚀🌐📚
