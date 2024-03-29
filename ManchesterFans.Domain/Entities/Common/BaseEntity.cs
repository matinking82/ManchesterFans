﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManchesterFans.Domain.Entities.Common
{
    public class BaseEntity
    {
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
    }
}
