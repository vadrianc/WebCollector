namespace WebCollector.Actions.Navigate
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Keeps track of the current addresses in a forward manner.
    /// </summary>
    public class BrowsingTracker
    { 
        private readonly Stack<string> m_Addresses;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsingTracker"/> class.
        /// </summary>
        public BrowsingTracker()
        {
            m_Addresses = new Stack<string>();
        }

        /// <summary>
        /// Add a new address to the history stack.
        /// </summary>
        /// <param name="address">The address to be added.</param>
        public void Push(string address)
        {
            if (address == null) throw new ArgumentNullException("address");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Cannot be empty of white space only", "address");

            m_Addresses.Push(address);
        }

        /// <summary>
        /// Remove and return the last address from the history stack.
        /// </summary>
        /// <returns>The last address from the history stack.</returns>
        public string Pop()
        {
            return m_Addresses.Pop();
        }

        /// <summary>
        /// Get the previous address from the navigation stack.
        /// </summary>
        public string GetPreviousAddress()
        {
            if (m_Addresses.Count == 0 || m_Addresses.Count == 1) return null;

            m_Addresses.Pop();
            return m_Addresses.Pop();
        }
    }
}
