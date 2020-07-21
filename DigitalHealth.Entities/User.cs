using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }

        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        public Profile Profile { get; set; }
        public Guid? ProfileId { get; set; }
    }
}