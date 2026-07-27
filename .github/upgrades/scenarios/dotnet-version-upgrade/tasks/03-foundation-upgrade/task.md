# 03-foundation-upgrade: Upgrade foundation projects to net10.0

Upgrade the Tier 1 projects — ChessTournament (net48 → net10.0, in-place) and FSharpTest (netcoreapp3.0 → net10.0) — including target framework change, package updates to net10.0-compatible versions, and resolution of any breaking-change compile errors. Enable nullable reference types (`<Nullable>enable</Nullable>`) on the C# project (ChessTournament) per the confirmed option, and remove any NuGet packages now provided by the framework reference (assessment rule NuGet.0003). Build and run tests after upgrade.

**Done when**: ChessTournament and FSharpTest target net10.0 and build successfully; nullable enabled on ChessTournament; redundant framework-provided packages removed; their tests pass.
