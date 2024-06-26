﻿using Newtonsoft.Json;

namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Streaming server settings
    /// </summary>
    public class StreamingServiceSettings : IValidatedResponse
    {
        /// <inheritdoc/>
        public bool ResponseValid => !string.IsNullOrEmpty(Server);
        /// <summary>
        /// The publish URL
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "server")]
        public string Server { set; get; } = null!;

        /// <summary>
        /// The publish key of the stream
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string? Key { set; get; }

        /// <summary>
        /// Indicates whether authentication should be used when connecting to the streaming server
        /// </summary>
        [JsonProperty(PropertyName = "use-auth")]
        public bool UseAuth { set; get; }

        /// <summary>
        /// The username to use when accessing the streaming server. Only present if use-auth is true
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string? Username { set; get; }

        /// <summary>
        /// The password to use when accessing the streaming server. Only present if use-auth is true
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string? Password { set; get; }
    }
}