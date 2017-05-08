using Newtonsoft.Json;

namespace matrix_sdk.src
{
    class MFederateJson
    {
        [JsonProperty("m.federate")]
        public bool MFederate { get; set; }
    }
}
