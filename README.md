# RoundRobin — Chess Tournament Setup

A .NET console application that generates **round-robin tournament schedules** for chess (and similar 1-vs-1 competitions). Given a number of players, it computes all possible matches and simulates a fair schedule where each player meets every other player, distributing matches across rounds.

## Features

- Generates a complete round-robin schedule for an even number of players.
- Validates input constraints (players must be an even number in the range **4–30**).
- Calculates key tournament metrics:
  - Maximum number of rounds
  - Matches per round
  - Total possible matches
- Uses a combinatorics library (combinations, permutations, variations) to build match sets.
- Outputs a readable summary to the console and to a results text file.

## Projects

| Project | Description |
| --- | --- |
| `ChessTournament` | Core console application and domain model (players, matches, rounds, scheduling). |
| `ChessTournament.Test` | Unit tests for the tournament logic. |
| `FSharpTest` | Experimental F# test project. |

## Getting Started

### Prerequisites

- Visual Studio 2022 or later (solution developed in Visual Studio 2026)
- .NET Framework 4.8 / .NET Core 3.0

### Build & Run

```powershell
git clone https://github.com/Ramin-Developer/RoundRobin.git
cd RoundRobin
dotnet build ChessTournament.sln
dotnet run --project ChessTournament
```

When prompted, enter the number of players (an even number between 4 and 30). The application prints the generated round-robin schedule and writes the results to `Results - {N} Players.txt`.

## How It Works

1. The user supplies the number of players.
2. `ProblemDesc` validates input and computes tournament parameters.
3. `Admin.Simulate()` builds the schedule by pairing players across rounds so each player faces every other exactly once.
4. Results are summarized to the screen and saved to a file.

## License

This project is provided as-is for educational and personal use.
