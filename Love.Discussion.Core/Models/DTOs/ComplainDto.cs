using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Models.DTOs
{
    public class ComplainDto
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool IsPositive { get; set; }
        public string Extra { get; set; } = string.Empty;
        public DateTime OccurenceDate { get; set; }
    }
}
