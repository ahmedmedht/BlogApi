using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<PostSectionDTO> PostSections { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<ReactDTO> Reacts { get; set; }
        public ICollection<string> TagNames { get; set; }
    }
}
