namespace WebCollectorTest.Actions
{
    using System;
    using NUnit.Framework;
    using SoftwareControllerApi.Action;
    using WebCollector.Actions;

    [TestFixture]
    public class WaitActionTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Category("WaitAction")]
        public void NegativeMin()
        {
            new WaitAction(-1, 5);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Category("WaitAction")]
        public void NegativeMax()
        {
            new WaitAction(3, -40);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("WaitAction")]
        public void MaxLessThanMin()
        {
            new WaitAction(40, 20);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("WaitAction")]
        public void NullName()
        {
            new WaitAction(null, 20, 100);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("WaitAction")]
        public void EmptyName()
        {
            new WaitAction(string.Empty, 20, 100);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("WaitAction")]
        public void WhitespaceName()
        {
            new WaitAction("   ", 20, 100);
        }

        [Test]
        [Category("WaitAction")]
        public void CheckMinAndMax()
        {
            WaitAction waitAction = new WaitAction(1000, 5000);

            Assert.That(waitAction.Minimum, Is.EqualTo(1000));
            Assert.That(waitAction.Maximum, Is.EqualTo(5000));
        }

        [Test]
        [Category("WaitAction")]
        public void CheckMinMaxAndName()
        {
            WaitAction waitAction = new WaitAction("abc", 1000, 5000);

            Assert.That(waitAction.Name, Is.EqualTo("abc"));
            Assert.That(waitAction.Minimum, Is.EqualTo(1000));
            Assert.That(waitAction.Maximum, Is.EqualTo(5000));
        }

        [Test]
        [Timeout(2100)]
        [Category("WaitAction")]
        public void CheckWait()
        {
            WaitAction waitAction = new WaitAction(1000, 2000);
            IResult result = waitAction.Execute();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.State, Is.EqualTo(ActionState.SUCCESS));
        }
    }
}
