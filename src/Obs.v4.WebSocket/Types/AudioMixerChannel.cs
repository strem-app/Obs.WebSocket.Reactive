using Newtonsoft.Json;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Audio Mixer Channel information
    /// </summary>
    public class AudioMixerChannel
    {
        /// <summary>
        /// Is channel enabled
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { set; get; }

        /// <summary>
        /// ID of the channel
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "id")]
        public int ID { set; get; }
    }
}