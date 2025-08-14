using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OutlookTestBlazor.Services
{
    public class SageX3Service
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SageX3Service> _logger;

        public SageX3Service(HttpClient httpClient, ILogger<SageX3Service> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool Success, string Response)> CreateLeadAsync(string name, string email, string subject)
        {
            var payload = new { name = name, email = email, subject = subject };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("https://your-sage-x3-host.example.com/sdata/crm/lead", content);
                var respText = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("SageX3 response: {status} {text}", response.StatusCode, respText);
                return (response.IsSuccessStatusCode, respText);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error calling Sage X3");
                return (false, ex.Message);
            }
        }
    }
}
