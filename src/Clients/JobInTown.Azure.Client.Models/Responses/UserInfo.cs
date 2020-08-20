using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobInTown.Azure.Client.Models.Responses
{
    public class UserInfo
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }

        public string ImageUrl { get; set; }

        public string FullName { get; set; }
    }
}
