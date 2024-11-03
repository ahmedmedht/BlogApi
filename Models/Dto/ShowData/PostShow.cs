using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.ShowData
{
    public class PostShow
    {
        public Guid Id { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }

        public UserModel User { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        public ICollection<PostSectionShow> PostSections { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<ReactModel> Reacts { get; set; }
        public ICollection<string> TagNames { get; set; }
    }
}
