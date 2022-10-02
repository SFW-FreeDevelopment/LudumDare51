using System;
using System.Text;

namespace LD51.Unity.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64String(this string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }
    }
}