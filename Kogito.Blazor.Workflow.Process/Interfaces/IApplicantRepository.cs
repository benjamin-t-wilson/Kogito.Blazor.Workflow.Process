using Kogito.Blazor.Workflow.Process.Entities.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kogito.Blazor.Workflow.Process.Interfaces
{
    public interface IApplicantRepository
    {
        Task<ApplicantWithId> PostNewApplicant(NewApplicant newApplicant);

        Task<List<ApplicantWithId>> GetApplicantsList();

        Task<ApplicantWithId> GetApplicantById(string id);

        Task<List<ApplicantTask>> GetApplicantsTaskList(string id);

        Task<ApplicantWithId> CompleteTask(string applicantId, string taskName, string taskId);
    }
}