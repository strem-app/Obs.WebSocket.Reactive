using System.Reactive;
using System.Reactive.Linq;
using Obs.v5.WebSocket.Reactive.Extensions;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types.Events;

namespace Obs.v5.WebSocket.Reactive;

public class ObservableOBSWebSocket : OBSWebsocket, IObservableOBSWebSocket
{
    /// <summary>The current program scene has changed.</summary>
    public IObservable<ProgramSceneChangedEventArgs> OnCurrentProgramSceneChanged { get; private set; }

    /// <summary>
    /// The list of scenes has changed.
    /// TODO: Make OBS fire this when scenes are reordered.
    /// </summary>
    public IObservable<SceneListChangedEventArgs> OnSceneListChanged { get; private set; }

    /// <summary>
    /// Triggered when the scene item list of the specified scene is reordered
    /// </summary>
    public IObservable<SceneItemListReindexedEventArgs> OnSceneItemListReindexed { get; private set; }

    /// <summary>
    /// Triggered when a new item is added to the item list of the specified scene
    /// </summary>
    public IObservable<SceneItemCreatedEventArgs> OnSceneItemCreated { get; private set; }

    /// <summary>
    /// Triggered when an item is removed from the item list of the specified scene
    /// </summary>
    public IObservable<SceneItemRemovedEventArgs> OnSceneItemRemoved { get; private set; }

    /// <summary>Triggered when the visibility of a scene item changes</summary>
    public IObservable<SceneItemEnableStateChangedEventArgs> OnSceneItemEnableStateChanged { get; private set; }

    /// <summary>
    /// Triggered when the lock status of a scene item changes
    /// </summary>
    public IObservable<SceneItemLockStateChangedEventArgs> OnSceneItemLockStateChanged { get; private set; }

    /// <summary>Triggered when switching to another scene collection</summary>
    public IObservable<CurrentSceneCollectionChangedEventArgs> OnCurrentSceneCollectionChanged { get; private set; }

    /// <summary>
    /// Triggered when a scene collection is created, deleted or renamed
    /// </summary>
    public IObservable<SceneCollectionListChangedEventArgs> OnSceneCollectionListChanged { get; private set; }

    /// <summary>Triggered when switching to another transition</summary>
    public IObservable<CurrentSceneTransitionChangedEventArgs> OnCurrentSceneTransitionChanged { get; private set; }

    /// <summary>
    /// Triggered when the current transition duration is changed
    /// </summary>
    public IObservable<CurrentSceneTransitionDurationChangedEventArgs> OnCurrentSceneTransitionDurationChanged { get; private set; }

    /// <summary>
    /// Triggered when a transition between two scenes starts. Followed by <see cref="E:OBSWebsocketDotNet.OBSWebsocket.CurrentProgramSceneChanged" />
    /// </summary>
    public IObservable<SceneTransitionStartedEventArgs> OnSceneTransitionStarted { get; private set; }

    /// <summary>
    /// Triggered when a transition (other than "cut") has ended. Please note that the from-scene field is not available in TransitionEnd
    /// </summary>
    public IObservable<SceneTransitionEndedEventArgs> OnSceneTransitionEnded { get; private set; }

    /// <summary>
    /// Triggered when a stinger transition has finished playing its video
    /// </summary>
    public IObservable<SceneTransitionVideoEndedEventArgs> OnSceneTransitionVideoEnded { get; private set; }

    /// <summary>Triggered when switching to another profile</summary>
    public IObservable<CurrentProfileChangedEventArgs> OnCurrentProfileChanged { get; private set; }

    /// <summary>
    /// Triggered when a profile is created, imported, removed or renamed
    /// </summary>
    public IObservable<ProfileListChangedEventArgs> OnProfileListChanged { get; private set; }

    /// <summary>Triggered when the streaming output state changes</summary>
    public IObservable<StreamStateChangedEventArgs> OnStreamStateChanged { get; private set; }

    /// <summary>Triggered when the recording output state changes</summary>
    public IObservable<RecordStateChangedEventArgs> OnRecordStateChanged { get; private set; }

    /// <summary>Triggered when state of the replay buffer changes</summary>
    public IObservable<ReplayBufferStateChangedEventArgs> OnReplayBufferStateChanged { get; private set; }

    /// <summary>
    /// Triggered when the preview scene selection changes (Studio Mode only)
    /// </summary>
    public IObservable<CurrentPreviewSceneChangedEventArgs> OnCurrentPreviewSceneChanged { get; private set; }

