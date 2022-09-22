set version=1.0.00
dotnet pack ../src/Obs.v4.WebSocket -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/Obs.v4.WebSocket.Reactive -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/Obs.v5.WebSocket.Reactive -c Release -o ../../_dist /p:version=%version%