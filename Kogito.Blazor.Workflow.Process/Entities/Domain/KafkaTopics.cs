namespace Kogito.Blazor.Workflow.Process.Entities.Domain
{
    public class ProcessedApplicant
    {
        public string id { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string data { get; set; }
        public string kogitoProcessinstanceId { get; set; }
        public string kogitoProcessId { get; set; }
        public string kogitoProcessinstanceState { get; set; }
        public string specversion { get; set; }
    }
}