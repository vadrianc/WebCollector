namespace WebCollector.Actions.Collect
{
    using System.Collections.Generic;

    /// <summary>
    /// Base class for defining an item that was collected.
    /// </summary>
    public abstract class ItemBase
    {
        /// <summary>
        /// The child items of this item.
        /// </summary>
        public IList<ItemBase> SubItems
        {
            get;
            set;
        }
    }
}
