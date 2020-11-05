namespace WebCollector.Config
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using Actions;
    using Rule;
    using SoftwareControllerApi.Action;
    using SoftwareControllerApi.Rule;
    using SoftwareControllerLib.Config;

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
        /// <exception cref="XmlException"><settings> tag was not found.</exception>
        protected override void InitSession()
        {
            if (!m_Reader.ReadToFollowing("settings")) {
                throw new XmlException("<settings> node not found");
            }

            string name = string.Empty;
            string address = string.Empty;
            string startAddress = string.Empty;
            IList<string> outputFiles = new List<string>();

            XmlReader settingsReader = m_Reader.ReadSubtree();

            while (settingsReader.Read()) {
                if (string.IsNullOrEmpty(name) && ReadNodeValue(settingsReader, out name)) continue;
                if (string.IsNullOrEmpty(address) && ReadNodeValue(settingsReader, out address)) continue;
                if (string.IsNullOrEmpty(startAddress) && ReadNodeValue(settingsReader, out startAddress)) continue;
                if (outputFiles.Count == 0 && ReadOutputNodes(settingsReader, outputFiles)) continue;
            }

            m_Session = new WebCollectorSession(name, address, startAddress, outputFiles);
        }

        /// <summary>
        /// Read actions from a configuration file and add to given rule.
        /// </summary>
        /// <param name="ruleReader">The XML reader for the rule.</param>
        /// <param name="rule">The rule where to add the parsed actions.</param>
        /// <exception cref="XmlException">"Unsupported action type found.</exception>
        protected override void ReadActions(XmlReader ruleReader, IRule rule)
        {
            UpdateProcessors(rule);

            while (ruleReader.Read()) {
                if (ruleReader.NodeType != XmlNodeType.Element || !ruleReader.Name.Equals("action")) continue;

                string typeStr = ruleReader.GetAttribute("type");
                ActionType type;
                if (!Enum.TryParse(typeStr.ToUpper(), out type)) continue;

                XmlReader actionReader = ruleReader.ReadSubtree();

                switch (type) {
                case ActionType.NAVIGATE: {
                        string where = GetStringAttribute(ruleReader, "where");
                        NavigateAction action = new NavigateAction(m_Session, where);
                        ReadTagAction(actionReader, rule, action);
                    }
                    break;

                case ActionType.COLLECT: {
                        bool isMultiCollect = GetBooleanAttribute(ruleReader, "isMultiCollect");
                        TagCollectAction action = new TagCollectAction(m_Session, isMultiCollect);

                        bool isRepeatCondition = GetBooleanAttribute(ruleReader, "isRepeatCondition");
                        if (isRepeatCondition) {
                            IRepeatableRule repeatableRule = rule as IRepeatableRule;
                            repeatableRule.CanRepeat += action.CanRepeat;
                        }

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

        private void UpdateProcessors(IRule rule)
        {
            if (!rule.IsProcessable) return;

            rule.ResultProcessors = new List<IResultProcessor>();
            foreach (string file in m_Session.OutputFiles) {
                rule.ResultProcessors.Add(ProcessorFactory.Create(file));
            }
        }

        private void ReadTagAction<T>(XmlReader reader, IRule rule, T action) where T : TagActionBase
        {
            while (reader.Read()) {
                if (!reader.Name.Equals("tag")) {
                    continue;
                } else {
                    break;
                }
            }

            action.Tag = reader.GetAttribute("value");
            action.Attributes = ReadProperties(reader);

            rule.AddAction(action);
        }

        private readonly string c_DoubleQuote = "double";

        private List<TagAttribute> ReadProperties(XmlReader reader)
        {
            List<TagAttribute> properties = new List<TagAttribute>();
            string key = null;
            string value = null;
            string quote_type = c_DoubleQuote;

            while (reader.Read()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    key = reader.Name;
                    quote_type = GetStringAttribute(reader, "quote_type");
                } else if (reader.NodeType == XmlNodeType.Text) {
                    value = reader.Value;
                }

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value)) {
                    AttributeDelimiter delimiter = string.IsNullOrEmpty(quote_type) ?
                        AttributeDelimiter.DoubleQuote :
                        (AttributeDelimiter)Enum.Parse(typeof(AttributeDelimiter), quote_type);
                    TagAttribute tagAttribute = new TagAttribute(key, value, delimiter);
                    properties.Add(tagAttribute);
                    key = null;
                    value = null;
                    quote_type = c_DoubleQuote;
                }

                if (reader.Name.Equals("tag") && reader.NodeType == XmlNodeType.EndElement) break;
            }

            return properties;
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

        private bool ReadOutputNodes(XmlReader reader, IList<string> outputFiles)
        {
            if (!reader.Name.Equals("output")) return false;

            while (reader.Read()) {
                if (reader.NodeType != XmlNodeType.Element || !reader.Name.Equals("save")) continue;

                string save;
                reader.Read();
                ReadNodeValue(reader, out save);
                outputFiles.Add(save);
            }

            return true;
        }
    }
}
