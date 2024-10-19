using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class PostSectionModel
    {
        public int Id { get; set; }

        public Guid PostId { get; set; } 
        public PostModel Post { get; set; }

        public string SectionText { get; set; }

        public Guid? ImageId { get; set; } 
        public ImageModel Image { get; set; }

        public int SectionOrder { get; set; }
    }
}
