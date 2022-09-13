using Newtonsoft.Json;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class MediaSourceSettings
    {
        /// <summary>
        /// Source Name
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { get; set; } = null!;

        /// <summary>
        /// Source Type
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sourceType")]
        public string SourceType { get; set; } = null!;

        /// <summary>
        /// Media settings
        /// </summary>
        [JsonProperty(PropertyName = "sourceSettings")]
        public FFMpegSourceSettings? Media { get; set; }


    }
}
