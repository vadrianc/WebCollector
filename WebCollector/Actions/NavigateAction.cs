using System;
using SoftwareControllerApi.Action;

namespace WebCollector.Actions
{
    /// <summary>
    /// Action for navigating to a web page.
    /// </summary>
    public class NavigateAction : TagActionBase, INavigateAction
    {
        /// <summary>
        /// Get the link to navigate to.
        /// </summary>
        public string Link
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
            throw new NotImplementedException();
        }
    }
}
