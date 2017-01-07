namespace WebCollectorTest.Actions
{
    using System.Collections.Generic;
    using System.IO;
    using NUnit.Framework;
    using SoftwareControllerApi.Action;
    using SoftwareControllerLib.Action;
    using WebCollector;
    using WebCollector.Actions;
    using WebCollector.Actions.Collect;
    using WebCollector.Config;

    [TestFixture]
    public class TagCollectActionTest
    {
        [Test]
        [Category("TagCollectAction")]
        public void TagCollect_MultiCollect_Execute()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "yellowpages_config.xml"));
            WebCollectorSession session = reader.Read();

            using (StreamReader sr = new StreamReader(Path.Combine("Resources", "yellowpages_sample.html"))) {
                session.Html = sr.ReadToEnd();
                TagCollectAction tagCollectAction = new TagCollectAction(session, true);
                tagCollectAction.Tag = "a";
                tagCollectAction.Class = "business-name";
                IResult result = tagCollectAction.Execute();

                Assert.That(result, Is.Not.Null);
                Assert.That(result.State, Is.EqualTo(ActionState.SUCCESS));

                SingleResult<IList<ItemBase>> listResult = result as SingleResult<IList<ItemBase>>;
                Assert.That(listResult.Content.Count, Is.EqualTo(30));
            }
        }

        [Test]
        [Category("TagCollectAction")]
        public void TagCollect_SingleCollect_Execute()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "yellowpages_config.xml"));
            WebCollectorSession session = reader.Read();

            using (StreamReader sr = new StreamReader(Path.Combine("Resources", "yellowpages_sample.html"))) {
                session.Html = sr.ReadToEnd();
                TagCollectAction tagCollectAction = new TagCollectAction(session, false);
                tagCollectAction.Tag = "a";
                tagCollectAction.Class = "business-name";
                IResult result = tagCollectAction.Execute();

                Assert.That(result, Is.Not.Null);
                Assert.That(result.State, Is.EqualTo(ActionState.SUCCESS));

                SingleResult<ItemBase> singleResult = result as SingleResult<ItemBase>;
                Assert.That(singleResult.Content, Is.InstanceOf<TextItem>());
            }
        }

        [Test]
        [Category("TagCollectAction")]
        public void TagCollect_Fail_Execute()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "yellowpages_config.xml"));
            WebCollectorSession session = reader.Read();

            using (StreamReader sr = new StreamReader(Path.Combine("Resources", "yellowpages_sample.html"))) {
                session.Html = sr.ReadToEnd();
                TagCollectAction tagCollectAction = new TagCollectAction(session, true);
                tagCollectAction.Tag = "inexistent-tag";
                tagCollectAction.Class = "inexistent-class";
                IResult result = tagCollectAction.Execute();

                Assert.That(result, Is.Not.Null);
                Assert.That(result.State, Is.EqualTo(ActionState.FAIL));
            }
        }

        [Test]
        [TestCase("a", "business-name", true)]
        [TestCase("inexistent-tag", "inexistent-class", false)]
        [Category("TagCollectAction")]
        public void TagCollect_CanCollect_Execute(string tag, string cls, bool canRepeat)
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "yellowpages_config.xml"));
            WebCollectorSession session = reader.Read();

            using (StreamReader sr = new StreamReader(Path.Combine("Resources", "yellowpages_sample.html"))) {
                session.Html = sr.ReadToEnd();
                TagCollectAction tagCollectAction = new TagCollectAction(session, true);
                tagCollectAction.Tag = tag;
                tagCollectAction.Class = cls;
                tagCollectAction.Execute();

                Assert.That(tagCollectAction.CanRepeat(), Is.EqualTo(canRepeat));
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        [Category("TagCollectAction")]
        public void TagCollect_IsMultiCollect(bool isMultiCollect)
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "yellowpages_config.xml"));
            WebCollectorSession session = reader.Read();

            using (StreamReader sr = new StreamReader(Path.Combine("Resources", "yellowpages_sample.html"))) {
                session.Html = sr.ReadToEnd();
                TagCollectAction tagCollectAction = new TagCollectAction(session, isMultiCollect);
                Assert.That(tagCollectAction.IsMultipleCollect, Is.EqualTo(isMultiCollect));

                tagCollectAction.IsMultipleCollect = isMultiCollect;
                Assert.That(tagCollectAction.IsMultipleCollect, Is.EqualTo(isMultiCollect));
            }
        }
    }
}