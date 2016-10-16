using System;
using SoftwareControllerApi.Action;

namespace WebCollector.Actions
{
    /// <summary>
    /// Action for collecting the content of a tag.
    /// </summary>
    public class CollectTagAction : ICollectAction
    {
        /// <summary>
        /// The name of the action.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Execute the collect action.
        /// </summary>
        /// <returns>The result in which the content of the found tag is stored.</returns>
        public IResult Execute()
        {
            throw new NotImplementedException();
        }
    }
}
