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
            Delimiter = AttributeDelimiter.DoubleQuote;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="delimiter">Flag indicating if the attribute value is single or double
        /// quoted or no quote is present.</param>
        public TagAttribute(string name, string value, AttributeDelimiter delimiter)
        {
            Name = name;
            Value = value;
            Delimiter = delimiter;
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
        /// Get or set if the attribute value is single or double quoted or no quote is present.
        /// </summary>
        public AttributeDelimiter Delimiter
        {
            get;
            private set;
        }
    }
}
