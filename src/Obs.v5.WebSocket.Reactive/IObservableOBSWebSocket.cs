using System.Reactive;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types.Events;

namespace Obs.v5.WebSocket.Reactive;

public interface IObservableOBSWebSocket : IOBSWebsocket
{
    /// <summary>The current program scene has changed.</summary>
    IObservable<ProgramSceneChangedEventArgs> OnCurrentProgramSceneChanged { get; }

    /// <summary>
    /// The list of scenes has changed.
    /// TODO: Make OBS fire this when scenes are reordered.
    /// </summary>
    IObservable<SceneListChangedEventArgs> OnSceneListChanged { get; }

    /// <summary>
    /// Triggered when the scene item list of the specified scene is reordered
    /// </summary>
    IObservable<SceneItemListReindexedEventArgs> OnSceneItemListReindexed { get; }

    /// <summary>
    /// Triggered when a new item is added to the item list of the specified scene
    /// </summary>
    IObservable<SceneItemCreatedEventArgs> OnSceneItemCreated { get; }

    /// <summary>
    /// Triggered when an item is removed from the item list of the specified scene
    /// </summary>
    IObservable<SceneItemRemovedEventArgs> OnSceneItemRemoved { get; }

    /// <summary>Triggered when the visibility of a scene item changes</summary>
    IObservable<SceneItemEnableStateChangedEventArgs> OnSceneItemEnableStateChanged { get; }

    /// <summary>
    /// Triggered when the lock status of a scene item changes
    /// </summary>
    IObservable<SceneItemLockStateChangedEventArgs> OnSceneItemLockStateChanged { get; }

    /// <summary>Triggered when switching to another scene collection</summary>
    IObservable<CurrentSceneCollectionChangedEventArgs> OnCurrentSceneCollectionChanged { get; }

    /// <summary>
    /// Triggered when a scene collection is created, deleted or renamed
    /// </summary>
    IObservable<SceneCollectionListChangedEventArgs> OnSceneCollectionListChanged { get; }

    /// <summary>Triggered when switching to another transition</summary>
    IObservable<CurrentSceneTransitionChangedEventArgs> OnCurrentSceneTransitionChanged { get; }

    /// <summary>
    /// Triggered when the current transition duration is changed
    /// </summary>
    IObservable<CurrentSceneTransitionDurationChangedEventArgs> OnCurrentSceneTransitionDurationChanged { get; }

    /// <summary>
    /// Triggered when a transition between two scenes starts. Followed by <see cref="E:OBSWebsocketDotNet.OBSWebsocket.CurrentProgramSceneChanged" />
    /// </summary>
    IObservable<SceneTransitionStartedEventArgs> OnSceneTransitionStarted { get; }

    /// <summary>
    /// Triggered when a transition (other than "cut") has ended. Please note that the from-scene field is not available in TransitionEnd
    /// </summary>
    IObservable<SceneTransitionEndedEventArgs> OnSceneTransitionEnded { get; }

    /// <summary>
    /// Triggered when a stinger transition has finished playing its video
    /// </summary>
    IObservable<SceneTransitionVideoEndedEventArgs> OnSceneTransitionVideoEnded { get; }

    /// <summary>Triggered when switching to another profile</summary>
    IObservable<CurrentProfileChangedEventArgs> OnCurrentProfileChanged { get; }

    /// <summary>
    /// Triggered when a profile is created, imported, removed or renamed
    /// </summary>
    IObservable<ProfileListChangedEventArgs> OnProfileListChanged { get; }

    /// <summary>Triggered when the streaming output state changes</summary>
    IObservable<StreamStateChangedEventArgs> OnStreamStateChanged { get; }

    /// <summary>Triggered when the recording output state changes</summary>
    IObservable<RecordStateChangedEventArgs> OnRecordStateChanged { get; }

    /// <summary>Triggered when state of the replay buffer changes</summary>
    IObservable<ReplayBufferStateChangedEventArgs> OnReplayBufferStateChanged { get; }

    /// <summary>
    /// Triggered when the preview scene selection changes (Studio Mode only)
    /// </summary>
    IObservable<CurrentPreviewSceneChangedEventArgs> OnCurrentPreviewSceneChanged { get; }

    /// <summary>Triggered when Studio Mode is turned on or off</summary>
    IObservable<StudioModeStateChangedEventArgs> OnStudioModeStateChanged { get; }

    /// <summary>Triggered when OBS exits</summary>
    IObservable<Unit> OnExitStarted { get; }

    /// <summary>
    /// Triggered when connected successfully to an obs-websocket server
    /// </summary>
    IObservable<Unit> OnConnected { get; }

    /// <summary>
    /// Triggered when disconnected from an obs-websocket server
    /// </summary>
    IObservable<ObsDisconnectionInfo> OnDisconnected { get; }

    /// <summary>A scene item is selected in the UI</summary>
    IObservable<SceneItemSelectedEventArgs> OnSceneItemSelected { get; }

