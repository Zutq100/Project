using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Models
{
    public class AuthData : IdentityUser
    {
        public string UserName { get; set; }
    }
}
