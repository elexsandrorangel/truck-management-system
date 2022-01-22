﻿using System;

namespace TruckManagement.ViewModels.Base
{
    public abstract class BaseViewModel
    {
        public int Id { get; set; }
     
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
