using OBSWebsocketDotNet;

namespace Obs.v5.WebSocket.Reactive;

public class ObservableOBSWebSocket : OBSWebsocket, IObservableOBSWebSocket
{
    public ObservableOBSWebSocket()
    { SetupObservables(); }

    public void SetupObservables()
    {
        
    }
}