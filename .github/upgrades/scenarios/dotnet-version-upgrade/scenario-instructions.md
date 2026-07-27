# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (all projects)
- **C# Language Version**: C# 14 — adopt latest language features across the whole solution
- **Solution Format**: Convert `.sln` to `.slnx`

## Source Control
- **Source Branch**: BeginTesting
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: Bottom-Up

### Project Structure
- Project Approach: In-place
- Package Management: Per-Project (defer CPM to post-migration)

### Modernization
- Nullable Reference Types: Enable

## Strategy
**Selected**: Bottom-Up
**Rationale**: .NET Framework projects (net48) with a dependency chain (ChessTournament ← ChessTournament.Test) crossing the Framework→modern boundary; tier-by-tier upgrade mechanics differ per layer. Fixed for multi-project Framework solutions.

### Execution Constraints
- Strict tier ordering: foundation project (ChessTournament) first, then independent FSharpTest, then dependent ChessTournament.Test.
- SDK-style conversion of legacy csproj precedes the TFM change for those projects.
- Validate build after each project's upgrade before proceeding to the next tier.
- Defer CPM: add a final cleanup recommendation once all projects are SDK-style on net10.0.
- Cross-cutting tasks (C# 14 modernization, .sln → .slnx) run after all projects build on net10.0.

## Reminders & Deferred Items
- 2025-06-14 — Pre-existing defective test `AdminTests.Should_generate_correct_rounds(4)`: helper `GetExpectedSolutions` never populates `result`, so it always fails. Unrelated to the upgrade. Fix or remove during final validation.
