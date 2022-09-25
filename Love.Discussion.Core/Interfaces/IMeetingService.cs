using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Models;

namespace Love.Discussion.Core.Interfaces
{
    public interface IMeetingService
    {
        void FinishMeeting(int meetingId);
        PaginatedList<Complain> GetPaginatedComplains(string path, PaginationInput pagination);
        IQueryable<Complain> GetComplains();
        public Complain GetComplain(int id);
        IQueryable<Meeting> GetMeetings();
        public Meeting GetMeeting(int id);
        Meeting LatestMeeting(DateTime? minDate = null);
        bool NewComplain(Complain complain);
        bool NewMeeting(Meeting meeting);
        void UpdateMeetingDate(int meetingId, DateTime newDate);
    }
}
