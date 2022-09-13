using System.Collections.Generic;
using Newtonsoft.Json;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Get Scene Info response
    /// </summary>
    public class GetSceneListInfo
    {
        /// <summary>
        /// Name of the currently active scene
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "current-scene")]
        public string CurrentScene { set; get; } = null!;

        /// <summary>
        /// Ordered list of the current profile's scenes
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "scenes")]
        public List<OBSScene> Scenes { set; get; } = null!;
    }
}