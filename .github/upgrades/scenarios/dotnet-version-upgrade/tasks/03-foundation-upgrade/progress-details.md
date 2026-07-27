# 03-foundation-upgrade — Progress Details

## Outcome: Completed

## What changed
- **ChessTournament.csproj**: Retargeted to `net10.0`. Enabled `Nullable`, `ImplicitUsings`, `LangVersion 14.0`. Removed legacy ClickOnce/bootstrapper properties, redundant framework references (`System.Web.Services`, `System.Data.DataSetExtensions`, `System.Net.Http`, `Microsoft.CSharp`), and the `System.ValueTuple` package (now in-box).
- **Deleted** `ChessTournament\Properties\AssemblyInfo.cs` — its manual assembly attributes collided with SDK auto-generated attributes (CS0579 duplicates). Removed `GenerateAssemblyInfo=false` so the SDK generates them.
- **FSharpTest.fsproj**: Retargeted from `netcoreapp3.0` to `net10.0`.

## Validation
- ChessTournament.csproj: **Build successful** on net10.0.
- FSharpTest.fsproj: **Build successful** on net10.0.

## Notes for downstream tasks
- ChessTournament.Test.csproj was already retargeted to net10.0 with modern NUnit/Test.Sdk packages — its build validation is Task 04.

## Files modified
- ChessTournament/ChessTournament.csproj
- FSharpTest/FSharpTest.fsproj
- ChessTournament/Properties/AssemblyInfo.cs (deleted)
