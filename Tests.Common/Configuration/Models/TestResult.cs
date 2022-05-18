
namespace Tests.Common.Configuration.Models
{
    public class TestResult
    {
        public string? FullName;
        public string? Outcome;
        public TestResult(string fullname, string outcome)
        {
            FullName = fullname;
            Outcome = outcome;
        }
    }
}
