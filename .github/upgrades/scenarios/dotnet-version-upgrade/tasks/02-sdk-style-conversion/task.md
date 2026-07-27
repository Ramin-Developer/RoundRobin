# 02-sdk-style-conversion: Convert legacy csproj to SDK-style

Convert the two legacy (non-SDK-style) projects — ChessTournament and ChessTournament.Test — to SDK-style project format while **keeping them on their current net48 TFM**. This is a structural change only; any `packages.config` is migrated to `PackageReference` as part of the conversion. FSharpTest is already SDK-style and is out of scope here.

**Done when**: ChessTournament and ChessTournament.Test use SDK-style project format on net48; any packages.config is converted to PackageReference; solution still builds on the current frameworks.
