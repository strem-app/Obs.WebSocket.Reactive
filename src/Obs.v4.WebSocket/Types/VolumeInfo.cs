﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Volume settings of an OBS source
    /// </summary>
    public class VolumeInfo
    {
        /// <summary>
        /// Source volume in linear scale (0.0 to 1.0)
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "volume")]
        public float Volume { internal set; get; }

        /// <summary>
        /// True if source is muted, false otherwise
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "muted")]
        public bool Muted { internal set; get; }

        /// <summary>
        /// Builds the object from the JSON response body
        /// </summary>
        /// <param name="data">JSON response body as a <see cref="JObject"/></param>
        public VolumeInfo(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }
}