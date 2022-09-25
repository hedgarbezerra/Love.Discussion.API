using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Models
{
    public class PaginationInput
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
