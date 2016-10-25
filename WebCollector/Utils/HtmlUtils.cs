using System;
using System.IO;
using System.Net;

namespace WebCollector.Utils
{
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

            if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK) return null;

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
