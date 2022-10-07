using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Entities
{
    public class Complain
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsPositive { get; set; }
        public string Extra { get; set; }
        public DateTime OccurenceDate { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int IdMeeting { get; set; }
        public int IdUser { get; set; }
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Meeting Meeting { get; set; }
    }
}
