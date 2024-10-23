using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class ImageDTO
    {
        public Guid Id { get; set; }
        public string Path { get; set; } // Path to the image file
        public string ImageType { get; set; } // File type (e.g., jpg, png)
        public string Url { get; set; } // URL of the image for easy retrieval
    }
}
