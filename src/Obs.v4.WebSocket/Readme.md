# Obs.Websockets.v4

This is basically a custom build of [https://github.com/BarRaider/obs-websocket-dotnet](https://github.com/BarRaider/obs-websocket-dotnet) for v4 of OBS which includes the fork from [https://github.com/Zingabopp/obs-websocket-dotnet/tree/dev](https://github.com/Zingabopp/obs-websocket-dotnet/tree/dev) to more easily make use of the .net 6 usage.

This is only bundled here because the Reactive wrapper depends on this specific version as the base version does not have normal `EventHandler<T>` delegates and is not `Task` based