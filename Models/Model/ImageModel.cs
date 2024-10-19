using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class ImageModel
    {
        public Guid Id { get; set; } // Changed to Guid

        [MaxLength(500)]
        public string Path { get; set; } // Path to the image

        public string ImageType { get; set; } // e.g., jpg, png

    }
}
