namespace Obs.v4.WebSocket.Types
{
    /// <summary>
    /// Interface implemented by responses to test if its valid.
    /// </summary>
    public interface IValidatedResponse
    {
        /// <summary>
        /// True if the response is valid, false otherwise.
        /// </summary>
        public bool ResponseValid { get; }
    }
}
