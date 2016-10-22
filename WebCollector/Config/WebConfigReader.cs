using System;
using System.Xml;
using SoftwareControllerApi.Action;
using SoftwareControllerApi.Rule;
using SoftwareControllerLib.Config;
using WebCollector.Actions;

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
            string startAddress = string.Empty;

            XmlReader settingsReader = m_Reader.ReadSubtree();

            while (settingsReader.Read()) {
                if (string.IsNullOrEmpty(name) && ReadNodeValue(settingsReader, out name)) continue;
                if (string.IsNullOrEmpty(address) && ReadNodeValue(settingsReader, out address)) continue;
                if (string.IsNullOrEmpty(startAddress) && ReadNodeValue(settingsReader, out startAddress)) continue;
            }

            m_Session = new WebCollectorSession(name, address, startAddress);
        }

        /// <summary>
        /// Read actions from a configuration file and add to given rule.
        /// </summary>
        /// <param name="ruleReader">The XML reader for the rule.</param>
        /// <param name="rule">The rule where to add the parsed actions.</param>
        protected override void ReadActions(XmlReader ruleReader, IRule rule)
        {
            while (ruleReader.Read()) {
                if (ruleReader.NodeType == XmlNodeType.Element && ruleReader.Name.Equals("action")) {
                    string typeStr = ruleReader.GetAttribute("type");
                    ActionType type;

                    if (!Enum.TryParse(typeStr.ToUpper(), out type)) continue;

                    XmlReader actionReader = ruleReader.ReadSubtree();

                    switch (type) {
                    case ActionType.NAVIGATE: {
                            NavigateAction action = new NavigateAction();
                            ReadTagAction(actionReader, rule, action);
                        }
                        break;

                    case ActionType.COLLECT: {
                            CollectAction action = new CollectAction();
                            ReadTagAction(actionReader, rule, action);
                        }
                        break;

                    case ActionType.WAIT:
                        ReadWaitAction(actionReader, rule);
                        break;

                    case ActionType.CLICK:
                    case ActionType.TYPE:
                        throw new XmlException(string.Format("Unsupported action type: {0}", type.ToString()));

                    default:
                        throw new XmlException(string.Format("Unknown action type: {0}", type.ToString()));
                    }
                }
            }
        }

        private void ReadTagAction<T>(XmlReader reader, IRule rule, T action) where T : TagActionBase
        {
            string tag = string.Empty;
            string cls = string.Empty;

            while (reader.Read()) {
                if (string.IsNullOrEmpty(tag) && ReadNodeValue(reader, out tag)) continue;
                if (string.IsNullOrEmpty(cls) && ReadNodeValue(reader, out cls)) continue;
            }

            action.Tag = tag;
            action.Class = cls;

            rule.AddAction(action);
        }

        private void ReadWaitAction(XmlReader reader, IRule rule)
        {
            string min = string.Empty;
            string max = string.Empty;

            while (reader.Read()) {
                if (string.IsNullOrEmpty(min) && ReadNodeValue(reader, out min)) continue;
                if (string.IsNullOrEmpty(max) && ReadNodeValue(reader, out max)) continue;
            }

            WaitAction action = new WaitAction(int.Parse(min), int.Parse(max));
            rule.AddAction(action);
        }

        private bool ReadNodeValue(XmlReader reader, out string output)
        {
            output = string.Empty;

            if (reader.NodeType == XmlNodeType.Text) {
                output = reader.Value;
                return true;
            }

            return false;
        }
    }
}
