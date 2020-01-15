using Newtonsoft.Json;
using System.Drawing;

namespace Onset.Utils
{
    /// <summary>
    /// Represents a RGBA color with the 255 format.
    /// </summary>
    public class Color
    {
        [JsonProperty("r")]
        public int Red { get; set; }

        [JsonProperty("g")]
        public int Green { get; set; }

        [JsonProperty("b")]
        public int Blue { get; set; }

        [JsonProperty("a")]
        public int Alpha { get; set; }

        public Color(int red = 255, int green = 255, int blue = 255, int alpha = 255)
        {
            Red = red;
            Blue = blue;
            Green = green;
            Alpha = alpha;
        }

        public Color(Color color) : this(color.Red, color.Green, color.Blue, color.Alpha)
        {
        }

        public Color(string hex) : this(ColorTranslator.FromHtml(!hex.StartsWith("#") ? "#" + hex : hex))
        {
        }

        public Color(System.Drawing.Color color) : this(color.R, color.G, color.B, color.A)
        {
        }

        public override string ToString()
        {
            return $"{Red:X2}{Green:X2}{Blue:X2}{Alpha:X2}";
        }

        public string ToHtmlHex()
        {
            return "#" + ToString();
        }
    }
}
