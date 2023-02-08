using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.Data.Models
{
    public class LocalUser : IdentityUser
    {
        public List<Note> Notes { get; set; }

    }
}
