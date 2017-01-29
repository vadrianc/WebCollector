namespace WebCollector.Actions
{
    /// <summary>
    /// Tag attribute properties model.
    /// </summary>
    public class TagAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public TagAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="isSingleQuote">Flag indicating if single quotes or double quotes are to used.</param>
        public TagAttribute(string name, string value, bool isSingleQuote)
        {
            Name = name;
            Value = value;
            IsSingleQuote = isSingleQuote;
        }

        /// <summary>
        /// Get or set the name of the attribute.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Get or set the value of the attribute.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Get or set if the attribute is value is single or double quoted.
        /// </summary>
        /// <value>
        /// True if single quotes shall be used, false if double quotes shall be used.
        /// </value>
        public bool IsSingleQuote
        {
            get;
            private set;
        }
    }
}
