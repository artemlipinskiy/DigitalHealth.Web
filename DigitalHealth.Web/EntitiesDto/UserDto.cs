using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.EntitiesDto
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid RoleId { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }

    }
}