﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class ReactDTO
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public bool? Liked { get; set; }
    }
}
