using System;
using SoftwareControllerApi.Action;

namespace WebCollector.Actions
{
    /// <summary>
    /// Action for collecting the content of a tag.
    /// </summary>
    public class CollectAction : TagActionBase, ICollectAction
    {
        /// <summary>
        /// Execute the collect action.
        /// </summary>
        /// <returns>The result in which the content of the found tag is stored.</returns>
        public override IResult Execute()
        {
            throw new NotImplementedException();
        }
    }
}
