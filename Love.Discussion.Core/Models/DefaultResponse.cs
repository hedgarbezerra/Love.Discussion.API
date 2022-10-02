using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Models
{
    public class DefaultResponse<T>
    {
        public bool Successful { get; set; }
        public IList<string> Messages { get; set; }
        public T Data { get; set; }
    }
}
