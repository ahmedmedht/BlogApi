using Models.Dto.ShowData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class PostDTO
    {
        //public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }
        //public string CategoryName { get; set; }

        //public ICollection<PostSectionDTO> PostSections { get; set; }
        //public ICollection<CommentDtoShow> Comments { get; set; }
        //public ICollection<ReactDtoShow> Reacts { get; set; }
        //public ICollection<string> TagNames { get; set; }
    }
}
