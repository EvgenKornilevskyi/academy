using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using Tests.Common.Configuration;

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
            string name = TestContext.CurrentContext.Test.Name.ToString();
            string result = TestContext.CurrentContext.Result.Outcome.ToString();
            TestServices.TestResultCollector.AddResult(name, result);
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
            TestServices.TestResultCollector.SaveResults();
        }
    }
}