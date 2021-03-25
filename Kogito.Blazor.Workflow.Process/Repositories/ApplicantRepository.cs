using Kogito.Blazor.Workflow.Process.Entities.Domain;
using Kogito.Blazor.Workflow.Process.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kogito.Blazor.Workflow.Process.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly HttpClient _httpClient;

        public ApplicantRepository(Interfaces.IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<ApplicantWithId> PostNewApplicant(NewApplicant newApplicant)
        {
            var postBody = new Dictionary<string, NewApplicant>()
            {
                {"applicant", newApplicant }
            };

            var postBodyAsJson = JsonSerializer.Serialize(postBody);
            var content = new StringContent(postBodyAsJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("applicants", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            var postResult = JsonSerializer.Deserialize<ApplicantWithId>(responseBody);

            return postResult;
        }

        public async Task<List<ApplicantWithId>> GetApplicantsList()
        {
            var response = await _httpClient.GetAsync("applicants");
            string responseBody = await response.Content.ReadAsStringAsync();

            var applicantList = JsonSerializer.Deserialize<List<ApplicantWithId>>(responseBody);

            return applicantList;
        }

        public async Task<ApplicantWithId> GetApplicantById(string id)
        {
            var response = await _httpClient.GetAsync($"applicants/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();

            var applicant = JsonSerializer.Deserialize<ApplicantWithId>(responseBody);

            return applicant;
        }

        public async Task<List<ApplicantTask>> GetApplicantsTaskList(string id)
        {
            var response = await _httpClient.GetAsync($"applicants/{id}/tasks");
            string responseBody = await response.Content.ReadAsStringAsync();

            var taskList = JsonSerializer.Deserialize<List<ApplicantTask>>(responseBody);

            return taskList;
        }

        public async Task<ApplicantWithId> CompleteTask(string applicantId, string taskName, string taskId)
        {
            var postBody = new Dictionary<string, string>();

            var postBodyAsJson = JsonSerializer.Serialize(postBody);
            var content = new StringContent(postBodyAsJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"applicants/{applicantId}/{taskName}/{taskId}", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            var postResult = JsonSerializer.Deserialize<ApplicantWithId>(responseBody);

            return postResult;
        }
    }
}