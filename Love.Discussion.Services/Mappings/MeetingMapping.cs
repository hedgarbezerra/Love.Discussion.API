using AutoMapper;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Models.DTOs;

namespace Love.Discussion.Services.Mappings
{
    public class MeetingMapping : Profile
    {
        public MeetingMapping()
        {
            CreateMap<Complain, ComplainDto>();
            CreateMap<ComplainDto, Complain>();
            CreateMap<Meeting, MeetingDto>();
            CreateMap<MeetingDto, Meeting>();
        }
    }
}
