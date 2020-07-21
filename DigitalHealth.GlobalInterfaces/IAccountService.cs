using DigitalHealth.Web.EntitiesDto;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IAccountService
    {
         Task<bool> Login(AccountLoginDto dto, IAuthenticationManager AuthenticationManager);
         Task Logout(IAuthenticationManager AuthenticationManager);
         Task<bool> LoginExist(string Login);
         Task<bool> PasswordMatch(string password, string repeatpassword);
         Task<RoleDto> GetRole(string name);
         Task<bool> Register(AccountRegisterDto dto);
         Task<ProfileDto> GetProfile(Guid userId);
         Task<Guid> GetUserId(string name);
         Task UpdateProfile(ProfileDto dto);
         Task<bool> VerifyHashedPassword(string hashedPassword, string password);

    }
}
