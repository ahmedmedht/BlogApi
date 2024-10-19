using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class ReactModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public Guid PostId { get; set; }
        public PostModel Post { get; set; }
        public bool? Liked { get; set; } = null;
    }
}