    /// <summary>Triggered when Studio Mode is turned on or off</summary>
    public IObservable<StudioModeStateChangedEventArgs> OnStudioModeStateChanged { get; private set; }

    /// <summary>Triggered when OBS exits</summary>
    public IObservable<Unit> OnExitStarted { get; private set; }

    /// <summary>
    /// Triggered when connected successfully to an obs-websocket server
    /// </summary>
    public IObservable<Unit> OnConnected { get; private set; }

    /// <summary>
    /// Triggered when disconnected from an obs-websocket server
    /// </summary>
    public IObservable<ObsDisconnectionInfo> OnDisconnected { get; private set; }

    /// <summary>A scene item is selected in the UI</summary>
    public IObservable<SceneItemSelectedEventArgs> OnSceneItemSelected { get; private set; }

    /// <summary>A scene item transform has changed</summary>
    public IObservable<SceneItemTransformEventArgs> OnSceneItemTransformChanged { get; private set; }

    /// <summary>The audio sync offset of an input has changed</summary>
    public IObservable<InputAudioSyncOffsetChangedEventArgs> OnInputAudioSyncOffsetChanged { get; private set; }

    /// <summary>A filter was added to a source</summary>
    public IObservable<SourceFilterCreatedEventArgs> OnSourceFilterCreated { get; private set; }

    /// <summary>A filter was removed from a source</summary>
    public IObservable<SourceFilterRemovedEventArgs> OnSourceFilterRemoved { get; private set; }

    /// <summary>Filters in a source have been reordered</summary>
    public IObservable<SourceFilterListReindexedEventArgs> OnSourceFilterListReindexed { get; private set; }

    /// <summary>Triggered when the visibility of a filter has changed</summary>
    public IObservable<SourceFilterEnableStateChangedEventArgs> OnSourceFilterEnableStateChanged { get; private set; }

    /// <summary>A source has been muted or unmuted</summary>
    public IObservable<InputMuteStateChangedEventArgs> OnInputMuteStateChanged { get; private set; }

    /// <summary>The volume of a source has changed</summary>
    public IObservable<InputVolumeChangedEventArgs> OnInputVolumeChanged { get; private set; }

    /// <summary>A custom broadcast message was received</summary>
    public IObservable<VendorEventArgs> OnVendorEvent { get; private set; }

    /// <summary>
    /// These events are emitted by the OBS sources themselves. For example when the media file ends. The behavior depends on the type of media source being used.
    /// </summary>
    public IObservable<MediaInputPlaybackEndedEventArgs> OnMediaInputPlaybackEnded { get; private set; }

    /// <summary>
    /// These events are emitted by the OBS sources themselves. For example when the media file starts playing. The behavior depends on the type of media source being used.
    /// </summary>
    public IObservable<MediaInputPlaybackStartedEventArgs> OnMediaInputPlaybackStarted { get; private set; }

    /// <summary>
    /// This is only emitted when something actively controls the media/VLC source. In other words, the source will never emit this on its own naturally.
    /// </summary>
    public IObservable<MediaInputActionTriggeredEventArgs> OnMediaInputActionTriggered { get; private set; }

    /// <summary>The virtual cam state has changed.</summary>
    public IObservable<VirtualcamStateChangedEventArgs> OnVirtualcamStateChanged { get; private set; }

    /// <summary>The current scene collection has begun changing.</summary>
    public IObservable<CurrentSceneCollectionChangingEventArgs> OnCurrentSceneCollectionChanging { get; private set; }

    /// <summary>The current profile has begun changing.</summary>
    public IObservable<CurrentProfileChangingEventArgs> OnCurrentProfileChanging { get; private set; }

    /// <summary>The name of a source filter has changed.</summary>
    public IObservable<SourceFilterNameChangedEventArgs> OnSourceFilterNameChanged { get; private set; }

    /// <summary>An input has been created.</summary>
    public IObservable<InputCreatedEventArgs> OnInputCreated { get; private set; }

    /// <summary>An input has been removed.</summary>
    public IObservable<InputRemovedEventArgs> OnInputRemoved { get; private set; }

    /// <summary>The name of an input has changed.</summary>
    public IObservable<InputNameChangedEventArgs> OnInputNameChanged { get; private set; }

    /// <summary>
    /// An input's active state has changed.
    /// When an input is active, it means it's being shown by the program feed.
    /// </summary>
    public IObservable<InputActiveStateChangedEventArgs> OnInputActiveStateChanged { get; private set; }

