using System.Reactive;
using Newtonsoft.Json.Linq;

namespace Obs.v4.WebSocket.Reactive;

public interface IObservableOBSWebSocket : IOBSWebSocket
{
    /// <summary>
    /// Exceptions thrown that are not passed up to the caller will be passed through this event.
    /// </summary>
    IObservable<OBSErrorEventArgs> OnOBSError { get; }

    /// <summary>
    /// Raised when a request is sent.
    /// </summary>
    IObservable<RequestData> OnRequestSent { get; }

    /// <summary>
    /// Raised when any "update-type" event is received.
    /// </summary>
    IObservable<JObject> OnEventReceived { get; }

    /// <summary>
    /// Raised when a request's response is received.
    /// </summary>
    IObservable<JObject> OnResponseReceived { get; }

    /// <summary>
    /// Triggered when switching to another scene
    /// </summary>
    IObservable<SceneChangeEventArgs> OnSceneChanged { get; }

    /// <summary>
    /// Triggered when a scene is created, deleted or renamed
    /// </summary>
    IObservable<Unit> OnSceneListChanged { get; }

    /// <summary>
    /// Scene items within a scene have been reordered.
    /// </summary>
    IObservable<SourceOrderChangedEventArgs> OnSourceOrderChanged { get; }

    /// <summary>
    /// Triggered when a new item is added to the item list of the specified scene
    /// </summary>
    IObservable<SceneItemUpdatedEventArgs> OnSceneItemAdded { get; }

    /// <summary>
    /// Triggered when an item is removed from the item list of the specified scene
    /// </summary>
    IObservable<SceneItemUpdatedEventArgs> OnSceneItemRemoved { get; }

    /// <summary>
    /// Triggered when the visibility of a scene item changes
    /// </summary>
    IObservable<SceneItemVisibilityChangedEventArgs> OnSceneItemVisibilityChanged { get; }

    /// <summary>
    /// Triggered when the lock status of a scene item changes
    /// </summary>
    IObservable<SceneItemLockChangedEventArgs> OnSceneItemLockChanged { get; }

    /// <summary>
    /// Triggered when switching to another scene collection
    /// </summary>
    IObservable<Unit> OnSceneCollectionChanged { get; }

    /// <summary>
    /// Triggered when a scene collection is created, deleted or renamed
    /// </summary>
    IObservable<Unit> OnSceneCollectionListChanged { get; }

    /// <summary>
    /// Triggered when switching to another transition
    /// </summary>
    IObservable<TransitionChangeEventArgs> OnTransitionChanged { get; }

    /// <summary>
    /// Triggered when the current transition duration is changed
    /// </summary>
    IObservable<TransitionDurationChangeEventArgs> OnTransitionDurationChanged { get; }

    /// <summary>
    /// Triggered when a transition is created or removed
    /// </summary>
    IObservable<Unit> OnTransitionListChanged { get; }

    /// <summary>
    /// Triggered when a transition between two scenes starts. Followed by <see cref="SceneChanged"/>
    /// </summary>
    IObservable<TransitionBeginEventArgs> OnTransitionBegin { get; }

    /// <summary>
    /// A transition (other than "cut") has ended. Added in v4.8.0
    /// </summary>
    IObservable<TransitionEndEventArgs> OnTransitionEnd { get; }

    /// <summary>
    /// A stinger transition has finished playing its video. Added in v4.8.0
    /// </summary>
    IObservable<TransitionVideoEndEventArgs> OnTransitionVideoEnd { get; }

    /// <summary>
    /// Triggered when switching to another profile
    /// </summary>
    IObservable<Unit> OnProfileChanged { get; }

    /// <summary>
    /// Triggered when a profile is created, imported, removed or renamed
    /// </summary>
    IObservable<Unit> OnProfileListChanged { get; }

    /// <summary>
    /// Triggered when the streaming output state changes
    /// </summary>
    IObservable<OutputStateChangedEventArgs> OnStreamingStateChanged { get; }

    /// <summary>
    /// Triggered when the recording output state changes
    /// </summary>
    IObservable<OutputStateChangedEventArgs> OnRecordingStateChanged { get; }

