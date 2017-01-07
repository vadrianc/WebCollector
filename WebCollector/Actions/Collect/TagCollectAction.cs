namespace WebCollector.Actions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Collect;
    using SoftwareControllerApi.Action;
    using SoftwareControllerLib.Action;

    /// <summary>
    /// Action for collecting the content of a tag.
    /// </summary>
    public class TagCollectAction : TagActionBase, ICollectAction
    {
        private Regex m_CollectRegex;
        private MatchCollection m_Matches;
        private int m_CurrentIndex;

        /// <summary>
        /// The regular expression to be used when collecting HTML data.
        /// </summary>
        protected virtual Regex CollectRegex
        {
            get {
                if (m_CollectRegex != null) return m_CollectRegex;

                if (!string.IsNullOrEmpty(Tag) && !string.IsNullOrEmpty(Class)) {
                    string pattern = string.Format("<{0} [^<]*class=\"{1}\"[^<]*>[^<]*</{2}>", Tag, Class, Tag);
                    m_CollectRegex = new Regex(pattern);
                } else if (!string.IsNullOrEmpty(Tag)) {
                    string pattern = string.Format("<{0}\"[^<]*>[^<]*</{1}>", Tag, Tag);
                    m_CollectRegex = new Regex(pattern);
                }

                return m_CollectRegex;
            }
        }

        /// <summary>
        /// Get or set if the action should collect a single match or multiple matches.
        /// </summary>
        /// <remarks>
        /// If no explicit value is provided, <c>false</c> is the default value.
        /// </remarks>
        public bool IsMultipleCollect { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagCollectAction"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        public TagCollectAction(WebCollectorSession session) : this(session, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagCollectAction"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="isMultipleCollect">Flag indicating if the action should collect a single match or multiple matches.</param>
        public TagCollectAction(WebCollectorSession session, bool isMultipleCollect) : base(session)
        {
            IsMultipleCollect = isMultipleCollect;
        }

        /// <summary>
        /// Execute the collect action.
        /// </summary>
        /// <returns>The result in which the content of the found tag is stored.</returns>
        public override IResult Execute()
        {
            if (!CollectRegex.IsMatch(Session.Html)) return new Result(ActionState.FAIL);
            if (IsMultipleCollect) return ExecuteMultiCollect();

            return ExecuteSingleCollect();
        }

        /// <summary>
        /// Repeat condition used by the rule which holds the action.
        /// </summary>
        /// <returns><c>true</c> if the rule can be repeated, <c>false</c> otherwise.</returns>
        public bool CanRepeat()
        {
            if (m_Matches == null) return false;

            return m_CurrentIndex < m_Matches.Count;
        }

        private IResult ExecuteMultiCollect()
        {
            m_Matches = CollectRegex.Matches(Session.Html);
            IList<ItemBase> items = new List<ItemBase>();

            foreach (Match match in m_Matches) {
                if (!match.Success) continue;

                TextItem item = new TextItem(StripHtmlTags(match.Groups[0].Value));
                items.Add(item);
            }

            return new SingleResult<IList<ItemBase>>(items, ActionState.SUCCESS);
        }

        private IResult ExecuteSingleCollect()
        {
            m_Matches = CollectRegex.Matches(Session.Html);
            if (m_CurrentIndex >= m_Matches.Count) {
                m_CurrentIndex = 0;
            }

            if (m_Matches.Count == 0) {
                return new Result(ActionState.NOT_EXECUTED);
            }

            Match match = m_Matches[m_CurrentIndex];
            m_CurrentIndex++;

            if (!match.Success) return new Result(ActionState.NOT_EXECUTED);

            TextItem item = new TextItem(StripHtmlTags(match.Groups[0].Value));
            return new SingleResult<ItemBase>(item, ActionState.SUCCESS);
        }

        private string StripHtmlTags(string input)
        {
            int index = input.IndexOf('>');
            string result = input.Remove(0, index + 1);
            index = result.IndexOf('<');

            return result.Remove(index);
        }
    }
}