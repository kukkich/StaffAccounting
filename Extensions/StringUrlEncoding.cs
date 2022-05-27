namespace StaffAccounting.Extensions
{
    public static class StringUrlEncoding
    {
        public static string EncodeAsUrl(this string str) => System.Net.WebUtility.UrlEncode(str);
        public static string DecodeAsUrl(this string str) => System.Net.WebUtility.UrlDecode(str);
    }
}
