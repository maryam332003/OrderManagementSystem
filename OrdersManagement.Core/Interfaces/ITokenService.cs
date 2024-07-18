using Microsoft.AspNetCore.Identity;
using OrdersManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Repositories.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user, UserManager<User> _userManager);

    }
}
