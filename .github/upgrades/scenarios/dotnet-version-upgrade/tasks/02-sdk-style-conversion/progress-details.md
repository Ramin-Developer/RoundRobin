# 02-sdk-style-conversion — Progress Details

## Outcome: Completed

## What changed
- **ChessTournament.csproj**: Converted to SDK-style (`<Project Sdk="Microsoft.NET.Sdk">`), still targeting net48. `System.ValueTuple` migrated from packages.config to `PackageReference`.
- **ChessTournament.Test.csproj**: Converted to SDK-style, still net48.
- **Removed** orphaned `ChessTournament\packages.config` and `ChesTournament.Test\packages.config` (packages already migrated to PackageReference).
- FSharpTest.fsproj: untouched (already SDK-style).

## Validation
- ChessTournament.Test build succeeds (transitively builds ChessTournament) on net48.
- No remaining packages.config files in the solution.

## Notes for downstream tasks
- ChessTournament.csproj still carries legacy ClickOnce/Bootstrapper properties and `LangVersion=6` (Debug) — to be cleaned up during the net10.0 upgrade (task 03) and C# 14 modernization (task 05).

## Files modified
- ChessTournament/ChessTournament.csproj
- ChesTournament.Test/ChessTournament.Test.csproj
- ChessTournament/packages.config (deleted)
- ChesTournament.Test/packages.config (deleted)
