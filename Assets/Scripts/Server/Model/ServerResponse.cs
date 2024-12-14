using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Server
{
    public class ServerResponse
    {
        [JsonProperty("response")] public string Response { get; private set; }
        [JsonProperty("status")] public string Status { get; private set; }
        [JsonProperty("data_submitted")] public string Data { get; private set; }
        [JsonIgnore] public ResponseType ResponseType { get; private set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            ResponseType = (ResponseType)Enum.Parse(typeof(ResponseType), Response);
        }
    }
}