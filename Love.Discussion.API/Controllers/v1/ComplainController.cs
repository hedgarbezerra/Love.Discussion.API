using Love.Discussion.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Love.Discussion.API.Controllers.v1
{
    //[Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ComplainController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IFeatureManager _featureManager;
        private ILogger _logger;

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var complain = _meetingService.GetComplain(id);

            if (complain is not null)
                return Ok(complain);
            return NoContent();
        }
    }
}
