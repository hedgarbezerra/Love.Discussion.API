using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Interfaces;
using Love.Discussion.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IRepository<Meeting> _meetingRepo;
        private readonly IRepository<Complain> _complainRepo;
        private readonly IUriService _uriService;
        public MeetingService(IRepository<Meeting> meetingRepo, IRepository<Complain> complainRepo, IUriService uriService)
        {
            _meetingRepo = meetingRepo;
            _complainRepo = complainRepo;
            _uriService = uriService;
        }

        public Meeting LatestMeeting(DateTime? minDate = null) => minDate is null ?
            _meetingRepo.Get().OrderByDescending(m => m.DateEnd).FirstOrDefault() :
            _meetingRepo.Get(m => m.DateEnd >= minDate).OrderByDescending(m => m.DateEnd).FirstOrDefault();

        public bool NewMeeting(Meeting meeting)
        {
            var latestMeeting = LatestMeeting();
            var startDate = latestMeeting is null ? DateTime.Now : latestMeeting.DateEnd;
            meeting.DateBegin = startDate;

            var result = _meetingRepo.Add(meeting);

            _meetingRepo.SaveChanges();

            return result.Id > 0;
        }

        public bool NewComplain(Complain complain)
        {
            var latestMeeting = LatestMeeting(DateTime.Now);
            if (latestMeeting is null)
                return false;

            var result = _complainRepo.Add(complain);
            _complainRepo.SaveChanges();

            return result.Id > 0;
        }

        public void UpdateMeetingDate(int meetingId, DateTime newDate)
        {
            Meeting meeting = _meetingRepo.Get(meetingId);
            meeting.DateEnd = newDate.AddDays(1).AddTicks(-1);
            _meetingRepo.Update(meeting);
            _meetingRepo.SaveChanges();
        }

        public void FinishMeeting(int meetingId)
        {
            Meeting meeting = _meetingRepo.Get(meetingId);
            meeting.DateEnd = DateTime.Today.AddDays(1).AddTicks(-1);
            _meetingRepo.Update(meeting);

            NewMeeting(new Meeting());
        }

        public PaginatedList<Complain> GetPaginatedComplains(string path, PaginationInput pagination)
            => new PaginatedList<Complain>(_complainRepo.Get(), _uriService, path, pagination.Index, pagination.Size);

        public IQueryable<Complain> GetComplains() => _complainRepo.Get().OrderBy(c => c.Date);
        public Complain GetComplain(int id) => _complainRepo.Get(id);

        public IQueryable<Meeting> GetMeetings() => _meetingRepo.Get().OrderBy(m => m.DateBegin);
        public Meeting GetMeeting(int id) => _meetingRepo.Get(id);
    }
}
