using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class Result
    {
        public object Value;
        public List<string> ErrorMessage;
        public int StatusCode;
        public Result()
        {
            Value = null;
            ErrorMessage = new List<string>();
            StatusCode = 200;
        }
    }
}
