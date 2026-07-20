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

- `LanguageId` — enum of 240 lowercase ISO-style codes (`@as` and `@is` are keyword-escaped; `ku` is intentionally commented out pending data). Every member has an explicit numeric value.
- `LanguageLocaleVariationCode` — enum of region codes (`US`, `DK`), script codes (`Cyrl`, `Latn`), and `Default`. Every member has an explicit numeric value that is a fixed contract: never renumber a member or reuse a retired value (values 254-266 and 272 are retired — they belonged to the ALL-CAPS script duplicates removed in 11.0). Script codes are mixed-case; parsing is case-insensitive so `"sr-CYRL"` and `"sr-Cyrl"` both resolve to `Cyrl`.
- `LanguageIsoCode` — record of (LanguageId, LanguageLocaleVariationCode) with `ToIsoCodeString(separator)`.
- `HumanHelper.CreateLanguageIsoCode(string)` — parses `"da-DK"`/`"da_DK"` case-insensitively; null/empty/invalid input (including numeric strings) falls back to `en`/`Default`. `HumanHelper.TryCreateLanguageIsoCode(string, out LanguageIsoCode?)` is the strict, non-falling-back variant (returns `false`/`null` on any invalid part).
- `Languages.LanguagePropertiesDictionary` — the single entry point mapping every `LanguageId` to its `LanguageProperties` (exposed as `IReadOnlyDictionary`).

Data layer: `HumanLanguages/LanguageNames/*.cs` — one class per language, named by its uppercased code (`DA.cs` → class `DA`). Each exposes a cached static `LanguageProperties { get; } = new(...)` containing:
- `LanguageNames` — this language's name written in every language (key = the display language).
- `VariationNativeNames` — display names for its locale variations.

Each `LanguageProperties` exposes its dictionaries as `IReadOnlyDictionary` (immutable to consumers).

## Invariants (enforced by AreYouHuman/IntegrityChecks.cs)

- Every `LanguageId` has an entry in `Languages.LanguagePropertiesDictionary`.
- Every data file's `LanguageNames` contains exactly the full `LanguageId` key set.
- No language name is empty or whitespace.

Consequently, adding a language means: add the enum member (with the next explicit numeric value), create its data file with names in all 240 languages, add its key to every other data file's `LanguageNames`, and register it in `Languages.cs`.

## Data conventions

- Names are sourced from CLDR locale data in each language's native script; where no attested name exists, the fallback is the named language's own endonym (e.g. "Danish" in Mapuche is `"dansk"`).
- Data files are UTF-8 with heavy non-ASCII content and inconsistent tab/space indentation — preserve encoding and local formatting when editing; bulk edits are safest via byte-safe scripts that only touch ASCII structure.
- Enum numeric values are a fixed contract: never renumber an existing member or reuse a retired value. New members append with the next free value.
- Other public-API breaks (removing/renaming members or types, changing signatures) require a major version bump — the last such round was 11.0 (removed the ALL-CAPS script duplicates and the dead `IV` type, switched exposed dictionaries to `IReadOnlyDictionary`, added `TryCreateLanguageIsoCode`).
