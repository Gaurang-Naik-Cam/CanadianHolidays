﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.Models
{
    public class Root
    {
        public List<Holiday> holidays { get; set; }

        public Root()
        {
            holidays = new List<Holiday>();
            
        }
    }
}
