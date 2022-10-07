using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual IList<Complain> Complains { get; set; }
    }
}
