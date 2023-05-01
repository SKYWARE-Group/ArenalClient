using Skyware.Arenal.Tracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyware.Arenal;

public static class StringExtensions
{

    public static string EmptyIfNull(this string original) => original ?? string.Empty;

}
