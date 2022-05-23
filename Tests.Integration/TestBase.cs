using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;

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
            var testName = TestContext.CurrentContext.Test.Name.ToString();
            var testResult = TestContext.CurrentContext.Result.Outcome.ToString();
            TestServices.TestResultCollector.AddResult(testName, testResult);
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