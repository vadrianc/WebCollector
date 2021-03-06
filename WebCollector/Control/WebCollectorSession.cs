﻿namespace WebCollector
{
    using System;
    using System.Collections.Generic;
    using Actions.Navigate;
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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="address"/> is null
        /// or
        /// <paramref name="startAddress"/> is null.
        /// or
        /// <paramref name="outputFiles"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="address"/> is empty or whitespace only
        /// or
        /// <paramref name="startAddress"/> is empty or whitespace only.
        /// </exception>
        public WebCollectorSession(string name, string address, string startAddress, IList<string> outputFiles) : base(name)
        {
            if (address == null) throw new ArgumentNullException("address");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Cannot be empty or white space only", "address");
            if (startAddress == null) throw new ArgumentNullException("startAddress");
            if (string.IsNullOrWhiteSpace(startAddress)) throw new ArgumentException("Cannot be empty or white space only", "startAddress");
            if (outputFiles == null) throw new ArgumentNullException("outputFiles");

            Address = address;
            StartAddress = startAddress;
            AddressTracker = new BrowsingTracker();
            OutputFiles = outputFiles;
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

        /// <summary>
        /// Get an object holding the addresses where navigation occurred to.
        /// </summary>
        public BrowsingTracker AddressTracker
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the list of output files.
        /// </summary>
        public IList<string> OutputFiles
        {
            get;
            private set;
        }
    }
}
