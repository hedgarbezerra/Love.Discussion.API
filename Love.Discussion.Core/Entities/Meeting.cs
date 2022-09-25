using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Entities
{
    public class Meeting
    {
        public int Id { get; set; }
        public DateTime DateBegin { get; set; } = DateTime.Now;
        public DateTime DateEnd { get; set; } = DateTime.MinValue;
        public virtual IList<Complain> Complains { get; set; } = new List<Complain>();
    }
}
