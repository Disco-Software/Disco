﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.DAL.Entities
{
    public class ErrorMessage : Base.BaseEntity<int>
    {
        public string Message { get; set; }
    }
}
