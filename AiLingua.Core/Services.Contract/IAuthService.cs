using AiLingua.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Services.Contract
{
    public interface IAuthService
    {
     Task<string>  CreateTokenAsync(User user, UserManager<User> userManager);

    }
}