    /// <summary>A scene item transform has changed</summary>
    IObservable<SceneItemTransformEventArgs> OnSceneItemTransformChanged { get; }

    /// <summary>The audio sync offset of an input has changed</summary>
    IObservable<InputAudioSyncOffsetChangedEventArgs> OnInputAudioSyncOffsetChanged { get; }

    /// <summary>A filter was added to a source</summary>
    IObservable<SourceFilterCreatedEventArgs> OnSourceFilterCreated { get; }

    /// <summary>A filter was removed from a source</summary>
    IObservable<SourceFilterRemovedEventArgs> OnSourceFilterRemoved { get; }

    /// <summary>Filters in a source have been reordered</summary>
    IObservable<SourceFilterListReindexedEventArgs> OnSourceFilterListReindexed { get; }

    /// <summary>Triggered when the visibility of a filter has changed</summary>
    IObservable<SourceFilterEnableStateChangedEventArgs> OnSourceFilterEnableStateChanged { get; }

    /// <summary>A source has been muted or unmuted</summary>
    IObservable<InputMuteStateChangedEventArgs> OnInputMuteStateChanged { get; }

    /// <summary>The volume of a source has changed</summary>
    IObservable<InputVolumeChangedEventArgs> OnInputVolumeChanged { get; }

    /// <summary>A custom broadcast message was received</summary>
    IObservable<VendorEventArgs> OnVendorEvent { get; }

    /// <summary>
    /// These events are emitted by the OBS sources themselves. For example when the media file ends. The behavior depends on the type of media source being used.
    /// </summary>
    IObservable<MediaInputPlaybackEndedEventArgs> OnMediaInputPlaybackEnded { get; }

    /// <summary>
    /// These events are emitted by the OBS sources themselves. For example when the media file starts playing. The behavior depends on the type of media source being used.
    /// </summary>
    IObservable<MediaInputPlaybackStartedEventArgs> OnMediaInputPlaybackStarted { get; }

    /// <summary>
    /// This is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
    /// </summary>
    IObservable<MediaInputActionTriggeredEventArgs> OnMediaInputActionTriggered { get; }

    /// <summary>The virtual cam state has changed.</summary>
    IObservable<VirtualcamStateChangedEventArgs> OnVirtualcamStateChanged { get; }

    /// <summary>The current scene collection has begun changing.</summary>
    IObservable<CurrentSceneCollectionChangingEventArgs> OnCurrentSceneCollectionChanging { get; }

    /// <summary>The current profile has begun changing.</summary>
    IObservable<CurrentProfileChangingEventArgs> OnCurrentProfileChanging { get; }

    /// <summary>The name of a source filter has changed.</summary>
    IObservable<SourceFilterNameChangedEventArgs> OnSourceFilterNameChanged { get; }

    /// <summary>An input has been created.</summary>
    IObservable<InputCreatedEventArgs> OnInputCreated { get; }

    /// <summary>An input has been removed.</summary>
    IObservable<InputRemovedEventArgs> OnInputRemoved { get; }

    /// <summary>The name of an input has changed.</summary>
    IObservable<InputNameChangedEventArgs> OnInputNameChanged { get; }

    /// <summary>
    /// An input's active state has changed.
    /// When an input is active, it means it's being shown by the program feed.
    /// </summary>
    IObservable<InputActiveStateChangedEventArgs> OnInputActiveStateChanged { get; }

    /// <summary>
    /// An input's show state has changed.
    /// When an input is showing, it means it's being shown by the preview or a dialog.
    /// </summary>
    IObservable<InputShowStateChangedEventArgs> OnInputShowStateChanged { get; }

    /// <summary>The audio balance value of an input has changed.</summary>
    IObservable<InputAudioBalanceChangedEventArgs> OnInputAudioBalanceChanged { get; }

    /// <summary>The audio tracks of an input have changed.</summary>
    IObservable<InputAudioTracksChangedEventArgs> OnInputAudioTracksChanged { get; }

    /// <summary>
    /// The monitor type of an input has changed.
    /// Available types are:
    /// - `OBS_MONITORING_TYPE_NONE`
    /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
    /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
    /// </summary>
    IObservable<InputAudioMonitorTypeChangedEventArgs> OnInputAudioMonitorTypeChanged { get; }

    /// <summary>
    /// A high-volume providing volume levels of all active inputs every 50 milliseconds.
    /// </summary>
    IObservable<InputVolumeMetersEventArgs> OnInputVolumeMeters { get; }

    /// <summary>The replay buffer has been saved.</summary>
    IObservable<ReplayBufferSavedEventArgs> OnReplayBufferSaved { get; }

    /// <summary>A new scene has been created.</summary>
    IObservable<SceneCreatedEventArgs> OnSceneCreated { get; }

    /// <summary>A scene has been removed.</summary>
    IObservable<SceneRemovedEventArgs> OnSceneRemoved { get; }

    /// <summary>The name of a scene has changed.</summary>
    IObservable<SceneNameChangedEventArgs> OnSceneNameChanged { get; }
}