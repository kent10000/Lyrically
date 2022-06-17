namespace LyricallyGUIOptions.Deserializers
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Options
    {
        [JsonProperty("SpotifyToken")]
        public object SpotifyToken { get; set; }

        [JsonProperty("GeniusToken")]
        public object GeniusToken { get; set; }

        [JsonProperty("UserExpUI")]
        public bool UserExpUi { get; set; }
        
        [JsonProperty("SpotifyAuth")]
        
        public object SpotifyAuth { get; set; }
    }
}