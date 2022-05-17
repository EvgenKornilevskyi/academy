using System.Collections.Concurrent;

namespace Tests.Common.Configuration.Helpers
{
    public class TestResultCollector
    {
        private static TestResultCollector instance;
        private static object locker = new object();
        private string path = Directory.GetCurrentDirectory() + "\\TestResults.json";

        public ConcurrentDictionary<string, string> TestResults { get; set; }

        protected TestResultCollector()
        {
            TestResults = new ConcurrentDictionary<string,string>();
        }

        public void AddResult(string TestName, string TestOutcome)
        {
            TestResults.TryAdd(TestName, TestOutcome);
        }

        public void SaveResults()
        {
            using(var sr = new StreamWriter(path))
            {
                foreach(var test in TestResults)
                {
                    sr.Write(test.Key);
                    sr.Write("  ");
                    sr.WriteLine(test.Value);
                }
            }
        }
        
        public static TestResultCollector getInstance()
        {
            if(instance == null)
            {
                lock(locker)
                {
                    if(instance == null)
                    {
                        instance = new TestResultCollector();
                    }
                }
            }
            return instance;
        }
    }
}
