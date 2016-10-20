using System.Xml;
using SoftwareControllerApi.Rule;
using SoftwareControllerLib.Config;

namespace WebCollector.Config
{
    /// <summary>
    /// Web specific configuration reader class.
    /// </summary>
    public class WebConfigReader : ConfigReaderBase<WebCollectorSession>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebConfigReader"/> class with the given configuration file path.
        /// </summary>
        /// <param name="config">The configuration file path.</param>
        public WebConfigReader(string config) : base(config) { }

        /// <summary>
        /// Read the session settings from the XML configuration file.
        /// </summary>
        protected override void InitSession()
        {
            if (!m_Reader.ReadToFollowing("settings")) {
                throw new XmlException("<settings> node not found");
            }

            string name = string.Empty;
            string address = string.Empty;

            XmlReader settingsReader = m_Reader.ReadSubtree();

            while (settingsReader.Read()) {
                if (settingsReader.Name.Equals("name")) {
                    settingsReader.Read();
                    if (settingsReader.NodeType == XmlNodeType.Text) {
                        name = settingsReader.Value;
                    }
                } else if (settingsReader.Name.Equals("address")) {
                    settingsReader.Read();
                    if (settingsReader.NodeType == XmlNodeType.Text) {
                        address = settingsReader.Value;
                    }
                }
            }

            m_Session = new WebCollectorSession(name, address);
        }

        /// <summary>
        /// Read actions from a configuration file and add to given rule.
        /// </summary>
        /// <param name="ruleReader">The XML reader for the rule.</param>
        /// <param name="rule">The rule where to add the parsed actions.</param>
        protected override void ReadActions(XmlReader ruleReader, IRule rule)
        {
           
        }
    }
}
