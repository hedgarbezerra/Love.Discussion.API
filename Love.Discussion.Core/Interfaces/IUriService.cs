using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Interfaces
{
    public interface IUriService
    {
        Uri GetPageUri(int pageIndex, int pageSize, string route);
        Uri GetUri(string route);
    }
}
