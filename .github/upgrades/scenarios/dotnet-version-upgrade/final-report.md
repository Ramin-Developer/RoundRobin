# Modernization Final Report — ChessTournamentSetup

**Branch:** `upgrade-dotnet-10`  •  **Target:** .NET 10 / C# 14  •  **Solution:** `ChessTournament.slnx`

## Summary
Upgraded the entire solution to .NET 10 with C# 14, converted `.sln` → `.slnx`, adopted
Central Package Management, modernized C# syntax across the codebase, codified style rules,
added repository-level build conventions, and expanded automated test coverage.

## Changes by Area

### Platform & Language
- All projects target `net10.0`; C# projects use `LangVersion 14.0`.
- Nullable reference types and implicit usings enabled solution-wide.
- Converted `ChessTournament.sln` → `ChessTournament.slnx`.

### Repository Conventions (new)
- `global.json` — pins SDK to `10.0.302` (`rollForward: latestFeature`).
- `Directory.Build.props` — centralizes shared build settings; C#-only `LangVersion` guarded so the F# project is unaffected.
- `Directory.Packages.props` — CPM enabled with `CentralPackageTransitivePinningEnabled`.
- `.editorconfig` — locks in file-scoped namespaces, collection expressions, access-modifier ordering, nullable; vendored `Combinatorics` code marked generated to suppress analyzer noise.
- `.github/workflows/ci.yml` — CI: restore, format verification (C# projects), Release build, and test.

### C# Modernization
- Per-project `GlobalUsings.cs`; all `using` directives centralized.
- File-scoped namespaces throughout.
- Collection expressions, primary constructors where applicable.
- Access modifiers ordered public → protected → private.

### Code Quality
- `Utility.InitializePlayers` returns `HashSet<Player>`, removing a null-forgiving cast in `ProblemDesc`.
- Test project folder/namespace corrected `ChesTournament.Test` → `ChessTournament.Test`.
- `InternalsVisibleTo` added to expose internals to the test assembly.

### Testing
- Expanded from **12 → 30 tests**: added `ProblemDescTests`, `ModelTests` (`UtilityTests`, `RoundTests`).

## Validation
- `dotnet build ChessTournament.slnx`: **0 Warnings, 0 Errors**.
- `dotnet test`: **30 passed, 0 failed**.
- `dotnet format` (C# projects): clean.

## Post-upgrade CI fix
- **Symptom:** `CI / build-and-test` failed in ~16s on `ubuntu-latest` while all steps passed locally on Windows.
- **Root cause:** `.gitattributes` used `* text=auto`, so `.cs` files checked out as CRLF on Windows but LF on Linux. The solution `.editorconfig` enforces `end_of_line = crlf` for `[*.cs]`, so `dotnet format --verify-no-changes` failed only on the Linux runner.
- **Fix:** Added `*.cs text eol=crlf` to `.gitattributes` so C# files check out with CRLF on all platforms, matching the enforced convention.

## Deferred / Optional Follow-ups
- Consider evaluating the CA analyzer hints surfaced at `--severity info` (CA1822, CA1859) as a future cleanup.
