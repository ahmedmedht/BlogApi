using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public Guid PostId { get; set; } 
        public PostModel Post { get; set; }

        public Guid UserId { get; set; } 
        public UserModel User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
