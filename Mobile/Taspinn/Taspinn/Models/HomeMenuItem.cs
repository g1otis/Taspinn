﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Taspinn.Models
{
    public enum MenuItemType
    {
        Shopping,
        Disposed,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
