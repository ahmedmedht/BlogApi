﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class PostModel
    {
        public Guid Id { get; set; } 

        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; } 
        public CategoryModel Category { get; set; }

        public ICollection<PostSectionModel> PostSections { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public ICollection<ReactModel> Reacts { get; set; }
        public ICollection<TagModel> Tags { get; set; }
    }
}
