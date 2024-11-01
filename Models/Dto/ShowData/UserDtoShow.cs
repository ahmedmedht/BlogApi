using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.ShowData
{
    public class UserDtoShow
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        // The image file that will be uploaded when creating or updating the user
        public Byte[] ImageUser { get; set; }
    }
}
