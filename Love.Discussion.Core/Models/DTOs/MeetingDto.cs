using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Models.DTOs
{
    public class MeetingDto
    {
        public DateTime DateBegin { get; set; } = DateTime.Now;
        public DateTime DateEnd { get; set; } = DateTime.MinValue;
        public virtual IList<ComplainDto> Complains { get; set; } = new List<ComplainDto>();
    }
}
