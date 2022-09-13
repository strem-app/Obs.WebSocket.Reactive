using System.Reactive;
using System.Reactive.Linq;

namespace Obs.v5.WebSocket.Reactive.Extensions;

public static class IObservableExtensions
{
    public static IObservable<Unit> ToUnit<T>(this IObservable<T> observable)
    { return observable.Select(x => Unit.Default); }
}