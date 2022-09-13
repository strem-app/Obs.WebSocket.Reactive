using Newtonsoft.Json;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Scene transition override settings
    /// </summary>
    public class TransitionOverrideInfo
    {
        /// <summary>
        /// Name of the current overriding transition. Empty string if no override is set.
        /// </summary>
        [JsonProperty(PropertyName = "transitionName")]
        public string Name { internal set; get; } = string.Empty;

        /// <summary>
        /// Transition duration in milliseconds. -1 if no override is set.
        /// </summary>
        [JsonProperty(PropertyName = "transitionDuration")]
        public int Duration { internal set; get; } = -1;
    }
}
