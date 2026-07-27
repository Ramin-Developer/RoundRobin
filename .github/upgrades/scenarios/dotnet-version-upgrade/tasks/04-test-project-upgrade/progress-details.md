# 04-test-project-upgrade — Progress Details

## Outcome: Completed

## What changed
- **ChessTournament.Test.csproj**: Already retargeted to `net10.0` with nullable, implicit usings, LangVersion 14.0, and modern test packages (Microsoft.NET.Test.Sdk 17.11.1, NUnit 3.14.0, NUnit3TestAdapter 4.6.0). No further project changes needed.

## Validation
- **Build successful** on net10.0.
- Tests: **11 passed, 1 failed** (12 total).
  - Failing test `AdminTests.Should_generate_correct_rounds(4)` is **pre-existing and defective** — its helper `GetExpectedSolutions` never populates the `result` list (builds a local `aRound` and discards it), so `expected` is always empty while `actual` has 3 rounds. This is unrelated to the .NET upgrade.

## Deferred
- Fix or remove the incomplete `Should_generate_correct_rounds` test (see Reminders).

## Files modified
- (none — project already in target state; validation only)
