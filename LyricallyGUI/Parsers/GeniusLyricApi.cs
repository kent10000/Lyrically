using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LyricallyGUI.Parsers;

public static class GeniusLyricApi
{
    public static string? GetLyrics(Uri webpage)
    {
        var web = new HtmlWeb();
        HtmlDocument doc;
        try
        {
            doc = web.Load(webpage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        var body = doc.DocumentNode.SelectSingleNode("//body");
        var child = body.ChildNodes;
        var node = child[3];
        var rawLyrics = node.InnerHtml;
        rawLyrics = rawLyrics.TrimStart().TrimEnd().Remove(0, 41);
        rawLyrics = rawLyrics.Remove(rawLyrics.Length - 1);
        rawLyrics = rawLyrics[(rawLyrics.IndexOf('>') + 1)..];

        rawLyrics = Regex.Replace(rawLyrics, "<br>", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, @"\\n", "\n");
        rawLyrics = Regex.Replace(rawLyrics, @"\\", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "</i>", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "<i>", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "]", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "\\?", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "\\[", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "amp;", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "<b>", string.Empty);
        rawLyrics = Regex.Replace(rawLyrics, "</b>", string.Empty);
        var subRawLyrics = Regex.Split(rawLyrics, "</p>");
        rawLyrics = subRawLyrics[0];
        
        string?[] rmRawLyrics = Regex.Split(rawLyrics, "<a");
        var lyricList = new List<string?>();
        foreach (var part in rmRawLyrics)
        {
            Debug.Assert(part != null, nameof(part) + " != null");
            string? lyrics;
            if (part.Contains('>'))
            {
                lyrics = part.Substring(part.IndexOf('>') + 1);
                lyrics = Regex.Replace(lyrics, "</a>", string.Empty);
            }
            else
            {
                lyrics = part;
            }

            lyrics = lyrics.TrimEnd().TrimStart();
            //Console.WriteLine(lyrics);
            lyricList.Add(lyrics + "\n");

        }

        return lyricList.Aggregate<string?, string?>(null, (current, line) => current + line);
    }
}