﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Disco.Domain.Models.Base
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
