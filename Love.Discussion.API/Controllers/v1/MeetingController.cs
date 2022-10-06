using AutoMapper;
using FluentValidation;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Interfaces;
using Love.Discussion.Core.Models;
using Love.Discussion.Core.Models.DTOs;
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
        private readonly ILogger<MeetingController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<MeetingDto> _meetingValidator;

        public MeetingController(IFeatureManager featureManager, ILogger<MeetingController> logger, IMeetingService meetingService, IMapper mapper, IValidator<MeetingDto> meetingValidator)
        {
            _featureManager = featureManager;
            _logger = logger;
            _meetingService = meetingService;
            _mapper = mapper;
            _meetingValidator = meetingValidator;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var meeting = _meetingService.GetMeeting(id);
            if (meeting is not null)
            {
                var mappedMeeting = _mapper.Map<MeetingDto>(meeting);
                var result = new DefaultResponse<MeetingDto>();
                return Ok(mappedMeeting);
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody]MeetingDto meetingDto)
        {
            var validationResult = _meetingValidator.Validate(meetingDto);
            if (!validationResult.IsValid)
                return Ok(validationResult.Errors);

            var meeting = _mapper.Map<Meeting>(meetingDto);

            if (validationResult.IsValid)
            {
                return Ok(true);
            }
            else
                return Ok(false);  
        }
    }
}