    /// <summary>
    /// An input's show state has changed.
    /// When an input is showing, it means it's being shown by the preview or a dialog.
    /// </summary>
    public IObservable<InputShowStateChangedEventArgs> OnInputShowStateChanged { get; private set; }

    /// <summary>The audio balance value of an input has changed.</summary>
    public IObservable<InputAudioBalanceChangedEventArgs> OnInputAudioBalanceChanged { get; private set; }

    /// <summary>The audio tracks of an input have changed.</summary>
    public IObservable<InputAudioTracksChangedEventArgs> OnInputAudioTracksChanged { get; private set; }

    /// <summary>
    /// The monitor type of an input has changed.
    /// Available types are:
    /// - `OBS_MONITORING_TYPE_NONE`
    /// - `OBS_MONITORING_TYPE_MONITOR_ONLY`
    /// - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`
    /// </summary>
    public IObservable<InputAudioMonitorTypeChangedEventArgs> OnInputAudioMonitorTypeChanged { get; private set; }

    /// <summary>
    /// A high-volume providing volume levels of all active inputs every 50 milliseconds.
    /// </summary>
    public IObservable<InputVolumeMetersEventArgs> OnInputVolumeMeters { get; private set; }

    /// <summary>The replay buffer has been saved.</summary>
    public IObservable<ReplayBufferSavedEventArgs> OnReplayBufferSaved { get; private set; }

    /// <summary>A new scene has been created.</summary>
    public IObservable<SceneCreatedEventArgs> OnSceneCreated { get; private set; }

    /// <summary>A scene has been removed.</summary>
    public IObservable<SceneRemovedEventArgs> OnSceneRemoved { get; private set; }

    /// <summary>The name of a scene has changed.</summary>
    public IObservable<SceneNameChangedEventArgs> OnSceneNameChanged { get; private set; }
    
