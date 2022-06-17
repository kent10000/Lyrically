using System.Diagnostics.CodeAnalysis;
using System.IO;
using LyricallyGUIOptions.Deserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LyricallyGUI;

public class Json
{
    private readonly string _filepath;
    private JObject _json;
    public Json(string filepath)
    {
        _filepath = filepath;
        _json = JObject.Parse(File.ReadAllText(_filepath));
    }

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public void ModifyElement(string elementName, string elementValue)
    {
        var content = JsonConvert.DeserializeObject<Options>(File.ReadAllText(_filepath));
        switch (elementName.ToLower())
        {
            case "geniustoken":
                if (content != null) content.GeniusToken = elementValue;
                break;
            case "spotifytoken":
                if (content != null) content.SpotifyToken = elementValue;
                break;
            case "userexpui":
                if (content != null) content.UserExpUi = elementValue.ToLower() is "0" or "true" || true;
                break;
            case "spotifyauth":
                if (content != null) content.SpotifyAuth = elementValue;
                break;
        }

        var serializeObject = JsonConvert.SerializeObject(content);
        File.WriteAllText(_filepath, serializeObject);
        _json = JObject.Parse(File.ReadAllText(_filepath));




    }

    public string? GetElement(string elementName)
    {
        var value = _json.GetValue(elementName);

        return value?.ToString();
    }
    
}