namespace WebCollector.Actions.Collect
{
    using System.Collections.Generic;

    /// <summary>
    /// Item representing a text.
    /// </summary>
    public class TextItem : ItemBase
    {
        /// <summary>
        /// The text content of the item.
        /// </summary>
        public string Content
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextItem"/> class.
        /// </summary>
        /// <param name="content">The text content.</param>
        public TextItem(string content) : this(content, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextItem"/> class.
        /// </summary>
        /// <param name="content">The text content.</param>
        /// <param name="subItems">The child items of this item.</param>
        public TextItem(string content, IList<ItemBase> subItems)
        {
            Content = content;
            SubItems = subItems;
        }
    }
}
