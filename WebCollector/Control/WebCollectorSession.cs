namespace WebCollector
{
    using SoftwareControllerLib.Control;

    /// <summary>
    /// The Web Collector session.
    /// </summary>
    public class WebCollectorSession : Session
    {
        private string m_Html;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebCollectorSession"/> class using the given name and web address.
        /// </summary>
        /// <param name="name">The name of the web collector session.</param>
        /// <param name="address">The web address.</param>
        /// <param name="startAddress">The first web address to navigate to.</param>
        public WebCollectorSession(string name, string address, string startAddress) : base(name)
        {
            Address = address;
            StartAddress = startAddress;
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
        /// The first address of the website to which to navigate to.
        /// </summary>
        public string StartAddress
        {
            get;
            private set;
        }


        /// <summary>
        /// The HTML of the current page.
        /// </summary>
        public string Html
        {
            get;
            set;
        }
    }
}
