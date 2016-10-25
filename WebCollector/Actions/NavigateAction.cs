using System;
using System.Text.RegularExpressions;
using SoftwareControllerApi.Action;
using SoftwareControllerLib.Action;
using SoftwareControllerLib.Utils;
using WebCollector.Utils;

namespace WebCollector.Actions
{
    /// <summary>
    /// Action for navigating to a web page.
    /// </summary>
    public class NavigateAction : TagActionBase, INavigateAction
    {
        private static Regex s_HrefRegex = new Regex("href=\"[^\"]+\"");

        private Regex m_HrefTagRegex;
        private string m_Class;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateAction"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        public NavigateAction(WebCollectorSession session) : base(session)
        {
        }

        /// <summary>
        /// Get the link to navigate to.
        /// </summary>
        public string Link
        {
            get;
            private set;
        }

        /// <summary>
        /// The class of the HTML tag.
        /// </summary>
        public override string Class
        {
            get
            {
                return m_Class;
            }

            set
            {
                m_HrefTagRegex = new Regex(string.Format("{0}{1}{2}", "<a [^<]*class=\"", value, "\"[^<]*>"));
                m_Class = value;
            }
        }

        /// <summary>
        /// Execute the navigation action.
        /// </summary>
        /// <returns>The result of the navigation action.</returns>
        public override IResult Execute()
        {
            if (!FindNextLink()) return new Result(null, ActionState.FAIL);
            string html = HtmlUtils.GetHtmlString(Link);
            Session.Html = html;

            ConsoleOutput.Instance.Message(string.Format("Navigated to {0}", Link));

            return new Result(html, ActionState.SUCCESS);
        }

        private bool FindNextLink()
        {
            MatchCollection matches = m_HrefTagRegex.Matches(Session.Html);
            if (matches.Count == 0) return false;

            Match match = s_HrefRegex.Match(matches[0].Value);
            if (!match.Success) return false;

            Link = match.Value.Replace("href=", string.Empty);
            Link = Link.Replace("\"", string.Empty);

            if (!Link.StartsWith(Session.Address)) Link = Session.Address + Link;
            if (Link.Contains("&amp;")) Link = Link.Replace("&amp;", "&");

            return true;
        }
    }
}
