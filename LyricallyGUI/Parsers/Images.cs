using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace LyricallyGUI.Parsers;

public class Images
{
    //TODO: Migrate to HttpClient();
    public Color GetAverageColor(Uri imageUrl)
    {
        var image = new MemoryStream(new WebClient().DownloadData(imageUrl));
        var bitmap = new Bitmap(image);


        /*for (var x = 0; x < bitmap.Width; x++)
        {
            var color = bitmap.GetPixel(x, y);

            rgb += color.ToArgb();
            
        }*/

        //rgb = rgb / bitmap.Width;
        return bitmap.GetPixel(30,30);

    }

    public Color InvertColor(Color color)
    {
        return color.GetBrightness() > 0.1 ? Color.Black : Color.White;

        /*var a = color.A;
        var r = color.R;
        var g = color.G;
        var b = color.B;*/
        
        //var iColor = Color.FromArgb(color.ToArgb() ^ 0xFFFFFF);
        
        /*var ia = iColor.A;
        var ir = iColor.R;
        var ig = iColor.G;
        var ib = iColor.B;
        if ((r < ir && r + 20 < ir) || (r > ir && r - 20 > ir)) //red
        {
            return Color.Black;
        } 
        if ((g < ig && g + 20 < ig) || (g > ig && g - 20 > ig)) 
        {
            return Color.Black;
        } 
        if ((b < ib && b + 20 < ib) || (b > ib && b - 20 > ir)) //red
        {
            return Color.Black;
        }*/

        //return iColor;
    }
    
}


