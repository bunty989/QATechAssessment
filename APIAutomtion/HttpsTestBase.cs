using NUnit.Framework;

namespace APIAutomation
{
    [TestFixture]
    public abstract class HttpTestBase
    {

        [OneTimeSetUp]
        public void Initialise()
        {
        }
        

        [OneTimeTearDown]
        public void TearDown()
        {
        }
    }
}
