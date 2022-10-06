using Love.Discussion.Core.Entities;
using Love.Discussion.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Repository.Repositories
{
    public class ComplainRepository : BaseRepository<Complain>
    {
        public ComplainRepository(LoveIdentityContext dbContext) : base(dbContext)
        {
        }
    }
}
