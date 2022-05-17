using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Helpers;

namespace Tests.Integration
{
    [TestFixture]
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
            string name = TestContext.CurrentContext.Test.FullName.ToString();
            string result = TestContext.CurrentContext.Result.Outcome.ToString();
            var collector = TestResultCollector.getInstance();
            collector.AddResult(name, result);
        }
    }
    [SetUpFixture]
    public class Assembly
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }
        [OneTimeTearDown]
        public void OneTimeTeatDown()
        {
            var collector = TestResultCollector.getInstance();
            collector.SaveResults();
        }
    }
}