    /// <summary>
    /// Triggered when state of the replay buffer changes
    /// </summary>
    IObservable<OutputStateChangedEventArgs> OnReplayBufferStateChanged { get; }

    /// <summary>
    /// Triggered every 2 seconds while streaming is active
    /// </summary>
    IObservable<StreamStatusEventArgs> OnStreamStatus { get; }

    /// <summary>
    /// Triggered when the preview scene selection changes (Studio Mode only)
    /// </summary>
    IObservable<SceneChangeEventArgs> OnPreviewSceneChanged { get; }

    /// <summary>
    /// Triggered when Studio Mode is turned on or off
    /// </summary>
    IObservable<StudioModeChangeEventArgs> OnStudioModeSwitched { get; }

    /// <summary>
    /// Triggered when OBS exits
    /// </summary>
    IObservable<Unit> OnOBSExit { get; }

    /// <summary>
    /// Triggered when connected successfully to an obs-websocket server
    /// </summary>
    IObservable<Unit> OnConnected { get; }

    /// <summary>
    /// Triggered when disconnected from an obs-websocket server
    /// </summary>
    IObservable<Unit> OnDisconnected { get; }

    /// <summary>
    /// Emitted every 2 seconds after enabling it by calling SetHeartbeat
    /// </summary>
    IObservable<HeartBeatEventArgs> OnHeartbeat { get; }

    /// <summary>
    /// A scene item is deselected
    /// </summary>
    IObservable<SceneItemSelectionEventArgs> OnSceneItemDeselected { get; }

    /// <summary>
    /// A scene item is selected
    /// </summary>
    IObservable<SceneItemSelectionEventArgs> OnSceneItemSelected { get; }

    /// <summary>
    /// A scene item transform has changed
    /// </summary>
    IObservable<SceneItemTransformEventArgs> OnSceneItemTransformChanged { get; }

    /// <summary>
    /// Audio mixer routing changed on a source
    /// </summary>
    IObservable<SourceAudioMixersChangedEventArgs> OnSourceAudioMixersChanged { get; }

    /// <summary>
    /// The audio sync offset of a source has changed
    /// </summary>
    IObservable<SourceAudioSyncOffsetEventArgs> OnSourceAudioSyncOffsetChanged { get; }

    /// <summary>
    /// A source has been created. A source can be an input, a scene or a transition.
    /// </summary>
    IObservable<SourceCreatedEventArgs> OnSourceCreated { get; }

    /// <summary>
    /// A source has been destroyed/removed. A source can be an input, a scene or a transition.
    /// </summary>
    IObservable<SourceDestroyedEventArgs> OnSourceDestroyed { get; }

    /// <summary>
    /// A filter was added to a source
    /// </summary>
    IObservable<SourceFilterAddedEventArgs> OnSourceFilterAdded { get; }

    /// <summary>
    /// A filter was removed from a source
    /// </summary>
    IObservable<SourceFilterRemovedEventArgs> OnSourceFilterRemoved { get; }

    /// <summary>
    /// Filters in a source have been reordered
    /// </summary>
    IObservable<SourceFiltersReorderedEventArgs> OnSourceFiltersReordered { get; }

    /// <summary>
    /// Triggered when the visibility of a filter has changed
    /// </summary>
    IObservable<SourceFilterVisibilityChangedEventArgs> OnSourceFilterVisibilityChanged { get; }

    /// <summary>
    /// A source has been muted or unmuted
    /// </summary>
    IObservable<SourceMuteStateChangedEventArgs> OnSourceMuteStateChanged { get; }

    /// <summary>
    /// A source has been renamed
    /// </summary>
    IObservable<SourceRenamedEventArgs> OnSourceRenamed { get; }

    /// <summary>
    /// The volume of a source has changed
    /// </summary>
    IObservable<SourceVolumeChangedEventArgs> OnSourceVolumeChanged { get; }

    /// <summary>
    /// An event was received that Obs.Websockets.v4 does not have a defined event handler for.
    /// </summary>
    IObservable<JObject> OnUnhandledEvent { get; }

    /// <summary>
    /// A custom broadcast message was received
    /// </summary>
    IObservable<BroadcastCustomMessageReceivedEventArgs> OnBroadcastCustomMessageReceived { get; }
}