using System;

namespace WebCollector
{
    /// <summary>
    /// The Web Collector session.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// The name of the session.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The address of the website from which data will be collected.
        /// </summary>
        public string Address
        {
            get;
            private set;
        }

        /// <summary>
        /// Run the session.
        /// </summary>
        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
