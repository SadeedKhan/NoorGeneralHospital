﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public bool IsActive { get; set; }
    }
}