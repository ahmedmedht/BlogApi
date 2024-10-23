using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public Guid? ImageId { get; set; } // Nullable, as a user may not have an image
        public string ImageUrl { get; set; } // URL for the user's image (if available)

        // The image file that will be uploaded when creating or updating the user
        public IFormFile ImageFile { get; set; }
    }
}
