# Upgrade Options — ChessTournament

Assessment: 3 projects (2× net48 C#, 1× netcoreapp3.0 F#) → net10.0. Two legacy csproj need SDK-style conversion; crosses .NET Framework → modern boundary.

## Strategy

### Upgrade Strategy
.NET Framework projects with dependencies detected (ChessTournament ← ChessTournament.Test) — the Framework→modern boundary requires tier-by-tier upgrade mechanics, so Bottom-Up is fixed.

| Value | Description |
|-------|-------------|
| **Bottom-Up** (selected) | Upgrade leaf/foundation projects first, then dependents tier by tier, validating each tier. Fixed for multi-project .NET Framework solutions — not configurable. |

## Project Structure

### Project Approach
The two net48 projects (a console/app and its test library) have no external Framework consumers and all migrate together, so in-place replacement of the target framework is cleanest — no multi-targeting overhead.

| Value | Description |
|-------|-------------|
| **In-place** (selected) | Replace each project's target framework directly with net10.0. Clean; requires all consumers to migrate together (they are). |
| Multi-targeting | Add net10.0 alongside net48 so libraries serve both old and new consumers during a transition. Not needed here. |

### Package Management
Crosses the .NET Framework → modern boundary and the two legacy csproj must first convert to SDK-style/PackageReference — adding CPM now would create churn, so defer it.

| Value | Description |
|-------|-------------|
| **Per-Project (defer CPM to post-migration)** (selected) | Each project keeps its own package versions during migration; CPM is added as a final cleanup recommendation once all projects are SDK-style on a single TFM. |
| Central Package Management (CPM) | Create `Directory.Packages.props` now and centralize versions. Adds friction during a Framework migration. |

## Modernization

### Nullable Reference Types
Small C# codebase (2 projects) and you explicitly want modern C# 14 from day one, so enabling nullable reference types fits.

| Value | Description |
|-------|-------------|
| **Enable Nullable Reference Types** (selected) | Adds `<Nullable>enable</Nullable>` to C# projects for compile-time null safety. May require code updates to resolve warnings. |
| Leave Disabled | Keep existing null handling; enable separately later. |
