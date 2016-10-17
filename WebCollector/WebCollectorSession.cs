using SoftwareControllerLib.Control;

namespace WebCollector
{
    /// <summary>
    /// The Web Collector session.
    /// </summary>
    public class WebCollectorSession : Session
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebCollectorSession"/> class using the given name and web address.
        /// </summary>
        /// <param name="name">The name of the web collector session.</param>
        /// <param name="address">The web address.</param>
        public WebCollectorSession(string name, string address) : base(name)
        {
            Address = address;
        }

        /// <summary>
        /// The address of the website from which data will be collected.
        /// </summary>
        public string Address
        {
            get;
            private set;
        }
    }
}
