using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<PostModel> Posts { get; set; }
    }
}
