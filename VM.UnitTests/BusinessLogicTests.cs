using NUnit.Framework;
using VM.BusinessLogic;

namespace VM.UnitTests
{
    [TestFixture]
    public class BusinessLogicTests : BaseClass
    {
        public Change Change { get; set; }

        [SetUp]
        public void FixtureSetup()
        {
            
        }

        [Test]
        public void TestCase()
        {
            var change = new Change(2.35m);
            Assert.IsNotEmpty(change.ToString()); 
            Assert.IsTrue(change.ToString().Contains("$"));
            Assert.IsTrue(change.ToString().Contains("¢"));


        }

        [TearDown]
        public void FixtureTearDown()
        {
        }
    }
}
