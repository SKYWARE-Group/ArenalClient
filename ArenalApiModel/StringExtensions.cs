using System.Text;

namespace Skyware.Arenal;

public static class StringExtensions
{

    public static string EmptyIfNull(this string original) => original ?? string.Empty;

    public static string Repeat(this string text, uint n) =>
        new StringBuilder(text.Length * (int)n)
          .Insert(0, text, (int)n)
          .ToString();

}
