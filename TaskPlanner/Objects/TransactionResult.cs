using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskPlanner.Objects
{
    public class TransactionResult
    {
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string MethodName { get; set; }
        public string Information { get; set; }
        public string ErrorMessage { get; set; }
        
    }

}
