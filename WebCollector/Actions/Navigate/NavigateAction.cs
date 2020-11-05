namespace WebCollector.Actions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using SoftwareControllerApi.Action;
    using SoftwareControllerLib.Action;
    using SoftwareControllerLib.Utils;
    using Utils;

    /// <summary>
    /// Action for navigating to a web page.
    /// </summary>
    public class NavigateAction : TagActionBase, INavigateAction
    {
        private static Regex s_HrefRegex = new Regex("(href=\"[^\"]+\")|(href='[^']+')|(href=[^>]+>)");

        private int m_CurrentIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateAction"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="where">The navigation direction.</param>
        public NavigateAction(WebCollectorSession session, string where) : base(session)
        {
            Where = where;
            WithEndTag = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateAction"/> class with the given session.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="session">The session.</param>
        /// <param name="where">The navigation direction.</param>
        public NavigateAction(string name, WebCollectorSession session, string where) : base(name, session)
        {
            Where = where;
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
        /// Get the navigation direction, such as "back" or "forward".
        /// </summary>
        public string Where
        {
            get;
            private set;
        }

        /// <summary>
        /// Execute the navigation action.
        /// </summary>
        /// <returns>The result of the navigation action.</returns>
        public override IResult Execute()
        {
            if (!FindNextLink()) return new Result(ActionState.FAIL);
            string html = HtmlUtils.GetHtmlString(Link);
            Session.AddressTracker.Push(Link);
            Session.Html = html;

            ConsoleOutput.Instance.Message(string.Format("Navigated to {0}", Link));

            return new Result(ActionState.SUCCESS);
        }

        private bool FindNextLink()
        {
            if (Where != null && Where.Equals("back")) {
                Link = Session.AddressTracker.GetPreviousAddress();
                return true;
            }

            List<Match> matches = GetMatches();
            if (matches.Count == 0) return false;

            Match match = s_HrefRegex.Match(matches[m_CurrentIndex].Value);
            if (m_CurrentIndex == matches.Count - 1) m_CurrentIndex = 0;
            else m_CurrentIndex++;

            if (!match.Success) return false;

            Link = match.Value.Replace("href=", string.Empty);
            Link = Link.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(">", string.Empty);

            if (!Link.StartsWith(Session.Address)) Link = Session.Address + Link;
            if (Link.Contains("&amp;")) Link = Link.Replace("&amp;", "&");

            return true;
        }
    }
}
