﻿namespace WebCollector.Utils
{
    using System;
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
    }
}
