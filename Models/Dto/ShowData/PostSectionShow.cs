using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.ShowData
{
    public class PostSectionShow
    {
        public int Id { get; set; }

        public Guid PostId { get; set; }
        public PostModel Post { get; set; }

        public string SectionText { get; set; }

        public Guid? ImageId { get; set; }
        public ImageModel Image { get; set; }
        public byte[] ImageFile { get; set; }

        public int SectionOrder { get; set; }
    }
}
