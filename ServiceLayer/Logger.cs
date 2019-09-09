using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    class Logger
    {
        public string Time { get; set; }
        public string Event { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        
    }
}
