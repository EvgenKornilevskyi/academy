using NUnit.Framework;

namespace Tests.Integration
{
    [TestFixture] 
    [Category("Regression")]
    [Parallelizable(ParallelScope.All)]
    public class TestBase
    {
        [SetUp]
        public void Init()
        {
        }

        [TearDown] 
        public void Cleanup()
        {
        }
    }
}