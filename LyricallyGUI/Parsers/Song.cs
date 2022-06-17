using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using LyricallyGUIPlaying.Deserializers;

namespace LyricallyGUI.Parsers;

public static class Song
{
    public static string ParseSong(string songName, IEnumerable<Artist> songArtists)
    {
        
        var songNameArray = songName.Split(' ');

        var isRemix = songName.ToLower().Contains("remix");
        var search = songNameArray.Aggregate<string?, string?>(null, (current, word) => current + word + "%20");
        search = search?.Remove(search.Length - 3);
        Debug.Assert(search != null, nameof(search) + " != null");
        var subSearch = Regex.Split(search, "%20-");
        search = subSearch[0];
        if (search.Contains('('))
        {
            search = search.Remove(search.IndexOf('(')-3);
            search = search.TrimEnd();
        }
        if (isRemix)
        {
            search = search + "%20(Remix)";
        }
        search = songArtists.Select(artist => artist.Name).Select(artistName => artistName.Split(' ')).Aggregate(search, (current1, artistNameArray) => artistNameArray.Aggregate(current1, (current, word) => current + "%20" + word));

        /*foreach (var artist in songArtists)
        {
            var artistName = artist.Name;
            var artistNameArray = artistName.Split(' ');
            search = artistNameArray.Aggregate(search, (current, word) => current + "%20" + word);
        }*/
        search = "/search?q=" + search;
        return search;
    }

    public static string PrimativeParseSong(string songName, Artist[] songArtists)
    {
        var songNameArray = songName.Split(' ');

        var isRemix = songName.ToLower().Contains("remix");
        var search = songNameArray.Aggregate<string?, string?>(null, (current, word) => current + word + "%20");
        search = search?.Remove(search.Length - 3);
        Debug.Assert(search != null, nameof(search) + " != null");
        var subSearch = Regex.Split(search, "%20-");
        search = subSearch[0];
        if (search.Contains('('))
        {
            search = search.Remove(search.IndexOf('(')-3);
            search = search.TrimEnd();
        }
        if (isRemix)
        {
            search = search + "%20(Remix)";
        }

        search += "%20" + songArtists[0].Name;

        /*foreach (var artist in songArtists)
        {
            var artistName = artist.Name;
            var artistNameArray = artistName.Split(' ');
            search = artistNameArray.Aggregate(search, (current, word) => current + "%20" + word);
        }*/
        search = "/search?q=" + search;
        return search;
    }
}