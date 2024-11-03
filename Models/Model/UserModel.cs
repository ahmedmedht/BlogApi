using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class UserModel
    {
        public Guid Id { get; set; } // Changed to Guid

        [MaxLength(150)]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Guid? UserImageId { get; set; }
        public ImageModel UserImage { get; set; }

        public ICollection<PostModel> MyPosts { get; set; } // Posts authored by the user
        public ICollection<FavPostModel> FavPosts { get; set; } // Posts favorited by the user
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<ReactModel> Reacts { get; set; }


    }
}
