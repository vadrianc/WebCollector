namespace WebCollector
{
    using System;
    using SoftwareControllerLib.Control;

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
        /// <param name="startAddress">The first web address to navigate to.</param>
        public WebCollectorSession(string name, string address, string startAddress) : base(name)
        {
            if (address == null) throw new ArgumentNullException("address");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Cannot be empty or white space only", "address");
            if (startAddress == null) throw new ArgumentNullException("startAddress");
            if (string.IsNullOrWhiteSpace(startAddress)) throw new ArgumentException("Cannot be empty or white space only", "startAddress");

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
