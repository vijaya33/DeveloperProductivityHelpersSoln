using System;
using System.Collections.Generic;
using System.Text;

using System.Text.RegularExpressions;

namespace GuardExtensions
{
    public static class StringGuardExtensions
    {
        public static string MustMatch(this string value, Regex regex, string? paramName = null)
        {
            if (regex is null) throw new ArgumentNullException(nameof(regex));
            if (!regex.IsMatch(value))
                throw new ArgumentException("String format is invalid.", paramName ?? "value");
            return value;
        }

        public static string MaxLength(this string value, int max, string? paramName = null)
        {
            if (max <= 0) throw new ArgumentOutOfRangeException(nameof(max));
            if (value.Length > max)
                throw new ArgumentException($"String length must be <= {max}.", paramName ?? "value");
            return value;
        }
    }

}