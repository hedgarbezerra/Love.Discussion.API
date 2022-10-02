using AutoMapper;
using FluentValidation;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Interfaces;
using Love.Discussion.Core.Models.DTOs;
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
        private readonly IMapper _mapper;
        private readonly IValidator<Complain> _complainValidator;
        private readonly IMeetingService _meetingService;
        private readonly IFeatureManager _featureManager;
        private readonly ILogger<ComplainController> _logger;

        public ComplainController(IMapper mapper, IMeetingService meetingService, IFeatureManager featureManager, ILogger<ComplainController> logger, IValidator<Complain> complainValidator)
        {
            _mapper = mapper;
            _meetingService = meetingService;
            _featureManager = featureManager;
            _logger = logger;
            _complainValidator = complainValidator;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var complain = _meetingService.GetComplain(id);
            if (complain is not null)
            {
                var complainDto = _mapper.Map<ComplainDto>(complain);
                return Ok(complainDto);
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ComplainDto complainDto)
        {
            var complain = _mapper.Map<Complain>(complainDto);
            var validationResult = _complainValidator.Validate(complain);

            if (validationResult.IsValid)
                return Ok(true);
            else
                return Ok(false);
            
        }
    }
}
