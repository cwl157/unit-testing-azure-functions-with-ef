﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzFuncUnitTestWithEf.Models
{
    public class BookInput
    {
        public string Title { get; set; }
        public string Author { get; set; }  
        public DateTime PublishedDate { get; set; } 
    }
}
