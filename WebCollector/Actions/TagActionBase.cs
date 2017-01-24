namespace WebCollector.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using SoftwareControllerApi.Action;

    /// <summary>
    /// Base class for defining an action related to a single HTML tag.
    /// </summary>
    public abstract class TagActionBase : IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagActionBase"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <exception cref="ArgumentNullException"><paramref name="session"/> is null.</exception>
        public TagActionBase(WebCollectorSession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            Session = session;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagActionBase"/> class with the given session.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <param name="session">The session.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty of white space only.</exception>
        public TagActionBase(string name, WebCollectorSession session) : this(session)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Cannot be empty of white space only.", "name");

            Name = name;
        }

        /// <summary>
        /// Get or set the HTML tag.
        /// </summary>
        public string Tag
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the collection of properties and their values.
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set flag saying if end tag should be included in pattern or not.
        /// </summary>
        protected bool WithEndTag
        {
            get;
            set;
        }

        private List<Regex> m_CollectRegexList;

        /// <summary>
        /// The regular expressions to be used when collecting HTML data.
        /// </summary>
        protected virtual List<Regex> CollectRegexes
        {
            get
            {
                if (m_CollectRegexList == null) {
                    m_CollectRegexList = new List<Regex>();

                    foreach (string pattern in GetPatterns(WithEndTag)) {
                        m_CollectRegexList.Add(new Regex(pattern));
                    }
                }

                return m_CollectRegexList;
            }
        }

        /// <summary>
        /// The name of the action.
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The session.
        /// </summary>
        public WebCollectorSession Session
        {
            get;
            set;
        }

        /// <summary>
        /// Execute the action.
        /// </summary>
        public abstract IResult Execute();

        /// <summary>
        /// Build the regex patterns using the tag and collection of properties.
        /// </summary>
        /// <param name="withEndTag">End tag should be included in the pattern.</param>
        /// <returns>The regular expression patterns list.</returns>
        /// <remarks>
        /// For each property there shall be a regex pattern.
        /// </remarks>
        protected List<string> GetPatterns(bool withEndTag)
        {
            List<string> patterns = new List<string>();

            foreach (KeyValuePair<string, string> pair in Properties) {
                StringBuilder patternBuilder = new StringBuilder();
                patternBuilder.Append("<");
                patternBuilder.Append(Tag);
                patternBuilder.Append("[^<]*");

                patternBuilder.Append(pair.Key);
                patternBuilder.Append('=');
                patternBuilder.Append("\"");
                patternBuilder.Append(pair.Value);
                patternBuilder.Append("\"");
                patternBuilder.Append("[^<]*>");

                if (withEndTag) {
                    patternBuilder.Append("[^<]*");
                    patternBuilder.Append("</");
                    patternBuilder.Append(Tag);
                    patternBuilder.Append(">");
                }

                patterns.Add(patternBuilder.ToString());
            }

            return patterns;
        }

        /// <summary>
        /// Get the list of matches which result from matching with all regular expression patterns.
        /// </summary>
        /// <returns>The list of matches.</returns>
        protected List<Match> GetMatches()
        {
            if (CollectRegexes.Count == 0) return new List<Match>();

            List<Match> matches = new List<Match>();
            MatchCollection matchCollection = CollectRegexes[0].Matches(Session.Html);
            foreach (Match match in matchCollection) {
                int count = 1;
                for (int index = 1; index < CollectRegexes.Count; index++) {
                    if (CollectRegexes[index].IsMatch(match.Groups[0].Value)) count++;
                }

                if (count == CollectRegexes.Count) matches.Add(match);
            }

            return matches;
        }
    }
}
