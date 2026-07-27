# .NET 10 Upgrade Plan

## Overview

**Target**: Upgrade all projects in ChessTournament.sln to net10.0, adopt C# 14 latest language features, and convert the solution to `.slnx` format.
**Scope**: 3 projects — ChessTournament (net48, C# app), ChessTournament.Test (net48, C# test library), FSharpTest (netcoreapp3.0, F#). Small solution.

### Selected Strategy
**Bottom-Up (Dependency-First)** — Upgrade from leaf/foundation projects to dependents, tier by tier.
**Rationale**: 3 projects with a 2-tier dependency graph crossing the .NET Framework → modern boundary; Bottom-Up is non-negotiable for multi-project .NET Framework solutions.

### Dependency graph
```
Tier 2: [ChessTournament.Test]
			 ↓
Tier 1: [ChessTournament]   [FSharpTest]
```
- **Tier 1 (foundation)**: ChessTournament (net48), FSharpTest (netcoreapp3.0) — no internal project references.
- **Tier 2**: ChessTournament.Test (net48) — references ChessTournament.

## Tasks

### 01-prerequisites: Verify SDK and toolchain readiness

Confirm the .NET 10 SDK is installed and that any `global.json` in the repository is compatible with net10.0. Verify the solution loads and establish a clean baseline build of the current state before any changes.

**Done when**: .NET 10 SDK validated as installed; `global.json` (if present) is compatible or updated; current solution state is confirmed loadable.

### 02-sdk-style-conversion: Convert legacy csproj to SDK-style

Convert the two legacy (non-SDK-style) projects — ChessTournament and ChessTournament.Test — to SDK-style project format while **keeping them on their current net48 TFM**. This is a structural change only; any `packages.config` is migrated to `PackageReference` as part of the conversion. FSharpTest is already SDK-style and is out of scope here.

**Done when**: ChessTournament and ChessTournament.Test use SDK-style project format on net48; any packages.config is converted to PackageReference; solution still builds on the current frameworks.

### 03-foundation-upgrade: Upgrade foundation projects to net10.0

Upgrade the Tier 1 projects — ChessTournament (net48 → net10.0, in-place) and FSharpTest (netcoreapp3.0 → net10.0) — including target framework change, package updates to net10.0-compatible versions, and resolution of any breaking-change compile errors. Enable nullable reference types (`<Nullable>enable</Nullable>`) on the C# project (ChessTournament) per the confirmed option, and remove any NuGet packages now provided by the framework reference (assessment rule NuGet.0003). Build and run tests after upgrade.

**Done when**: ChessTournament and FSharpTest target net10.0 and build successfully; nullable enabled on ChessTournament; redundant framework-provided packages removed; their tests pass.

### 04-test-project-upgrade: Upgrade test project to net10.0

Upgrade the Tier 2 project ChessTournament.Test (net48 → net10.0, in-place) to match the project it tests. Update test packages to net10.0-compatible versions, address the binding/entry-point issue flagged by the assessment (Binding.0003), enable nullable reference types, and fix any breaking-change compile errors. Build and run the full test suite.

**Done when**: ChessTournament.Test targets net10.0 and builds; nullable enabled; all tests pass against the upgraded ChessTournament.

### 05-csharp14-modernization: Adopt C# 14 language features

Set the C# language version to the latest (C# 14) across the C# projects and modernize the codebase to use newer C# language features where they improve clarity and correctness (e.g., collection expressions, pattern matching, primary constructors, file-scoped namespaces, and other applicable C# 14 features). Apply only safe, behavior-preserving modernizations. F# project is unaffected.

**Done when**: C# projects compile with C# 14 selected; applicable modern language features adopted without behavior changes; solution builds and all tests pass.

### 06-slnx-conversion: Convert solution to .slnx

Convert `ChessTournament.sln` to the XML-based `.slnx` solution format, preserving all project references and configurations. Verify the new solution file loads and builds.

**Done when**: A valid `ChessTournament.slnx` exists with all projects referenced; the solution builds via the `.slnx` file; the legacy `.sln` is handled per user preference.

### 07-final-validation: Full solution validation and cleanup

Perform a full solution build and run the entire test suite to confirm the upgrade is complete and green. Document the deferred Central Package Management recommendation — all projects are now SDK-style on a single TFM, so CPM can be added cleanly without VersionOverride friction.

**Done when**: Full solution builds with zero errors and zero warnings; all tests pass; deferred CPM recommendation documented.
