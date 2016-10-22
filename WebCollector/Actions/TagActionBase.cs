using SoftwareControllerApi.Action;

namespace WebCollector.Actions
{
    /// <summary>
    /// Base class for defining an action related to a single HTML tag.
    /// </summary>
    public abstract class TagActionBase : IAction
    {
        /// <summary>
        /// The HTML tag.
        /// </summary>
        public string Tag
        {
            get;
            set;
        }

        /// <summary>
        /// The class of the HTML tag.
        /// </summary>
        public string Class
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
        /// Execute the action.
        /// </summary>
        public abstract IResult Execute();
    }
}
