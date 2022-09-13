using System.Collections.Generic;
using Newtonsoft.Json;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Describes a scene in OBS, along with its items
    /// </summary>
    public class OBSScene
    {
        /// <summary>
        /// OBS Scene name
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; } = null!;

        /// <summary>
        /// Scene item list
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "sources")]
        public List<SceneItem> Items = null!;
    }
}