namespace WebCollector.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Exposes utility methods for working with HTML.
    /// </summary>
    public static class HtmlUtils
    {
        /// <summary>
        /// Navigates to the given URI and returns the response string.
        /// </summary>
        /// <param name="uri">URI to navigate to.</param>
        /// <returns>The response string.</returns>
        /// 
        public static string GetHtmlString(string uri)
        {
            if (uri == null) throw new ArgumentNullException("uri", "Cannot be null");
            if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentException("Cannot be empty or whitespace only", "uri");
            if (!Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute)) throw new ArgumentException(string.Format("Invalid uri: {0}", uri));

            WebRequest request = WebRequest.Create(uri);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            HttpWebResponse webResponse = response as HttpWebResponse;
            if (webResponse != null && webResponse.StatusCode != HttpStatusCode.OK) return null;

            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream)) {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Strip the starting and ending tags of a HTML element.
        /// </summary>
        /// <param name="input">The HTML element.</param>
        /// <returns>The text between the starting and ending tags of a HTML element.</returns>
        public static string StripHtmlTags(string input)
        {
            if (input == null) throw new ArgumentNullException("input", "Cannot be null");

            int start = input.IndexOf('<');
            int end = input.IndexOf('>');
            if (start == -1 || end == -1) throw new ArgumentException(string.Format("Invalid HTML element: {0}", input), "input");

            while (start != -1 && end != -1) {
                if (start > end) throw new ArgumentException(string.Format("Badly formatted HTML element: {0}", input), "input");
                input = input.Remove(start, end - start + 1);
                start = input.IndexOf('<');
                end = input.IndexOf('>');
            }

            return input;
        }

        /// <summary>
        /// Taken from http://www.w3schools.com/html/html_entities.asp
        /// </summary>
        private static readonly Dictionary<string, char> s_Symbols = new Dictionary<string, char>() {
            { "&nbsp;", ' ' },
            { "&lt;", '<' },
            { "&gt;", '>' },
            { "&amp;", '&' },
            { "&quot;", '"' },
            { "&apos;", '\'' },
            { "&cent;", '¢' },
            { "&pound;", '£' },
            { "&yen;", '¥' },
            { "&euro;", '€' },
            { "&copy;", '©' },
            { "&reg;", '®' }
        };

        /// <summary>
        /// Replace all HTML specific symbols with their corresponding character.
        /// </summary>
        /// <param name="input">The input string containing symbols.</param>
        /// <returns>The input string with replaced symbols.</returns>
        public static string ReplaceSymbols(string input)
        {
            if (input == null) throw new ArgumentNullException("input");

            foreach (KeyValuePair<string, char> pair in s_Symbols) {
                input = input.Replace(pair.Key, pair.Value.ToString());
            }

            return input;
        }
    }
}
