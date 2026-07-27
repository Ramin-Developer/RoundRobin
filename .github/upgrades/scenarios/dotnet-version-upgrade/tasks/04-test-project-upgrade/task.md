# 04-test-project-upgrade: Upgrade test project to net10.0

Upgrade the Tier 2 project ChessTournament.Test (net48 → net10.0, in-place) to match the project it tests. Update test packages to net10.0-compatible versions, address the binding/entry-point issue flagged by the assessment (Binding.0003), enable nullable reference types, and fix any breaking-change compile errors. Build and run the full test suite.

**Done when**: ChessTournament.Test targets net10.0 and builds; nullable enabled; all tests pass against the upgraded ChessTournament.
