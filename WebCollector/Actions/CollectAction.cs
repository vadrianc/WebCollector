using SoftwareControllerApi.Action;
using SoftwareControllerLib.Action;

namespace WebCollector.Actions
{
    /// <summary>
    /// Action for collecting the content of a tag.
    /// </summary>
    public class CollectAction : TagActionBase, ICollectAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectAction"/> class with the given session.
        /// </summary>
        /// <param name="session">The session.</param>
        public CollectAction(WebCollectorSession session) : base(session)
        {
        }

        /// <summary>
        /// Execute the collect action.
        /// </summary>
        /// <returns>The result in which the content of the found tag is stored.</returns>
        public override IResult Execute()
        {
            return new Result(null, ActionState.NOT_EXECUTED);
        }
    }
}
