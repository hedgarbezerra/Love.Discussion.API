using Love.Discussion.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IFeatureManager _featureManager;
        private ILogger _logger;

        public MeetingController(IFeatureManager featureManager, ILogger logger, IMeetingService meetingService)
        {
            _featureManager = featureManager;
            _logger = logger;
            _meetingService = meetingService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var meeting = _meetingService.GetMeeting(id);
            
            if (meeting is not null)
                return Ok(meeting);
            return NoContent();
        }
    }
}