    public ObservableOBSWebSocket()
    { 
        OnCurrentProgramSceneChanged = Observable.FromEventPattern<ProgramSceneChangedEventArgs>(
                x => CurrentProgramSceneChanged += x,
                x => CurrentProgramSceneChanged -= x)
            .Select(x => x.EventArgs);
        
        OnSceneListChanged = Observable.FromEventPattern<SceneListChangedEventArgs>(
                x => SceneListChanged += x,
                x => SceneListChanged -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemListReindexed = Observable.FromEventPattern<SceneItemListReindexedEventArgs>(
                x => SceneItemListReindexed += x,
                x => SceneItemListReindexed -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemCreated = Observable.FromEventPattern<SceneItemCreatedEventArgs>(
                x => SceneItemCreated += x,
                x => SceneItemCreated -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemRemoved = Observable.FromEventPattern<SceneItemRemovedEventArgs>(
                x => SceneItemRemoved += x,
                x => SceneItemRemoved -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemEnableStateChanged = Observable.FromEventPattern<SceneItemEnableStateChangedEventArgs>(
                x => SceneItemEnableStateChanged += x,
                x => SceneItemEnableStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemLockStateChanged = Observable.FromEventPattern<SceneItemLockStateChangedEventArgs>(
                x => SceneItemLockStateChanged += x,
                x => SceneItemLockStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentProgramSceneChanged = Observable.FromEventPattern<ProgramSceneChangedEventArgs>(
                x => CurrentProgramSceneChanged += x,
                x => CurrentProgramSceneChanged -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentSceneCollectionChanged = Observable.FromEventPattern<CurrentSceneCollectionChangedEventArgs>(
                x => CurrentSceneCollectionChanged += x,
                x => CurrentSceneCollectionChanged -= x)
            .Select(x => x.EventArgs);
        
        OnSceneCollectionListChanged = Observable.FromEventPattern<SceneCollectionListChangedEventArgs>(
                x => SceneCollectionListChanged += x,
                x => SceneCollectionListChanged -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentSceneTransitionChanged = Observable.FromEventPattern<CurrentSceneTransitionChangedEventArgs>(
                x => CurrentSceneTransitionChanged += x,
                x => CurrentSceneTransitionChanged -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentSceneTransitionDurationChanged = Observable.FromEventPattern<CurrentSceneTransitionDurationChangedEventArgs>(
                x => CurrentSceneTransitionDurationChanged += x,
                x => CurrentSceneTransitionDurationChanged -= x)
            .Select(x => x.EventArgs);
        
        OnSceneTransitionStarted = Observable.FromEventPattern<SceneTransitionStartedEventArgs>(
                x => SceneTransitionStarted += x,
                x => SceneTransitionStarted -= x)
            .Select(x => x.EventArgs);
        
        OnSceneTransitionEnded = Observable.FromEventPattern<SceneTransitionEndedEventArgs>(
                x => SceneTransitionEnded += x,
                x => SceneTransitionEnded -= x)
            .Select(x => x.EventArgs);
        
        OnSceneTransitionVideoEnded = Observable.FromEventPattern<SceneTransitionVideoEndedEventArgs>(
                x => SceneTransitionVideoEnded += x,
                x => SceneTransitionVideoEnded -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentProfileChanged = Observable.FromEventPattern<CurrentProfileChangedEventArgs>(
                x => CurrentProfileChanged += x,
                x => CurrentProfileChanged -= x)
            .Select(x => x.EventArgs);
        
        OnProfileListChanged = Observable.FromEventPattern<ProfileListChangedEventArgs>(
                x => ProfileListChanged += x,
                x => ProfileListChanged -= x)
            .Select(x => x.EventArgs);
        
        OnStreamStateChanged = Observable.FromEventPattern<StreamStateChangedEventArgs>(
                x => StreamStateChanged += x,
                x => StreamStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnRecordStateChanged = Observable.FromEventPattern<RecordStateChangedEventArgs>(
                x => RecordStateChanged += x,
                x => RecordStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnReplayBufferStateChanged = Observable.FromEventPattern<ReplayBufferStateChangedEventArgs>(
                x => ReplayBufferStateChanged += x,
                x => ReplayBufferStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentPreviewSceneChanged = Observable.FromEventPattern<CurrentPreviewSceneChangedEventArgs>(
                x => CurrentPreviewSceneChanged += x,
                x => CurrentPreviewSceneChanged -= x)
            .Select(x => x.EventArgs);
        
        OnStudioModeStateChanged = Observable.FromEventPattern<StudioModeStateChangedEventArgs>(
                x => StudioModeStateChanged += x,
                x => StudioModeStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnExitStarted = Observable.FromEventPattern(
                x => ExitStarted += x,
                x => ExitStarted -= x)
            .ToUnit();
        
        OnConnected = Observable.FromEventPattern(
                x => Connected += x,
                x => Connected -= x)
            .ToUnit();
        
        OnDisconnected = Observable.FromEventPattern<ObsDisconnectionInfo>(
                x => Disconnected += x,
                x => Disconnected -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemSelected = Observable.FromEventPattern<SceneItemSelectedEventArgs>(
                x => SceneItemSelected += x,
                x => SceneItemSelected -= x)
            .Select(x => x.EventArgs);
        
        OnSceneItemTransformChanged = Observable.FromEventPattern<SceneItemTransformEventArgs>(
                x => SceneItemTransformChanged += x,
                x => SceneItemTransformChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputAudioSyncOffsetChanged = Observable.FromEventPattern<InputAudioSyncOffsetChangedEventArgs>(
                x => InputAudioSyncOffsetChanged += x,
                x => InputAudioSyncOffsetChanged -= x)
            .Select(x => x.EventArgs);
        
        OnSourceFilterCreated = Observable.FromEventPattern<SourceFilterCreatedEventArgs>(
                x => SourceFilterCreated += x,
                x => SourceFilterCreated -= x)
            .Select(x => x.EventArgs);
        
        OnSourceFilterRemoved = Observable.FromEventPattern<SourceFilterRemovedEventArgs>(
                x => SourceFilterRemoved += x,
                x => SourceFilterRemoved -= x)
            .Select(x => x.EventArgs);
        
        OnSourceFilterListReindexed = Observable.FromEventPattern<SourceFilterListReindexedEventArgs>(
                x => SourceFilterListReindexed += x,
                x => SourceFilterListReindexed -= x)
            .Select(x => x.EventArgs);
        
        OnSourceFilterEnableStateChanged = Observable.FromEventPattern<SourceFilterEnableStateChangedEventArgs>(
                x => SourceFilterEnableStateChanged += x,
                x => SourceFilterEnableStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputMuteStateChanged = Observable.FromEventPattern<InputMuteStateChangedEventArgs>(
                x => InputMuteStateChanged += x,
                x => InputMuteStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputVolumeChanged = Observable.FromEventPattern<InputVolumeChangedEventArgs>(
                x => InputVolumeChanged += x,
                x => InputVolumeChanged -= x)
            .Select(x => x.EventArgs);
        
        OnVendorEvent = Observable.FromEventPattern<VendorEventArgs>(
                x => VendorEvent += x,
                x => VendorEvent -= x)
            .Select(x => x.EventArgs);
        
        OnMediaInputPlaybackEnded = Observable.FromEventPattern<MediaInputPlaybackEndedEventArgs>(
                x => MediaInputPlaybackEnded += x,
                x => MediaInputPlaybackEnded -= x)
            .Select(x => x.EventArgs);
        
        OnMediaInputPlaybackStarted = Observable.FromEventPattern<MediaInputPlaybackStartedEventArgs>(
                x => MediaInputPlaybackStarted += x,
                x => MediaInputPlaybackStarted -= x)
            .Select(x => x.EventArgs);
        
        OnMediaInputActionTriggered = Observable.FromEventPattern<MediaInputActionTriggeredEventArgs>(
                x => MediaInputActionTriggered += x,
                x => MediaInputActionTriggered -= x)
            .Select(x => x.EventArgs);
        
        OnVirtualcamStateChanged = Observable.FromEventPattern<VirtualcamStateChangedEventArgs>(
                x => VirtualcamStateChanged += x,
                x => VirtualcamStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentSceneCollectionChanging = Observable.FromEventPattern<CurrentSceneCollectionChangingEventArgs>(
                x => CurrentSceneCollectionChanging += x,
                x => CurrentSceneCollectionChanging -= x)
            .Select(x => x.EventArgs);
        
        OnCurrentProfileChanging = Observable.FromEventPattern<CurrentProfileChangingEventArgs>(
                x => CurrentProfileChanging += x,
                x => CurrentProfileChanging -= x)
            .Select(x => x.EventArgs);
        
        OnSourceFilterNameChanged = Observable.FromEventPattern<SourceFilterNameChangedEventArgs>(
                x => SourceFilterNameChanged += x,
                x => SourceFilterNameChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputCreated = Observable.FromEventPattern<InputCreatedEventArgs>(
                x => InputCreated += x,
                x => InputCreated -= x)
            .Select(x => x.EventArgs);
        
        OnInputRemoved = Observable.FromEventPattern<InputRemovedEventArgs>(
                x => InputRemoved += x,
                x => InputRemoved -= x)
            .Select(x => x.EventArgs);
        
        OnInputNameChanged = Observable.FromEventPattern<InputNameChangedEventArgs>(
                x => InputNameChanged += x,
                x => InputNameChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputActiveStateChanged = Observable.FromEventPattern<InputActiveStateChangedEventArgs>(
                x => InputActiveStateChanged += x,
                x => InputActiveStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputShowStateChanged = Observable.FromEventPattern<InputShowStateChangedEventArgs>(
                x => InputShowStateChanged += x,
                x => InputShowStateChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputAudioBalanceChanged = Observable.FromEventPattern<InputAudioBalanceChangedEventArgs>(
                x => InputAudioBalanceChanged += x,
                x => InputAudioBalanceChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputAudioTracksChanged = Observable.FromEventPattern<InputAudioTracksChangedEventArgs>(
                x => InputAudioTracksChanged += x,
                x => InputAudioTracksChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputAudioMonitorTypeChanged = Observable.FromEventPattern<InputAudioMonitorTypeChangedEventArgs>(
                x => InputAudioMonitorTypeChanged += x,
                x => InputAudioMonitorTypeChanged -= x)
            .Select(x => x.EventArgs);
        
        OnInputVolumeMeters = Observable.FromEventPattern<InputVolumeMetersEventArgs>(
                x => InputVolumeMeters += x,
                x => InputVolumeMeters -= x)
            .Select(x => x.EventArgs);
        
        OnReplayBufferSaved = Observable.FromEventPattern<ReplayBufferSavedEventArgs>(
                x => ReplayBufferSaved += x,
                x => ReplayBufferSaved -= x)
            .Select(x => x.EventArgs);
        
        OnSceneCreated = Observable.FromEventPattern<SceneCreatedEventArgs>(
                x => SceneCreated += x,
                x => SceneCreated -= x)
            .Select(x => x.EventArgs);
        
        OnSceneRemoved = Observable.FromEventPattern<SceneRemovedEventArgs>(
                x => SceneRemoved += x,
                x => SceneRemoved -= x)
            .Select(x => x.EventArgs);
        
        OnSceneNameChanged = Observable.FromEventPattern<SceneNameChangedEventArgs>(
                x => SceneNameChanged += x,
                x => SceneNameChanged -= x)
            .Select(x => x.EventArgs);
    }
}