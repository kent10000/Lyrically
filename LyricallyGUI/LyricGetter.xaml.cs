#region

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using LyricallyGUI.Parsers;
using LyricallyGUILyrics.Deserializers;
using LyricallyGUIPlaying.Deserializers;
using Newtonsoft.Json;

// ReSharper disable CommentTypo

#endregion

namespace LyricallyGUI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class LyricGetter
{
    //TODO: Add API token login system

    private readonly DispatcherTimer _timer = new();
    private int _interval;
    private string? _currentSong;
    private readonly Json _json;
    private string? _geniusToken;
    private string? _spotifyToken;
    private string _token;
    private string? _authToken;
    private bool _checkPass = true; //keep *false* | only set to true if you know tokens are correct

    public LyricGetter()
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        InitializeComponent();
        _json = new Json($"{directory}\\privatetokens.json");
        _geniusToken = _json.GetElement("GeniusToken");
        _spotifyToken = _json.GetElement("SpotifyToken");
        _authToken = _json.GetElement("SpotifyAuth");
        _token = SpotifyExtensions.GenerateToken(_spotifyToken!, _authToken!);
        _timer.Tick += timer_tick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, _interval);
        _timer.Start();
    }

    

    private void timer_tick(object? sender, EventArgs e)
    {
        Login();
        Lyrics();
    }

    private void Login()
    {
        //TODO: Optimze Checks
        if (_checkPass) return;
        while (_token == "-1")
        {
            var prompt = new Input
            {
                Title =
                {
                    Content = "Enter Basic Spotify Auth"
                }
            };
            prompt.ShowDialog();
            _json.ModifyElement("SpotifyAuth", prompt.InputText);
            _authToken = _json.GetElement("SpotifyAuth");
            _token = SpotifyExtensions.GenerateToken(_spotifyToken!, _authToken!);
        }

        while (_token == "")
        {
            var prompt = new Input
            {
                Title =
                {
                    Content = "Enter Spotify Token"
                }
            };
            prompt.ShowDialog();
            _json.ModifyElement("SpotifyToken", prompt.InputText);
            _spotifyToken = _json.GetElement("SpotifyToken");
            _token = SpotifyExtensions.GenerateToken(_spotifyToken!, _authToken!);
        }

        while (TestToken.GoodTokenGenius(_geniusToken!) == false)
        {
            var prompt = new Input
            {
                Title =
                {
                    Content = "Enter Genius Token"
                }
            };
            prompt.ShowDialog();
            _json.ModifyElement("GeniusToken", prompt.InputText);
            _geniusToken = _json.GetElement("GeniusToken");
        }

        _checkPass = true;
    }


    private async void Lyrics()
    {
        _token = SpotifyExtensions.GenerateToken(_spotifyToken!, _authToken!);
        var spotifyClient = new HttpClient();
        const string spotifyBaseAddress = "https://api.spotify.com";
        spotifyClient.BaseAddress = new Uri(spotifyBaseAddress);
        spotifyClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(
            "Bearer " + _token);
        var spotifyResponse = await spotifyClient.GetAsync("/v1/me/player/currently-playing");
        var songJson = await spotifyResponse.Content.ReadAsStringAsync();
        var songData = JsonConvert.DeserializeObject<Playing>(songJson);

        //todo: potential crash when switcing from no song playing to song

        if (songData == null || songData.IsPlaying == false)
        {
            if (_currentSong == null)
            {
                Title.Text = "Error! Song not found";
                Main.Text = "There is no song currently playing.";
            }

            _timer.Interval = new TimeSpan(0, 0, 15);
            return;
        }

        if (_currentSong != songData.Item.Name)
        {
            var songTitle = songData.Item.Name;
            _currentSong = songTitle;
            var songArtists = songData.Item.Artists;
            var searchUri = Song.ParseSong(songTitle!, songArtists!);
            var albumArt = songData.Item.Album.Images[0].Url;

            var geniusClient = new HttpClient();
            const string geniusBaseAddress = "https://api.genius.com";
            geniusClient.BaseAddress = new Uri(geniusBaseAddress);
            geniusClient.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse("Bearer " + _geniusToken);
            var geniusResponse = await geniusClient.GetAsync(searchUri);
            var lyricsJson = await geniusResponse.Content.ReadAsStringAsync();
            var lyricsData = JsonConvert.DeserializeObject<Lyrics>(lyricsJson);
            if (lyricsData == null) return;
            
            
            //TODO: Songs with many artists not working. 

            var tArray = songTitle.Split('(');
            var pSongTitle = tArray[0];
            tArray = pSongTitle.Split('-');
            pSongTitle = tArray[0];
            pSongTitle = pSongTitle.TrimEnd();


            //Uri lyricUrl;
            Hit? searchSong = null;

            //Proposed Search Alg

            /*if (lyricsData?.Response.Hits[0] != null)
            {
                searchSong = lyricsData?.Response.Hits[0];
            }
            else
            {
                searchSong = null;
            }
            */
            /*foreach (var song in lyricsData?.Response.Hits)
            {
                var nameOfSong = song.Result.Title;
                var removeD = new Regex("-");
                var removeP = new Regex("\\(");
                nameOfSong = removeD.Split(nameOfSong)[0];
                nameOfSong = removeP.Split(nameOfSong)[0];
                
                if (nameOfSong == pSongTitle)
                {
                    searchSong = song;
                }
                /*var songResponse = await geniusClient.GetAsync(song.Result.ApiPath);
                var songsJson = await songResponse.Content.ReadAsStringAsync();
                var songsData = JsonConvert.DeserializeObject<Songs>(songsJson);
                var titleOfSongG = song.Result.Title;
                var alblumObjectOfSongG = songsData.Response.Song.Album;
                string alblumOfSongG;
                string alblumOfSongS;
                if (songData?.Item.Album.AlbumType == "single")
                {
                    alblumOfSongS = null;
                }
                else
                {
                    alblumOfSongS = songData.Item.Album.Name;
                }

                if (alblumObjectOfSongG != null)
                {
                    alblumOfSongG = alblumObjectOfSongG.Name;
                }
                else
                {
                    alblumOfSongG = null;
                }

                if (alblumOfSongS == alblumOfSongG)
                {
                    searchSong = song;
                }*/
            //}


            foreach (var song in lyricsData.Response.Hits)
            {
                var reg = new Regex("\\.");
                var title = song.Result.Title;
                var titleArray = title.Split('(');
                title = titleArray[0];
                titleArray = title.Split('-');
                title = titleArray[0];
                title = title.TrimEnd();

                title = reg.Replace(title, string.Empty);
                pSongTitle = reg.Replace(pSongTitle, string.Empty);

                if (!string.Equals(title, pSongTitle, StringComparison.CurrentCultureIgnoreCase)) continue;
                searchSong = song;
                break;
            }

            if (searchSong == null)
            {
                searchUri = Song.PrimativeParseSong(songTitle, songArtists!);
                geniusResponse = await geniusClient.GetAsync(searchUri);
                lyricsJson = await geniusResponse.Content.ReadAsStringAsync();
                lyricsData = JsonConvert.DeserializeObject<Lyrics>(lyricsJson);
                if (lyricsData == null) return;
                foreach (var song in lyricsData.Response.Hits)
                {
                    var reg = new Regex("\\.");
                    var title = song.Result.Title;
                    var titleArray = title.Split('(');
                    title = titleArray[0];
                    titleArray = title.Split('-');
                    title = titleArray[0];
                    title = title.TrimEnd();

                    title = reg.Replace(title, string.Empty);
                    pSongTitle = reg.Replace(pSongTitle, string.Empty);

                    if (!string.Equals(title, pSongTitle, StringComparison.CurrentCultureIgnoreCase)) continue;
                    searchSong = song;
                    break;
                }
            }

            string titleText;
            string bodyText;
            if (searchSong != null)
            {
                var searchUrl = searchSong.Result.Url;

                if (searchUrl != null)
                {
                    var lyrics = GeniusLyricApi.GetLyrics(searchUrl);

                    var fullTitle = searchSong.Result.FullTitle;
                    titleText = $"{fullTitle}";
                    Debug.Assert(lyrics != null, nameof(lyrics) + " != null");
                    bodyText = lyrics;
                }
                else
                {
                    titleText = "Error! Lyrics Not Found!";
                    bodyText = $"Unable to find lyrics for {songTitle}";
                }
            }
            else
            {
                titleText = "Error! Lyrics Not Found!";
                bodyText = $"Unable to find lyrics for \"{songTitle}\"";
            }


            Title.Text = titleText;
            Main.Text = bodyText;


            var images = new Images();
            var color = images.GetAverageColor(albumArt!);
            var a = color.A;
            var r = color.R;
            var g = color.G;
            var b = color.B;
            var brush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            
            Main.Background = brush;
            Top.Background = brush;
            Top.Background = brush;

            //TODO: Add option for richer background color; GetAverageColor() instead of picking a single pixel

            var iColor = images.InvertColor(color);

            var ia = iColor.A;
            var ir = iColor.R;
            var ig = iColor.G;
            var ib = iColor.B;
            var iBrush = new SolidColorBrush(Color.FromArgb(ia, ir, ig, ib));
            Main.Foreground = iBrush;
            Title.Foreground = iBrush;
            Settings.Foreground = iBrush;

            //Console.Clear();
            //Console.WriteLine(searchUri);
            //Console.WriteLine(lyricUrl);
            //Console.WriteLine(lyrics);

            /*Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    Title.Text = $"{lyricsData!.Response.Hits[0].Result.FullTitle}";
                    Main.Text = lyrics;
                    var images = new Images();
                    var color = images.GetAverageColor(alblumArt!);
                    var a = color.A;
                    var r = color.R;
                    var g = color.G;
                    var b = color.B;
                    var brsuh = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    /*var dColor = images.DarkenColor(color, .1f);
                    var da = dColor.A;
                    var dr = dColor.R;
                    var dg = dColor.G;
                    var db = dColor.B;
                    var dBrsuh = new SolidColorBrush(Color.FromArgb(a, r, g, b));*/
            //var tBrsuh = new SolidColorBrush(Color.FromArgb(a, (byte)(r+5), (byte)(g+5), (byte)(b+5)));
            /*Main.Background = brsuh;
            Title.Background = brsuh;

            var iColor = images.InvertColor(color);

            var ia = iColor.A;
            var ir = iColor.R;
            var ig = iColor.G;
            var ib = iColor.B;
            var iBrsuh = new SolidColorBrush(Color.FromArgb(ia, ir, ig, ib));
            Main.Foreground = iBrsuh;
            Title.Foreground = iBrsuh;
        }
        );*/
            //Console.WriteLine(color);
        }

        var timeout = songData.Item.DurationMs - songData.ProgressMs;
        if (timeout != null) _interval = (int)timeout;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, _interval + 1000);
    }
    
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        _timer.Interval = new TimeSpan(0);
    }

    private void ButtonRepeat_OnClick(object sender, RoutedEventArgs e)
    {
        SpotifyExtensions.SkipBackward(_spotifyToken!, _authToken!);
        _timer.Interval = new TimeSpan(10);
    }

    private void ButtonSkip_OnClick(object sender, RoutedEventArgs e)
    {
        SpotifyExtensions.SkipForward(_spotifyToken!, _authToken!);
        _timer.Interval = new TimeSpan(10);
    }

    private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
    {
        MainWindow.ToggleContent();
    }
}