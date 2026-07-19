# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
dotnet build                                          # build solution
dotnet test AreYouHuman/AreYouHuman.csproj            # run all tests (MSTest)
dotnet test AreYouHuman/AreYouHuman.csproj --filter "FullyQualifiedName~CheckDanishWithLocaleDash"   # single test
dotnet pack HumanLanguages/HumanLanguages.csproj -c Release -p:Version=X.Y.Z
```

In Git Bash, use `-p:Version=...` (the `/p:` form gets mangled by path conversion).

Releases: pushing a tag matching `v[0-9]+.[0-9]+.[0-9]+` triggers `.github/workflows/release.yml`, which builds, tests, packs and pushes to NuGet with the tag as version.

## Architecture

Two projects: `HumanLanguages` (the net10.0 NuGet library, no dependencies) and `AreYouHuman` (MSTest tests).

The library is a static, fully cross-translated database of language names: the name of each of 240 languages written in each of those 240 languages, plus per-language locale/script variations.

Core types (all in `HumanLanguages/`):

- `LanguageId` — enum of 240 lowercase ISO-style codes (`@as` and `@is` are keyword-escaped; `ku` is intentionally commented out pending data).
- `LanguageLocaleVariationCode` — enum of region codes (`US`, `DK`), script codes (`Cyrl`, `Latn`), and `Default`. The ALL-CAPS script members at the tail (`CYRL`, `LATN`, …) are legacy duplicates with different underlying values, kept only for backwards compatibility — case-insensitive parsing always resolves to the mixed-case member, and data dictionaries carry both keys. Prefer the mixed-case members.
- `LanguageIsoCode` — record of (LanguageId, LanguageLocaleVariationCode) with `ToIsoCodeString(separator)`.
- `HumanHelper.CreateLanguageIsoCode(string)` — parses `"da-DK"`/`"da_DK"` case-insensitively; null/empty/invalid input (including numeric strings) falls back to `en`/`Default`.
- `Languages.LanguagePropertiesDictionary` — the single entry point mapping every `LanguageId` to its `LanguageProperties`.

Data layer: `HumanLanguages/LanguageNames/*.cs` — one class per language, named by its uppercased code (`DA.cs` → class `DA`). Each exposes a cached static `LanguageProperties { get; } = new(...)` containing:
- `LanguageNames` — this language's name written in every language (key = the display language).
- `VariationNativeNames` — display names for its locale variations.

`IV.cs` is compiled but referenced nowhere (`iv` is not in the enum); it is kept because removing a public type would be a breaking change.

## Invariants (enforced by AreYouHuman/IntegrityChecks.cs)

- Every `LanguageId` has an entry in `Languages.LanguagePropertiesDictionary`.
- Every data file's `LanguageNames` contains exactly the full `LanguageId` key set.
- No language name is empty or whitespace.

Consequently, adding a language means: add the enum member, create its data file with names in all 240+1 languages, add its key to every other data file's `LanguageNames`, and register it in `Languages.cs`.

## Data conventions

- Names are sourced from CLDR locale data in each language's native script; where no attested name exists, the fallback is the named language's own endonym (e.g. "Danish" in Mapuche is `"dansk"`).
- Data files are UTF-8 with heavy non-ASCII content and inconsistent tab/space indentation — preserve encoding and local formatting when editing; bulk edits are safest via byte-safe scripts that only touch ASCII structure.
- The public API must stay backwards compatible: do not remove or renumber enum members, and do not remove public types (see the CYRL/IV notes above).
