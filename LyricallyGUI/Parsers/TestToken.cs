using System.Diagnostics;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace LyricallyGUI.Parsers;

public class TestToken
{
    public static bool GoodTokenGenius(string token)
    {
        var client = new RestClient("https://api.genius.com/");
        var request = new RestRequest("songs/378195");
        request.AddHeader("Authorization", "Bearer " + token);
        var response = client.Execute(request);
        Debug.Assert(response.Content != null, "response.Content != null");
        var obj = JObject.Parse(response.Content);
        if (obj.GetValue("response") != null)
        {
            return true;
        } 
        return false;
    }
}