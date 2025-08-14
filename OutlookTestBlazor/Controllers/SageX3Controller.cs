using Microsoft.AspNetCore.Mvc;
using OutlookTestBlazor.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OutlookTestBlazor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SageX3Controller : ControllerBase
    {
        private readonly SageX3Service _sage;
        private readonly ILogger<SageX3Controller> _logger;

        public SageX3Controller(SageX3Service sage, ILogger<SageX3Controller> logger)
        {
            _sage = sage;
            _logger = logger;
        }

        [HttpPost("create-lead")]
        public async Task<IActionResult> CreateLead([FromBody] LeadRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
                return BadRequest(new { message = "Invalid request" });

            var (success, response) = await _sage.CreateLeadAsync(request.Name, request.Email, request.Subject);
            if (success) return Ok(new { message = "Lead created in Sage X3", raw = response });
            _logger.LogWarning("Failed to create lead: {resp}", response);
            return StatusCode(500, new { message = "Failed to create lead in Sage X3", raw = response });
        }
    }

    public class LeadRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
    }
}
