# Roadmap & Backlog — ChessTournamentSetup

Tracking observations and follow-up work identified during the .NET 10 / C# 14 modernization
(branch `upgrade-dotnet-10`). Items here are deferred, not blockers for merge.

## Analyzer Hints (surfaced by `dotnet format --severity info`)
- **CA1822** — `Player.Compare` does not access instance data; can be marked `static`.
- **CA1859** — `Round.SetupRound` returns `IEnumerable<Match>`; change to `List<Match>` for performance.
- **CA1859** — `Admin.SetupRounds` returns `IEnumerable<Round>`; change to `List<Round>` for performance.

## Tooling / CI
- `dotnet format` cannot process the F# project (`FSharpTest.fsproj`) at solution scope, so the
  CI format step is scoped to the two C# projects. Revisit if F# formatting tooling matures.
- Consider adding code coverage collection (e.g., `--collect:"XPlat Code Coverage"`) to the CI test step.
- Consider raising analyzer severity gradually (e.g., enable `AnalysisMode` in `Directory.Build.props`)
  once the CA hints above are addressed.

## Testing
- Test count expanded 12 → 30. Consider adding coverage for edge cases in
  `Utility.FindAllMatchesFor` and `DisplayRemainigLists` (note: method name misspelling — see below).

## Naming / Cleanup
- Typo in public API: `Utility.DisplayRemainigLists` → `DisplayRemainingLists`. Rename is a breaking
  change to the internal surface; batch with other internal cleanups.
- Verify the `Combinatorics` folder is truly vendored/generated; if it is first-party, remove the
  `generated_code=true` suppression in `.editorconfig` and address analyzer findings.

## Package Management
- CPM is enabled with transitive pinning. Audit transitive versions after the next dependency bump
  to confirm pins remain intentional.

## IDE Hygiene
- After the `.sln` → `.slnx` conversion and the `ChesTournament.Test` → `ChessTournament.Test`
  folder rename, reload the solution from `ChessTournament.slnx` to clear any stale in-memory
  project references (old `.sln` no longer exists on disk).
