using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kogito.Blazor.Workflow.Process.Entities.Domain
{
    public class NewApplicant
    {
        [Required]
        public string name { get; set; }

        [Required]
        public int creditScore { get; set; }
    }

    public class Applicant
    {
        public string name { get; set; }
        public int creditScore { get; set; }
        public bool approved { get; set; }
    }

    public class ApplicantWithId
    {
        public string id { get; set; }
        public Applicant applicant { get; set; }
    }

    public class ApplicantTask
    {
        public string id { get; set; }
        public string nodeInstanceId { get; set; }
        public string name { get; set; }
        public int state { get; set; }
        public string phase { get; set; }
        public string phaseStatus { get; set; }
        public ApplicantTaskParameters parameters { get; set; }
        public Dictionary<string, string> results { get; set; }
    }

    public class ApplicantTaskParameters
    {
        public string Skippable { get; set; }
        public string TaskName { get; set; }
        public string NodeName { get; set; }
        public Applicant applicant { get; set; }
    }
}