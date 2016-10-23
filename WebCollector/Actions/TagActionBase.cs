using SoftwareControllerApi.Action;

namespace WebCollector.Actions
{
    /// <summary>
    /// Base class for defining an action related to a single HTML tag.
    /// </summary>
    public abstract class TagActionBase : IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagActionBase"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        public TagActionBase(WebCollectorSession session)
        {
            Session = session;
        }

        /// <summary>
        /// The HTML tag.
        /// </summary>
        public virtual string Tag
        {
            get;
            set;
        }

        /// <summary>
        /// The class of the HTML tag.
        /// </summary>
        public virtual string Class
        {
            get;
            set;
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
    }
}
