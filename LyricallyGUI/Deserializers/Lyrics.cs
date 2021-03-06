// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var lyrics = Lyrics.FromJson(jsonString);

using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LyricallyGUILyrics.Deserializers
{
    public partial class Lyrics
    {
        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public Meta Meta { get; set; }

        [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
        public Response Response { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public long? Status { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("hits", NullValueHandling = NullValueHandling.Ignore)]
        public Hit[] Hits { get; set; }
    }

    public partial class Hit
    {
        [JsonProperty("highlights", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Highlights { get; set; }

        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public Index? Index { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public Index? Type { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public Result Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("annotation_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? AnnotationCount { get; set; }

        [JsonProperty("api_path", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiPath { get; set; }

        [JsonProperty("artist_names", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtistNames { get; set; }

        [JsonProperty("full_title", NullValueHandling = NullValueHandling.Ignore)]
        public string FullTitle { get; set; }

        [JsonProperty("header_image_thumbnail_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri HeaderImageThumbnailUrl { get; set; }

        [JsonProperty("header_image_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri HeaderImageUrl { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("lyrics_owner_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? LyricsOwnerId { get; set; }

        //[JsonProperty("lyrics_state", NullValueHandling = NullValueHandling.Ignore)]
        //public LyricsState? LyricsState { get; set; }

        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty("pyongs_count")]
        public long? PyongsCount { get; set; }

        [JsonProperty("relationships_index_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri RelationshipsIndexUrl { get; set; }

        [JsonProperty("release_date_components")]
        public ReleaseDateComponents ReleaseDateComponents { get; set; }

        [JsonProperty("release_date_for_display")]
        public string ReleaseDateForDisplay { get; set; }

        [JsonProperty("song_art_image_thumbnail_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SongArtImageThumbnailUrl { get; set; }

        [JsonProperty("song_art_image_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SongArtImageUrl { get; set; }

        [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
        public Stats Stats { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("title_with_featured", NullValueHandling = NullValueHandling.Ignore)]
        public string TitleWithFeatured { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }

        [JsonProperty("featured_artists", NullValueHandling = NullValueHandling.Ignore)]
        public object[] FeaturedArtists { get; set; }

        [JsonProperty("primary_artist", NullValueHandling = NullValueHandling.Ignore)]
        public PrimaryArtist PrimaryArtist { get; set; }
    }

    public partial class PrimaryArtist
    {
        [JsonProperty("api_path", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiPath { get; set; }

        [JsonProperty("header_image_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri HeaderImageUrl { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("image_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ImageUrl { get; set; }

        [JsonProperty("is_meme_verified", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMemeVerified { get; set; }

        [JsonProperty("is_verified", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsVerified { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }

        [JsonProperty("iq", NullValueHandling = NullValueHandling.Ignore)]
        public long? Iq { get; set; }
    }

    public partial class ReleaseDateComponents
    {
        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public long? Year { get; set; }

        [JsonProperty("month", NullValueHandling = NullValueHandling.Ignore)]
        public long? Month { get; set; }

        [JsonProperty("day", NullValueHandling = NullValueHandling.Ignore)]
        public long? Day { get; set; }
    }

    public partial class Stats
    {
        [JsonProperty("unreviewed_annotations", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnreviewedAnnotations { get; set; }

        [JsonProperty("hot", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hot { get; set; }

        [JsonProperty("pageviews", NullValueHandling = NullValueHandling.Ignore)]
        public long? Pageviews { get; set; }
    }

    public enum Index { Song };

    public enum LyricsState { Complete };

    public partial class Lyrics
    {
        public static Lyrics FromJson(string json) => JsonConvert.DeserializeObject<Lyrics>(json, LyricallyGUILyrics.Deserializers.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Lyrics self) => JsonConvert.SerializeObject((object)self, (JsonSerializerSettings)LyricallyGUILyrics.Deserializers.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                IndexConverter.Singleton,
                LyricsStateConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class IndexConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Index) || t == typeof(Index?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "song")
            {
                return Index.Song;
            }
            throw new Exception("Cannot unmarshal type Index");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Index)untypedValue;
            if (value == Index.Song)
            {
                serializer.Serialize(writer, "song");
                return;
            }
            throw new Exception("Cannot marshal type Index");
        }

        public static readonly IndexConverter Singleton = new IndexConverter();
    }

    internal class LyricsStateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LyricsState) || t == typeof(LyricsState?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "complete")
            {
                return LyricsState.Complete;
            }
            throw new Exception("Cannot unmarshal type LyricsState");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (LyricsState)untypedValue;
            if (value == LyricsState.Complete)
            {
                serializer.Serialize(writer, "complete");
                return;
            }
            throw new Exception("Cannot marshal type LyricsState");
        }

        public static readonly LyricsStateConverter Singleton = new LyricsStateConverter();
    }
}
