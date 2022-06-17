using LyricallyGUIToken.Deserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace LyricallyGUI.Parsers;

public class SpotifyExtensions
{
    
    public static string GenerateToken(string refreshToken, string authToken)
    {
        //TODO: Implement Auto Base64 encode
        
        var client = new RestClient("https://accounts.spotify.com/");

        var request = new RestRequest("api/token", Method.Post);
        request.AddHeader("Authorization",
            "Basic " + authToken);
        //request.AddHeader("Content-Type", "application/x-www-form-urlencoded"); Not needed with v17
        request.AddParameter("grant_type", "refresh_token");
        request.AddParameter("refresh_token", refreshToken);
        var response = client.Execute(request);
        if (response.Content != null)
        {
            var obj = JObject.Parse(response.Content);
            if (obj.GetValue("error")?.ToString() == "invalid_client")
            {
                return "-1";
            }
        }

        var info = JsonConvert.DeserializeObject<Token>(response.Content!);
        return (info?.AccessToken) ?? string.Empty;
    }

    public static void SkipForward(string refreshToken, string authToken)
    {
        var client = new RestClient("https://api.spotify.com/");

        var request = new RestRequest("v1/me/player/next", Method.Post);
        request.AddHeader("Authorization",
            $"Bearer {GenerateToken(refreshToken, authToken)}");
        client.Execute(request);
    }
    
    public static void SkipBackward(string refreshToken, string authToken)
    {
        var client = new RestClient("https://api.spotify.com/");

        var request = new RestRequest("v1/me/player/previous", Method.Post);
        request.AddHeader("Authorization",
            $"Bearer {GenerateToken(refreshToken, authToken)}");
        client.Execute(request);
    }
}
