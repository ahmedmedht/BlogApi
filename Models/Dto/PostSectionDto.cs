using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class PostSectionDTO
    {
        public int Id { get; set; }
        public string SectionText { get; set; }
        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int SectionOrder { get; set; }
    }
}
