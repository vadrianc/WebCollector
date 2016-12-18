namespace WebCollector.Actions
{
    using System;
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
