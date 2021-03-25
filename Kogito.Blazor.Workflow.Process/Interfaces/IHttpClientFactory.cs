using System.Net.Http;

namespace Kogito.Blazor.Workflow.Process.Interfaces
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}