using Love.Discussion.Core.Entities;
using Love.Discussion.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Repository.Repositories
{
    public class MeetingRepository : BaseRepository<Meeting>
    {
        public MeetingRepository(LoveIdentityContext dbContext) : base(dbContext)
        {
        }
    }
}
