using Newtonsoft.Json;
using System.Collections.Concurrent;
using Tests.Common.Configuration.Models;

namespace ResultsManager.Tests.Common.Configuration.Helpers
{
    public class TestResultCollector
    {
        //private static TestResultCollector instance;
        //private static object locker = new object();
        private static readonly string dateTime = DateTime.Now.ToString().Replace(":", ".");
        private static readonly string path = Directory.GetCurrentDirectory() + $"\\TestResults-{dateTime}.json";

        public ConcurrentDictionary<string, string> TestResults { get; set; }
        public List<TestResult> ListOfResults { get; set; }

        public TestResultCollector()
        {
            TestResults = new ConcurrentDictionary<string, string>();
            ListOfResults = new List<TestResult>();
        }

        public void AddResult(string TestName, string TestOutcome)
        {
            TestResults.TryAdd(TestName, TestOutcome);
        }

        public void SaveResults()
        {
            foreach (var test in TestResults)
            {
                ListOfResults.Add(new TestResult(test.Key, test.Value));
            }
            var resultsJson = JsonConvert.SerializeObject(ListOfResults);
            using (var sw = new StreamWriter(path))
            {
                sw.Write(resultsJson);
            }
        }

        //public static TestResultCollector getInstance()
        //{
        //    if(instance == null)
        //    {
        //        lock(locker)
        //        {
        //            if(instance == null)
        //            {
        //                instance = new TestResultCollector();
        //            }
        //        }
        //    }
        //    return instance;
        //}
    }
}

