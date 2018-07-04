using NUnit.Framework;

namespace VM.UnitTests
{
    public class BaseClass
    {
        [OneTimeSetUp]
        public void BaseSetup()
        {
        }
        [OneTimeTearDown]
        public void BaseTearDown()
        {
        }
    }
}
